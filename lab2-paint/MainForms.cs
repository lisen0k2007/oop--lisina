using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace PaintApp
{
    public partial class MainForm : Form
    {
        private Drawing _drawing = new Drawing();
        private Shape _currentShape = null;
        private string _tool = "Line";
        private Color _color = Color.Black;
        private int _thickness = 2;

        // Для перемещения фигур (★)
        private Shape _selectedShape = null;
        private bool _isMoving = false;
        private Point _lastMousePos;

        // Для истории (★★)
        private Stack<string> _undoStack = new Stack<string>();
        private Stack<string> _redoStack = new Stack<string>();

        public MainForm()
        {
            InitializeComponent();
            
            this.DoubleBuffered = true;

            // Подписки
            pictureBox.Paint += PictureBox_Paint;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;

            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;

            // Подсветка первой кнопки
            btnLine.BackColor = Color.LightBlue;
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            // Очищаем фон (обязательно для предпросмотра)
            e.Graphics.Clear(Color.White);

            // 1. Рисуем все готовые фигуры
            _drawing.Draw(e.Graphics);

            // 2. Рисуем фигуру, которую сейчас тянем (предпросмотр)
            if (_currentShape != null)
            {
                _currentShape.Draw(e.Graphics);
            }

            // 3. Рисуем выделение
            if (_selectedShape != null)
            {
                using var pen = new Pen(Color.Red, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
                var b = new Rectangle(
                    Math.Min(_selectedShape.Start.X, _selectedShape.End.X) - 5,
                    Math.Min(_selectedShape.Start.Y, _selectedShape.End.Y) - 5,
                    Math.Abs(_selectedShape.End.X - _selectedShape.Start.X) + 10,
                    Math.Abs(_selectedShape.End.Y - _selectedShape.Start.Y) + 10);
                e.Graphics.DrawRectangle(pen, b);
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            // Проверка на клик по фигуре (для перемещения)
            _selectedShape = null;
            for (int i = _drawing.Shapes.Count - 1; i >= 0; i--)
            {
                if (_drawing.Shapes[i].Contains(e.Location))
                {
                    _selectedShape = _drawing.Shapes[i];
                    _isMoving = true;
                    _lastMousePos = e.Location;
                    pictureBox.Invalidate();
                    return;
                }
            }

            // Создаем новую фигуру
            SaveState(); // Сохраняем для Undo
            
            switch (_tool)
            {
                case "Line": _currentShape = new LineShape(e.Location, _color, _thickness); break;
                case "Rect": _currentShape = new RectShape(e.Location, _color, _thickness); break;
                case "Ellipse": _currentShape = new EllipseShape(e.Location, _color, _thickness); break;
                case "Pencil": _currentShape = new PencilShape(e.Location, _color, _thickness); break;
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            // Если тянем фигуру (предпросмотр)
            if (_currentShape != null)
            {
                _currentShape.MoveEnd(e.Location); // Обновляем конец
                pictureBox.Invalidate();           // Перерисовываем (видим предпросмотр)
            }
            // Если перемещаем выделенную фигуру
            else if (_isMoving && _selectedShape != null)
            {
                int dx = e.X - _lastMousePos.X;
                int dy = e.Y - _lastMousePos.Y;
                _selectedShape.Move(dx, dy);
                _lastMousePos = e.Location;
                pictureBox.Invalidate();
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (_currentShape != null)
            {
                _drawing.Add(_currentShape); // Фиксируем фигуру
                _currentShape = null;        // Очищаем временную переменную
                pictureBox.Invalidate();
            }
            _isMoving = false;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z) DoUndo();
            if (e.Control && e.KeyCode == Keys.Y) DoRedo();
            
            if (e.KeyCode == Keys.Delete && _selectedShape != null)
            {
                SaveState();
                _drawing.Shapes.Remove(_selectedShape);
                _selectedShape = null;
                pictureBox.Invalidate();
            }
        }

        // --- КНОПКИ ИНСТРУМЕНТОВ ---
        private void ResetButtonsColor()
        {
            btnLine.BackColor = SystemColors.Control;
            btnRect.BackColor = SystemColors.Control;
            btnEllipse.BackColor = SystemColors.Control;
            btnPencil.BackColor = SystemColors.Control;
        }

        private void btnLine_Click(object sender, EventArgs e) { _tool = "Line"; ResetButtonsColor(); btnLine.BackColor = Color.LightBlue; }
        private void btnRect_Click(object sender, EventArgs e) { _tool = "Rect"; ResetButtonsColor(); btnRect.BackColor = Color.LightBlue; }
        private void btnEllipse_Click(object sender, EventArgs e) { _tool = "Ellipse"; ResetButtonsColor(); btnEllipse.BackColor = Color.LightBlue; }
        private void btnPencil_Click(object sender, EventArgs e) { _tool = "Pencil"; ResetButtonsColor(); btnPencil.BackColor = Color.LightBlue; }

        private void btnColor_Click(object sender, EventArgs e)
        {
            using var dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK) _color = dlg.Color;
        }

        private void numWidth_ValueChanged(object sender, EventArgs e)
        {
            _thickness = (int)numWidth.Value;
        }

        private void btnUndo_Click(object sender, EventArgs e) { DoUndo(); }

        private void btnClear_Click(object sender, EventArgs e)
        {
            SaveState();
            _drawing.Clear();
            _selectedShape = null;
            pictureBox.Invalidate();
        }

        // --- ФАЙЛЫ ---
        private void btnSave_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog { Filter = "JSON|*.json" };
            if (sfd.ShowDialog() == DialogResult.OK) _drawing.Save(sfd.FileName);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog { Filter = "JSON|*.json" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SaveState();
                _drawing.Load(ofd.FileName);
                _selectedShape = null;
                pictureBox.Invalidate();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog { Filter = "PNG Image|*.png" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using var bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
                using var g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                _drawing.Draw(g);
                bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        // --- ИСТОРИЯ ---
        private void SaveState()
        {
            var tempPath = Path.GetTempFileName();
            _drawing.Save(tempPath);
            _undoStack.Push(File.ReadAllText(tempPath));
            File.Delete(tempPath);
            _redoStack.Clear();
        }

        private void DoUndo()
        {
            if (_undoStack.Count == 0) return;
            var tempPath = Path.GetTempFileName();
            _drawing.Save(tempPath);
            _redoStack.Push(File.ReadAllText(tempPath));
            
            var prevState = _undoStack.Pop();
            File.WriteAllText(tempPath, prevState);
            _drawing.Load(tempPath);
            File.Delete(tempPath);
            _selectedShape = null;
            pictureBox.Invalidate();
        }

        private void DoRedo()
        {
            if (_redoStack.Count == 0) return;
            var tempPath = Path.GetTempFileName();
            _drawing.Save(tempPath);
            _undoStack.Push(File.ReadAllText(tempPath));
            
            var nextState = _redoStack.Pop();
            File.WriteAllText(tempPath, nextState);
            _drawing.Load(tempPath);
            File.Delete(tempPath);
            _selectedShape = null;
            pictureBox.Invalidate();
        }
    }
}
