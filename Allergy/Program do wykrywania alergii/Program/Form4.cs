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
using Emgu.CV.Util;
using Emgu.CV.UI;



namespace Program
{
    public partial class Form4 : Form
    {

       


        public static Form1 form1 = new Form1();

        Matrix matrix = new Matrix();

        Counturs contours = new Counturs();

        Image<Bgr, byte> img4_1 = form1.img_copy;

        Image<Bgr, byte> img4 = form1.img_copy_2;

        double[] con_area = new double[9];
        double[] Max_2 = new double[9];
        double[] Min_2 = new double[9];
        int[] pod_area = new int[9];
        int[] ava_area = new int[9];
        int[] med_area = new int[9];
        int[] pro_area = new int[9];
        string[] inscript = new string[9];


        Bitmap memoryImage;

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {


            label1.Text = "";

            label2.Text = "";

            label3.Text = "";

            label4.Text = "";

            label5.Text = "";

            label6.Text = "";

            label7.Text = "";



            label8.Text = "";

            label9.Text = "";

            label10.Text = "";

            label11.Text = "";

            label12.Text = "";

            label13.Text = "";

            label14.Text = "";


            label15.Text = "";

            label16.Text = "";

            label17.Text = "";

            label18.Text = "";

            label19.Text = "";

            label20.Text = "";

            label21.Text = "";



            label22.Text = "";

            label23.Text = "";

            label24.Text = "";

            label25.Text = "";

            label26.Text = "";

            label27.Text = "";

            label28.Text = "";



            label29.Text = "";

            label30.Text = "";

            label31.Text = "";

            label32.Text = "";

            label33.Text = "";

            label34.Text = "";

            label35.Text = "";


            label36.Text = "";

            label37.Text = "";

            label38.Text = "";

            label39.Text = "";

            label40.Text = "";

            label41.Text = "";

            label42.Text = "";


            label43.Text = "";

            label44.Text = "";

            label45.Text = "";

            label46.Text = "";

            label47.Text = "";

            label48.Text = "";

            label49.Text = "";


            label50.Text = "";

            label51.Text = "";

            label52.Text = "";

            label53.Text = "";

            label54.Text = "";

            label55.Text = "";

            label56.Text = "";


            label57.Text = "";

            label58.Text = "";

            label59.Text = "";

            label60.Text = "";

            label61.Text = "";

            label62.Text = "";

            label63.Text = "";




        }


