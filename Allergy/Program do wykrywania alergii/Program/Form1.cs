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
    public partial class Form1 : Form
    {
       
        public Image<Bgr, byte> img_copy;
        public Image<Bgr, byte> img_copy_2;
        public Image<Bgr, byte> img2;

        

        int iron_r_c = 0;
        int szar_r_c = 0;
        int rain_r_c = 0;

        Matrix matrix = new Matrix();

        Counturs contours = new Counturs();

        

        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            Matrix.form1 = this;
            Form2.form1 = this;
            Form4.form1 = this;
            Form5.form1 = this;

            button2.Enabled = false;
            button1.Enabled = false;


            label3.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";

            pomiarReferencyjnyToolStripMenuItem.Enabled = false;
            dopasowanieObrazuToolStripMenuItem.Enabled = false;
        }



        public Tuple <Image<Bgr, byte>,int,int,int> openpicture()

        {
           
       
            Tuple <int, int, Image<Bgr, byte>> open = matrix.open();


            int iron_r = 0;
            int szar_r = 0;
            int rain_r = 0;



            Image<Bgr, byte> img = open.Item3;

            if (img == null)
            {


                img = new Image<Bgr, byte>(Properties.Resources.tlo);
                pomiarReferencyjnyToolStripMenuItem.Enabled = false;
                dopasowanieObrazuToolStripMenuItem.Enabled = false;

            }
            else
            {

                img_copy_2 = img;

                img_copy = img.Copy();

                imageBox1.Image = img;


                Tuple<int, int, int> cho = matrix.choice(img);
                 iron_r = cho.Item1;
                 szar_r = cho.Item2;
                 rain_r = cho.Item3;


                if (iron_r == 1)
                {
                    label3.Text = "Skala Iron";

                }
                else if (szar_r == 1)
                {
                    label3.Text = "Skala Szarości";


                }
                else if (rain_r == 1)
                {
                    label3.Text = "Skala Rain";

                }




                iron_r_c = iron_r;
                szar_r_c = szar_r;
                rain_r_c = rain_r;


                pomiarReferencyjnyToolStripMenuItem.Enabled = true;
                dopasowanieObrazuToolStripMenuItem.Enabled = true;
            }





            textBox1.Text = "";
            textBox2.Text = "";

           




            return (Tuple.Create(img,iron_r, rain_r, szar_r));
        }




        private void wczytanieObrazuToolStripMenuItem_Click(object sender, EventArgs e)
        {
             Tuple<Image<Bgr, byte>,int,int,int> openpicture2 = openpicture();
             img2=openpicture2.Item1;
             fragmentpicture(img2);

            button1.Enabled = true;
            button2.Enabled = false;

           
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";
        }

        public void Main_Contour()
        {

          
            Image<Bgr, byte> img3 = img_copy;

            Tuple<int, int, double, double> chan = change();



            contours.contour_0(chan.Item1, chan.Item2, img3, iron_r_c, szar_r_c, rain_r_c);

            imageBox1.Image = img3;

            fragmentpicture(img3);

            

        }




        public List<Image<Bgr, byte>> pic(Image<Bgr, byte> Image)

        {
  
            Bitmap[,] bmps = new Bitmap[3, 3];
            Image<Bgr, Byte>[,] myImage1 = new Image<Bgr, Byte>[3, 3];
            Image<Gray, Byte>[,] myImage = new Image<Gray, Byte>[3, 3];

            Image img = Image.Convert<Bgr, byte>().ToBitmap();


            int widthThird = (int)((double)img.Width / 3.0 + 0.5);
            int heightThird = (int)((double)img.Height / 3.0 + 0.5);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {

                    bmps[i, j] = new Bitmap(widthThird, heightThird);
                    Graphics g = Graphics.FromImage(bmps[i, j]);
                    g.DrawImage(img, new Rectangle(0, 0, widthThird, heightThird), new Rectangle(j * widthThird, i * heightThird, widthThird, heightThird), GraphicsUnit.Pixel);
                    myImage1[i, j] = new Image<Bgr, Byte>(bmps[i, j]);

                   
                    g.Dispose();
                }
            }

            List<Image<Bgr, byte>> imageBoxList1 = new List<Image<Bgr, byte>>();
            imageBoxList1.Add(myImage1[0, 0]);
            imageBoxList1.Add(myImage1[0, 1]);
            imageBoxList1.Add(myImage1[0, 2]);
            imageBoxList1.Add(myImage1[1, 0]);
            imageBoxList1.Add(myImage1[1, 1]);
            imageBoxList1.Add(myImage1[1, 2]);
            imageBoxList1.Add(myImage1[2, 0]);
            imageBoxList1.Add(myImage1[2, 1]);
            imageBoxList1.Add(myImage1[2, 2]);





            return imageBoxList1;

        }


        public List<Image<Bgr, byte>> pic_2(Image<Bgr, byte> Image)

        {

            Bitmap[,] bmps = new Bitmap[3, 3];
            Image<Bgr, Byte>[,] myImage1 = new Image<Bgr, Byte>[3, 3];
            Image<Gray, Byte>[,] myImage = new Image<Gray, Byte>[3, 3];

            Image img = Image.Convert<Bgr, byte>().ToBitmap();


            int widthThird = (int)((double)img.Width / 3.0 + 0.5);
            int heightThird = (int)((double)img.Height / 3.0 + 0.5);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {

                    bmps[i, j] = new Bitmap(widthThird, heightThird);
                    Graphics g = Graphics.FromImage(bmps[i, j]);
                    g.DrawImage(img, new Rectangle(0, 0, widthThird, heightThird), new Rectangle(j * widthThird, i * heightThird, widthThird, heightThird), GraphicsUnit.Pixel);
                    myImage1[i, j] = new Image<Bgr, Byte>(bmps[i, j]);

                   
                    g.Dispose();
                }
            }

            List<Image<Bgr, byte>> imageBoxList1 = new List<Image<Bgr, byte>>();
            imageBoxList1.Add(myImage1[0, 0]);
            imageBoxList1.Add(myImage1[0, 1]);
            imageBoxList1.Add(myImage1[0, 2]);
            imageBoxList1.Add(myImage1[1, 0]);
            imageBoxList1.Add(myImage1[1, 1]);
            imageBoxList1.Add(myImage1[1, 2]);
            imageBoxList1.Add(myImage1[2, 0]);
            imageBoxList1.Add(myImage1[2, 1]);
            imageBoxList1.Add(myImage1[2, 2]);





            return imageBoxList1;

        }






        public void fragmentpicture(Image<Bgr, byte> Image)
        {

            PictureBox[] pictureBoxs = new PictureBox[9];
            List<Image<Bgr, byte>> imageBoxList_1 = pic(Image);

            pictureBoxs[0] = imageBox2;
            pictureBoxs[1] = imageBox3;
            pictureBoxs[2] = imageBox4;
            pictureBoxs[3] = imageBox5;
            pictureBoxs[4] = imageBox6;
            pictureBoxs[5] = imageBox7;
            pictureBoxs[6] = imageBox8;
            pictureBoxs[7] = imageBox9;
            pictureBoxs[8] = imageBox10;


           for (int i = 0; i < 9; i++)
           {
               pictureBoxs[i].Image = imageBoxList_1[i].Bitmap;


            }
            // 


        }
       
        private void imageBox2_Click(object sender, EventArgs e)
        {

            List<Image<Bgr, byte>> imageBoxList_1 = pic_2(img2);

            PictureBox pb = imageBox2 as PictureBox;
            
            Form2 fp = new Form2(pb.Image);
            fp.imageBox1.Image = imageBoxList_1[0];

            Tuple<string> ins_2 = fp.ins(imageBoxList_1[0]);
            label5.Text = ins_2.Item1;

            fp.Show();
          





        }
        private void imageBox3_Click(object sender, EventArgs e)
        {
            List<Image<Bgr, byte>> imageBoxList_1 = pic_2(img2);

            PictureBox pb = imageBox3 as PictureBox;

            Form2 fp = new Form2(pb.Image);

            
            fp.imageBox1.Image = imageBoxList_1[1];


            Tuple<string> ins_2 = fp.ins(imageBoxList_1[1]);
            label6.Text = ins_2.Item1;


            fp.Show();


        }
        private void imageBox4_Click(object sender, EventArgs e)
        {
            List<Image<Bgr, byte>> imageBoxList_1 = pic(img2);

            PictureBox pb = imageBox4 as PictureBox;
            
            Form2 fp = new Form2(pb.Image);
            fp.imageBox1.Image = imageBoxList_1[2];



            Tuple<string> ins_2 = fp.ins(imageBoxList_1[2]);
            label7.Text = ins_2.Item1;
            fp.Show();


        }

        private void imageBox5_Click(object sender, EventArgs e)
        {
            List<Image<Bgr, byte>> imageBoxList_1 = pic_2(img2);

            PictureBox pb = imageBox5 as PictureBox;
            
            Form2 fp = new Form2(pb.Image);
            fp.imageBox1.Image = imageBoxList_1[3];


            Tuple<string> ins_2 = fp.ins(imageBoxList_1[3]);
            label8.Text = ins_2.Item1;
            fp.Show();


        }
        private void imageBox6_Click(object sender, EventArgs e)
        {
            List<Image<Bgr, byte>> imageBoxList_1 = pic_2(img2);
            PictureBox pb = imageBox6 as PictureBox;

            Form2 fp = new Form2(pb.Image);
            fp.imageBox1.Image = imageBoxList_1[4];


            Tuple<string> ins_2 = fp.ins(imageBoxList_1[4]);
            label9.Text = ins_2.Item1;
            fp.Show();


        }
        private void imageBox7_Click(object sender, EventArgs e)
        {
            List<Image<Bgr, byte>> imageBoxList_1 = pic_2(img2);

            PictureBox pb = imageBox7 as PictureBox;

            Form2 fp = new Form2(pb.Image);
            fp.imageBox1.Image = imageBoxList_1[5];


            Tuple<string> ins_2 = fp.ins(imageBoxList_1[5]);
            label10.Text = ins_2.Item1;
            fp.Show();


        }
        private void imageBox8_Click(object sender, EventArgs e)
        {
            List<Image<Bgr, byte>> imageBoxList_1 = pic_2(img2);
            PictureBox pb = imageBox8 as PictureBox;

            Form2 fp = new Form2(pb.Image);
            fp.imageBox1.Image = imageBoxList_1[6];


            Tuple<string> ins_2 = fp.ins(imageBoxList_1[6]);
            label11.Text = ins_2.Item1;
            fp.Show();


        }
        private void imageBox9_Click(object sender, EventArgs e)
        {
            List<Image<Bgr, byte>> imageBoxList_1 = pic_2(img2);
            PictureBox pb = imageBox9 as PictureBox;

            Form2 fp = new Form2(pb.Image);
            fp.imageBox1.Image = imageBoxList_1[7];

            Tuple<string> ins_2 = fp.ins(imageBoxList_1[7]);
            label12.Text = ins_2.Item1;
            fp.Show();


        }
        private void imageBox10_Click(object sender, EventArgs e)
        {
            List<Image<Bgr, byte>> imageBoxList_1 = pic_2(img2);
            PictureBox pb = imageBox10 as PictureBox;

            Form2 fp = new Form2(pb.Image);
            fp.imageBox1.Image = imageBoxList_1[8];


            Tuple<string> ins_2 = fp.ins(imageBoxList_1[8]);
            label13.Text = ins_2.Item1;
            fp.Show();


        }

      

        private void button1_Click(object sender, EventArgs e)
        {


             Form3 form3 = new Form3(this);
             form3.ShowDialog();
             button2.Enabled = true;



        }

        public Tuple<int, int, double, double> change()
        {

           
            double i = 0;
            double j = 0;
            int k = 0;
            int l = 0;
            double a = 0;
            double b = 0;

            i = double.Parse(textBox1.Text);
            j = double.Parse(textBox2.Text);


            b = Math.Round(-255 / (j - i) * i, 2);
            a = Math.Round(255 / (j - i), 2);

            double hr = 0;
            double r = Math.Round(j - i, 2);



            if (iron_r_c == 1)
            {
                hr = r * 0.72;
            }
            else if (rain_r_c == 1)
            {
                hr = r * 0.73;
            }
            else if (szar_r_c == 1)
            {
                hr = r * 0.76;
            }


          

            k = Convert.ToInt32((a * (hr + i) + b));
            l = Convert.ToInt32((a * j) + b);


      

            return (Tuple.Create(k, l, a, b));
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form4 form4 = new Form4();
            form4.Show();



        }

        private void pomiarReferencyjnyToolStripMenuItem_Click(object sender, EventArgs e)
        {


            Form5 form5 = new Form5();
            form5.Show();



        }

        private void dopasowanieObrazuToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form6 form6 = new Form6();
            form6.Show();




        }
    }
    }
