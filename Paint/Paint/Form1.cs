using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace Paint
{

    public partial class Paint : Form
    {
        Point start, current;
        Shape currentShape = Shape.Free;
        Pen pen = new Pen(Color.Black, 2);
        Graphics g;
        Bitmap bmp;
        GraphicsPath gp = new GraphicsPath();
        enum Shape { Free, Line, Elipse, Rectangle, Triangle, Pipette, Fill};


        public Paint()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(pictureBox1.Image);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            start = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                switch (currentShape)
                {
                    case Shape.Free:
                        current = e.Location;
                        g.DrawLine(pen, start, current);
                        start = current;
                        break;
                    case Shape.Line:
                        current = e.Location;
                        gp.Reset();
                        gp.AddLine(start, current);
                        break;
                    case Shape.Rectangle:
                        current = e.Location;
                        gp.Reset();
                        if (current.X - start.X > 0 && current.Y - start.Y > 0) 
                            gp.AddRectangle(new Rectangle(start.X, start.Y, current.X - start.X, current.Y - start.Y));
                        if (current.X - start.X < 0 && current.Y - start.Y > 0)
                            gp.AddRectangle(new Rectangle(current.X, start.Y , start.X - current.X, current.Y - start.Y));
                        if (current.X - start.X < 0 && current.Y - start.Y < 0)
                            gp.AddRectangle(new Rectangle(current.X, current.Y, start.X - current.X, start.Y - current.Y));
                        if (current.X - start.X > 0 && current.Y - start.Y < 0)
                            gp.AddRectangle(new Rectangle(start.X, current.Y, current.X - start.X, start.Y - current.Y));
                        break;

                    case Shape.Triangle:
                        current = e.Location;
                        gp.Reset();
                        Point[] k = new Point[3];
                        if (current.Y > start.Y)
                        {
                            k[0] = new Point((current.X - start.X)/2 + start.X, start.Y);
                            k[1] = new Point(start.X, current.Y);
                            k[2] = new Point(current.X, current.Y);
                        }
                        
                        if (current.Y < start.Y)
                        { 
                            k[0] = new Point(current.X, start.Y);
                            k[1] = new Point(start.X, start.Y);
                            k[2] = new Point((current.X - start.X)/2 + start.X, current.Y);
                        }   
                        gp.AddPolygon(k);
                        break;
                    case Shape.Elipse:
                        current = e.Location;
                        gp.Reset();
                        gp.AddEllipse(new Rectangle(start.X, start.Y, current.X - start.X, current.Y - start.Y));
                        break;
                }
            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            g.DrawPath(pen, gp);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentShape = Shape.Free;
            pictureBox1.Cursor = Cursors.PanSE;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentShape = Shape.Line;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawPath(pen, gp);
        }
            
        private void button3_Click(object sender, EventArgs e)
        {
            currentShape = Shape.Triangle;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            currentShape = Shape.Rectangle;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            currentShape = Shape.Elipse;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK) 
            {
                pen.Color = colorDialog1.Color;
                currentColor.BackColor = colorDialog1.Color;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = (float)numericUpDown1.Value;
        }

        private void currentColor_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            currentShape = Shape.Pipette;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            currentShape = Shape.Fill;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                fs.Close();
            }
        }
           private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "D:\\" ;
            openFileDialog1.Filter = "Peg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif" ;
            openFileDialog1.Title = "Open Image File";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)openFileDialog1.OpenFile();
                fs.Close();
            }
        }
    }
}
