using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSize();
        }

        private class ArrayPoints
        {
            private int idx;
            private Point[] points;

            public ArrayPoints(int size)
            {
                if (size <= 0)
                    points = new Point[2];
                else
                    points = new Point[size];
            }

            public void SetPoint(int x, int y)
            {
                if (idx >= points.Length)
                    idx = 0;

                points[idx] = new Point(x, y);
                ++idx;
            }

            public void ResetPoints()
            {
                idx = 0;
            }

            public int NumPoints
            {
                get => idx;
            }

            public Point[] Points
            {
                get => points;
            }
        }

        ArrayPoints arrayPoints = new ArrayPoints(2);
        private bool isPressed = false;

        Bitmap bitmap;
        Graphics graphics;

        Pen pen = new Pen(Color.Black, 3f);

        private void SetSize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            bitmap = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(bitmap);

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
            arrayPoints.ResetPoints();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isPressed)
                return;

            arrayPoints.SetPoint(e.X, e.Y);
            if (arrayPoints.NumPoints >= 2)
            {
                graphics.DrawLines(pen, arrayPoints.Points);
                pictureBox1.Image = bitmap;
                arrayPoints.SetPoint(e.X, e.Y);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = bitmap;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG(*.JPG)|*.jpg";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox1.Image != null)
                    pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }
    }
}
