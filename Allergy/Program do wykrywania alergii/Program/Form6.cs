using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.Util;


namespace Program
{
    public partial class Form6 : Form
    {

        Matrix matrix = new Matrix();

        Image<Bgr, byte> CC;

        Image<Bgr, byte> temp2;

        Rectangle rect;

        Point StartLocation;
        Point EndLocation;
        bool IsMouseDown = false;

        

        public Form6()
        {
            InitializeComponent();
        }



        private void Form6_Load(object sender, EventArgs e)
        {

            Tuple<int, int, Image<Bgr, byte>> kul = matrix.open();

            CC = kul.Item3;

            if(CC==null)
            {
                CC= new Image<Bgr, byte>(Properties.Resources.tlo); 
                this.Close();

            }



            pictureBox1.Image = CC.Bitmap;


            label2.Text = "";
            label3.Text = "";

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






        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == true)
            {
                EndLocation = e.Location;
                IsMouseDown = false;
                if (rect != null)
                {

                    CC.ROI = rect;
                    Image<Bgr, byte> temp = CC.CopyBlank();
                    CC.CopyTo(temp);
                    CC.ROI = Rectangle.Empty;
                   

                    temp2 = temp.Copy();

                    int width = temp.Width;
                    int height = temp.Height;

                    label2.Text = "Wysokość: " + height;
                    label3.Text = "Szerokość: " + width;


                }


            }


        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (rect != null)
            {

                e.Graphics.DrawRectangle(Pens.Blue, GetRectangle());


            }

        }

        public void save()
        {
          
            temp2.Save(textBox1.Text);

        }



        private void button1_Click(object sender, EventArgs e)
        {
            save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
