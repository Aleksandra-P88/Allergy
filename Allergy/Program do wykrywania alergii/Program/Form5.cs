using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;




namespace Program
{
    public partial class Form5 : Form
    {


        Rectangle rect;
        Point StartLocation;
        Point EndLocation;
        bool IsMouseDown = false;

        public static Form1 form1 = new Form1();


        Image<Bgr, byte> CC_2 = form1.img_copy_2;



        public Form5()
        {
            InitializeComponent();



        }


        private void Form5_Load(object sender, EventArgs e)
        {




            pictureBox1.Image = CC_2.Bitmap;


            label4.Text = "";
            label5.Text = "";
            label6.Text = "";

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            StartLocation = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == true)
            {

                EndLocation = e.Location;
                pictureBox1.Invalidate();
            }

        }

        private Rectangle GetRectangle()
        {

            rect = new Rectangle();
            rect.X = Math.Min(StartLocation.X, EndLocation.X);
            rect.Y = Math.Min(StartLocation.Y, EndLocation.Y);
            rect.Width = Math.Abs(StartLocation.X - EndLocation.X);
            rect.Height = Math.Abs(StartLocation.Y - EndLocation.Y);






            return rect;


        }




        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            if (rect != null)
            {

                e.Graphics.DrawEllipse(Pens.Blue, GetRectangle());


            }




        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

            if (IsMouseDown == true)
            {
                EndLocation = e.Location;
                IsMouseDown = false;
                if (rect != null)
                {

                    CC_2.ROI = rect;
                    Image<Bgr, byte> temp = CC_2.CopyBlank();
                    CC_2.CopyTo(temp);
                    CC_2.ROI = Rectangle.Empty;
                    imageBox1.Image = temp;



                    int width = temp.Width;
                    int height = temp.Height;
                    int area_2 = width * height;



                    label4.Text = "Pole :" + area_2;

                    double pr = Math.Round(Math.Sqrt(area_2 / 3.14), 2);

                    label5.Text = "Promień: " + pr;

                    label6.Text = "Średnica: " + 2 * pr;



                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Close();


        }
    }
}
