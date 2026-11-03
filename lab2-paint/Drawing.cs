using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaintApp
{
    public class Drawing
    {
        public List<Shape> Shapes { get; set; } = new List<Shape>();

        public void Add(Shape shape) => Shapes.Add(shape);
        public void Undo() { if (Shapes.Count > 0) Shapes.RemoveAt(Shapes.Count - 1); }
        public void Clear() => Shapes.Clear();

        public void Draw(Graphics g)
        {
            foreach (var shape in Shapes)
                shape.Draw(g);
        }

        // --- СОХРАНЕНИЕ И ЗАГРУЗКА ---
        public void Save(string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            // Сохраняем тип фигуры, чтобы знать, что восстанавливать
            var list = new List<object>();
            foreach (var s in Shapes)
            {
                list.Add(new
                {
                    Type = s.GetType().Name,
                    s.Start,
                    s.End,
                    s.ColorArgb,
                    s.Width,
                    Points = (s as PencilShape)?.Points
                });
            }
            File.WriteAllText(path, JsonSerializer.Serialize(list, options));
        }

        public void Load(string path)
        {
            if (!File.Exists(path)) return;
            var json = File.ReadAllText(path);
            var doc = JsonDocument.Parse(json);
            Shapes.Clear();

            foreach (var elem in doc.RootElement.EnumerateArray())
            {
                string type = elem.GetProperty("Type").GetString();
                var start = elem.GetProperty("Start").Deserialize<Point>();
                var end = elem.GetProperty("End").Deserialize<Point>();
                var color = Color.FromArgb(elem.GetProperty("ColorArgb").GetInt32());
                var width = elem.GetProperty("Width").GetInt32();

                Shape shape = type switch
                {
                    nameof(LineShape) => new LineShape(start, color, width),
                    nameof(RectShape) => new RectShape(start, color, width),
                    nameof(EllipseShape) => new EllipseShape(start, color, width),
                    nameof(PencilShape) => new PencilShape(start, color, width),
                    _ => null
                };

                if (shape != null)
                {
                    shape.End = end;
                    if (shape is PencilShape pencil)
                    {
                        var pts = elem.GetProperty("Points").Deserialize<List<Point>>();
                        if (pts != null) pencil.Points = pts;
                    }
                    Shapes.Add(shape);
                }
            }
        }
    }
}
