using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization;

namespace PaintApp
{
    public abstract class Shape
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        [JsonIgnore]
        public Color Color { get; set; }

        public int ColorArgb
        {
            get => Color.ToArgb();
            set => Color = Color.FromArgb(value);
        }

        public int Width { get; set; }

        protected Shape() { }

        protected Shape(Point start, Color color, int width)
        {
            Start = End = start;
            Color = color;
            Width = width;
        }

        public virtual void MoveEnd(Point p) => End = p;

        protected Rectangle Bounds() => new Rectangle(
            Math.Min(Start.X, End.X),
            Math.Min(Start.Y, End.Y),
            Math.Abs(End.X - Start.X),
            Math.Abs(End.Y - Start.Y));

        public abstract void Draw(Graphics g);

        public abstract bool Contains(Point p);

        public virtual void Move(int dx, int dy)
        {
            Start = new Point(Start.X + dx, Start.Y + dy);
            End = new Point(End.X + dx, End.Y + dy);
        }
    }

    public class LineShape : Shape
    {
        public LineShape() { }
        public LineShape(Point start, Color color, int width) : base(start, color, width) { }

        public override void Draw(Graphics g)
        {
            using var pen = new Pen(Color, Width);
            g.DrawLine(pen, Start, End);
        }

        public override bool Contains(Point p)
        {
            var d1 = Math.Sqrt(Math.Pow(p.X - Start.X, 2) + Math.Pow(p.Y - Start.Y, 2));
            var d2 = Math.Sqrt(Math.Pow(p.X - End.X, 2) + Math.Pow(p.Y - End.Y, 2));
            var len = Math.Sqrt(Math.Pow(End.X - Start.X, 2) + Math.Pow(End.Y - Start.Y, 2));
            return Math.Abs((d1 + d2) - len) < 5 + Width;
        }
    }

    public class RectShape : Shape
    {
        public RectShape() { }
        public RectShape(Point start, Color color, int width) : base(start, color, width) { }

        public override void Draw(Graphics g)
        {
            using var pen = new Pen(Color, Width);
            g.DrawRectangle(pen, Bounds());
        }

        public override bool Contains(Point p)
        {
            var b = Bounds();
            b.Inflate(Width, Width);
            return b.Contains(p);
        }
    }

    public class EllipseShape : Shape
    {
        public EllipseShape() { }
        public EllipseShape(Point start, Color color, int width) : base(start, color, width) { }

        public override void Draw(Graphics g)
        {
            using var pen = new Pen(Color, Width);
            g.DrawEllipse(pen, Bounds());
        }

        public override bool Contains(Point p)
        {
            var b = Bounds();
            b.Inflate(Width, Width);
            return b.Contains(p);
        }
    }

    public class PencilShape : Shape
    {
        public List<Point> Points { get; set; } = new List<Point>();

        public PencilShape() { }
        public PencilShape(Point start, Color color, int width) : base(start, color, width)
        {
            Points.Add(start);
        }

        public override void MoveEnd(Point p)
        {
            End = p;
            Points.Add(p);
        }

        public override void Draw(Graphics g)
        {
            if (Points.Count < 2) return;
            using var pen = new Pen(Color, Width);
            g.DrawLines(pen, Points.ToArray());
        }

        public override void Move(int dx, int dy)
        {
            base.Move(dx, dy);
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i] = new Point(Points[i].X + dx, Points[i].Y + dy);
            }
        }

        public override bool Contains(Point p)
        {
            foreach (var pt in Points)
            {
                if (Math.Abs(pt.X - p.X) < 5 + Width && Math.Abs(pt.Y - p.Y) < 5 + Width)
                    return true;
            }
            return false;
        }
    }
}
