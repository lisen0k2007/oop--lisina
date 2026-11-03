namespace PaintApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            topPanel = new Panel();
            btnExport = new Button();
            btnOpen = new Button();
            btnSave = new Button();
            btnClear = new Button();
            btnUndo = new Button();
            numWidth = new NumericUpDown();
            btnColor = new Button();
            btnPencil = new Button();
            btnEllipse = new Button();
            btnRect = new Button();
            btnLine = new Button();
            pictureBox = new PictureBox();
            topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.Controls.Add(btnExport);
            topPanel.Controls.Add(btnOpen);
            topPanel.Controls.Add(btnSave);
            topPanel.Controls.Add(btnClear);
            topPanel.Controls.Add(btnUndo);
            topPanel.Controls.Add(numWidth);
            topPanel.Controls.Add(btnColor);
            topPanel.Controls.Add(btnPencil);
            topPanel.Controls.Add(btnEllipse);
            topPanel.Controls.Add(btnRect);
            topPanel.Controls.Add(btnLine);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(1913, 200);
            topPanel.TabIndex = 0;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(1396, 132);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(150, 46);
            btnExport.TabIndex = 10;
            btnExport.Text = "Экспорт PNG";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // btnOpen
            // 
            btnOpen.Location = new Point(1613, 46);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(150, 46);
            btnOpen.TabIndex = 9;
            btnOpen.Text = "Открыть";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(1415, 37);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 46);
            btnSave.TabIndex = 8;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(288, 124);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(150, 46);
            btnClear.TabIndex = 7;
            btnClear.Text = "Очистить";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnUndo
            // 
            btnUndo.Location = new Point(61, 121);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(150, 46);
            btnUndo.TabIndex = 6;
            btnUndo.Text = "Отменить";
            btnUndo.UseVisualStyleBackColor = true;
            btnUndo.Click += btnUndo_Click;
            // 
            // numWidth
            // 
            numWidth.Location = new Point(1091, 34);
            numWidth.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numWidth.Name = "numWidth";
            numWidth.Size = new Size(240, 39);
            numWidth.TabIndex = 5;
            numWidth.Value = new decimal(new int[] { 2, 0, 0, 0 });
            numWidth.ValueChanged += numWidth_ValueChanged;
            // 
            // btnColor
            // 
            btnColor.Location = new Point(865, 32);
            btnColor.Name = "btnColor";
            btnColor.Size = new Size(150, 46);
            btnColor.TabIndex = 4;
            btnColor.Text = "Цвет";
            btnColor.UseVisualStyleBackColor = true;
            btnColor.Click += btnColor_Click;
            // 
            // btnPencil
            // 
            btnPencil.Location = new Point(670, 29);
            btnPencil.Name = "btnPencil";
            btnPencil.Size = new Size(150, 46);
            btnPencil.TabIndex = 3;
            btnPencil.Text = "Карандаш";
            btnPencil.UseVisualStyleBackColor = true;
            btnPencil.Click += btnPencil_Click;
            // 
            // btnEllipse
            // 
            btnEllipse.Location = new Point(475, 26);
            btnEllipse.Name = "btnEllipse";
            btnEllipse.Size = new Size(150, 46);
            btnEllipse.TabIndex = 2;
            btnEllipse.Text = "Эллипс";
            btnEllipse.UseVisualStyleBackColor = true;
            btnEllipse.Click += btnEllipse_Click;
            // 
            // btnRect
            // 
            btnRect.Location = new Point(215, 18);
            btnRect.Name = "btnRect";
            btnRect.Size = new Size(211, 46);
            btnRect.TabIndex = 1;
            btnRect.Text = "Прямоугольник";
            btnRect.UseVisualStyleBackColor = true;
            btnRect.Click += btnRect_Click;
            // 
            // btnLine
            // 
            btnLine.Location = new Point(31, 18);
            btnLine.Name = "btnLine";
            btnLine.Size = new Size(150, 46);
            btnLine.TabIndex = 0;
            btnLine.Text = "Линия";
            btnLine.UseVisualStyleBackColor = true;
            // 
            // pictureBox
            // 
            pictureBox.BackColor = Color.White;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Location = new Point(0, 200);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(1913, 828);
            pictureBox.TabIndex = 1;
            pictureBox.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1913, 1028);
            Controls.Add(pictureBox);
            Controls.Add(topPanel);
            Name = "MainForm";
            Text = "MainForm";
            
            topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel topPanel;
        private Button btnRect;
        private Button btnLine;
        private Button btnColor;
        private Button btnPencil;
        private Button btnEllipse;
        private Button btnClear;
        private Button btnUndo;
        private PictureBox pictureBox;
        private NumericUpDown numWidth;
        private Button btnOpen;
        private Button btnSave;
        private Button btnExport;
    }
}