        public void report (Image<Bgr, byte>Image,Image<Bgr, byte> Image_1)
         {

         
            // z konturami 
            List<Image<Bgr, byte>> pic_4_1 = form1.pic_2(Image_1);
            List<Image<Bgr, byte>> imageBoxList_1 = pic_4_1;

            
            // z bez konturów
            List<Image<Bgr, byte>> pic_4 = form1.pic(Image);
           
            List<Image<Bgr, byte>> imageBoxList_1_1 = pic_4;


            PictureBox[] pictureBoxs = new PictureBox[9];

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

            for (int i = 0; i < 9; i++)
            {


                Tuple<int, int, double, double, int, int, int> obi = matrix.maximin_2(imageBoxList_1_1[i]);
                pod_area[i] = obi.Item7;


                Max_2[i]= obi.Item4;
                Min_2[i] = obi.Item3;

                Tuple<double, int, double, int> obi_1 = matrix.med_ave(imageBoxList_1_1[i]);
                ava_area[i] = obi_1.Item4;


                Tuple<double, int, double, int> obi_2 = matrix.med_ave(imageBoxList_1_1[i]);
                med_area[i] = obi_2.Item2;


                Tuple<int> obi_3 = matrix.maximin(imageBoxList_1_1[i]);

                pro_area[i] = obi_3.Item1;


                Tuple<int, int, int> cho = matrix.choice(imageBoxList_1_1[i]);

                int[] iron_1 = new int[9];

                iron_1[i] = cho.Item1;

                int[] szar_1 = new int[9];

                szar_1[i] = cho.Item2;


                int[] rain_1 = new int[9];

                rain_1[i] = cho.Item3;

                Tuple<int, int, double, double> obi_3_1 = form1.change();


                double[] Min_2_1 = new double[9];

                Min_2_1[i] = ((obi_3_1.Item1 - obi_3_1.Item4) / obi_3_1.Item3);
               

               

                Tuple<double, double> obi_4 = contours.contour_1(obi_3_1.Item1, obi_3_1.Item2, imageBoxList_1_1[i], iron_1[i], szar_1[i], rain_1[i]);

                con_area[i] = obi_4.Item1;

                if ((Max_2[i] >= Min_2[i]) && con_area[i] >= 10)
                {


                    inscript[i] = "Wykryto";

                 






                }
                else if ((!(Max_2[i] >= Min_2[i])) || con_area[i] <= 10)
                {



                    inscript[i] = "Brak";

                   




                }


            }


            label1.Text = "Pole I: " + pro_area[0];

            label2.Text = "Pole II: " + con_area[0];

            label3.Text = "Pole III: " + med_area[0];

            label4.Text = "Pole IV: " + ava_area[0];

            label5.Text = "Pole V: " + pod_area[0];

            label6.Text = "Max.Temperatura: " + Max_2[0];

            label7.Text = "Status zmiany: " + inscript[0];



            label8.Text = "Pole I: " + pro_area[1];

            label9.Text = "Pole II: " + con_area[1];

            label10.Text = "Pole III: " + med_area[1];

            label11.Text = "Pole IV: " + ava_area[1];

            label12.Text = "Pole V: " + pod_area[1];

            label13.Text = "Max.Temperatura: " + Max_2[1];

            label14.Text = "Status zmiany: " + inscript[1];


            label15.Text = "Pole I: " + pro_area[2];

            label16.Text = "Pole II: " + con_area[2];

            label17.Text = "Pole III: " + med_area[2];

            label18.Text = "Pole IV: " + ava_area[2];

            label19.Text = "Pole V: " + pod_area[2];

            label20.Text = "Max.Temperatura: " + Max_2[2];

            label21.Text = "Status zmiany: " + inscript[2];



            label22.Text = "Pole I: " + pro_area[3];

            label23.Text = "Pole II: " + con_area[3];

            label24.Text = "Pole III: " + med_area[3];

            label25.Text = "Pole IV: " + ava_area[3];

            label26.Text = "Pole V: " + pod_area[3];

            label27.Text = "Max.Temperatura: " + Max_2[3];

            label28.Text = "Status zmiany: " + inscript[3];



            label29.Text = "Pole I: " + pro_area[4];

            label30.Text = "Pole II: " + con_area[4];

            label31.Text = "Pole III: " + med_area[4];

            label32.Text = "Pole IV: " + ava_area[4];

            label33.Text = "Pole V: " + pod_area[4];

            label34.Text = "Max.Temperatura: " + Max_2[4];

            label35.Text = "Status zmiany: " + inscript[4];


            label36.Text = "Pole I: " + pro_area[5];

            label37.Text = "Pole II: " + con_area[5];

            label38.Text = "Pole III: " + med_area[5];

            label39.Text = "Pole IV: " + ava_area[5];

            label40.Text = "Pole V: " + pod_area[5];

            label41.Text = "Max.Temperatura: " + Max_2[5];

            label42.Text = "Status zmiany: " + inscript[5];


            label43.Text = "Pole I: " + pro_area[6];

            label44.Text = "Pole II: " + con_area[6];

            label45.Text = "Pole III: " + med_area[6];

            label46.Text = "Pole IV: " + ava_area[6];

            label47.Text = "Pole V: " + pod_area[6];

            label48.Text = "Max.Temperatura: " + Max_2[6];

            label49.Text = "Status zmiany: " + inscript[6];


            label50.Text = "Pole I: " + pro_area[7];

            label51.Text = "Pole II: " + con_area[7];

            label52.Text = "Pole III: " + med_area[7];

            label53.Text = "Pole IV: " + ava_area[7];

            label54.Text = "Pole V: " + pod_area[7];

            label55.Text = "Max.Temperatura: " + Max_2[7];

            label56.Text = "Status zmiany: " + inscript[7];


            label57.Text = "Pole I: " + pro_area[8];

            label58.Text = "Pole II: " + con_area[8];

            label59.Text = "Pole III: " + med_area[8];

            label60.Text = "Pole IV: " + ava_area[8];

            label61.Text = "Pole V: " + pod_area[8];

            label62.Text = "Max.Temperatura: " + Max_2[8];

            label63.Text = "Status zmiany: " + inscript[8];





        }

        private void button1_Click(object sender, EventArgs e)
        {
            report(img4,img4_1);
        }



        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {


        }


        private void CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawImage(memoryImage, 0, 0);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            CaptureScreen();
            printDocument1.Print();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();

            form1.button1.Enabled = false;
            form1.button2.Enabled = false;

        }
    }
}
