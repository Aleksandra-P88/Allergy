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
using System.Windows.Forms.DataVisualization.Charting;

namespace Program
{
    public partial class Form2 : Form
    {

        public static Form1 form1 = new Form1();
      
        Counturs counturs = new Counturs();

        Image<Bgr, byte> imgcopy;

        Image<Bgr, byte> imgcopy_2;

        Image<Bgr, byte> imgcopy_3;

        Image<Bgr, byte> imgcopy_4;

        Image<Bgr, byte> imgcopy_5;

        Image<Bgr, byte> imgcopy_6;

        Graphics d;
        Pen p;
        Matrix matrix = new Matrix();

        public Form2(Image img)
        {
            InitializeComponent();

            Bitmap bmpImage = new Bitmap(img);

        
        }


        public Form2()
        {
           

        }


        public double DEGTORAD(int deg)
        {
            return Math.PI * deg / 180;


        }

        public void GenerateCirlce(double px_max, double py_max, Image<Bgr, byte> imgInput, double radius, int degree)

        {
            for (int i = 0; i < degree; i++)
            {

                if (i == 1)
                {

                    PointF P1 = new PointF();
                    PointF P2 = new PointF();

                    P1.X = Convert.ToSingle(py_max + ((float)Math.Cos(DEGTORAD(360)) * radius));
                    P1.Y = Convert.ToSingle(px_max + ((float)Math.Sin(DEGTORAD(360)) * radius));
                    P2.X = Convert.ToSingle(py_max + ((float)Math.Cos(DEGTORAD(1)) * radius));
                    P2.Y = Convert.ToSingle(px_max + ((float)Math.Sin(DEGTORAD(1)) * radius));
                    d.DrawLine(p, P1, P2);


                }
                else
                {

                    PointF P1 = new PointF();
                    PointF P2 = new PointF();

                    P1.X = Convert.ToSingle(py_max + ((float)Math.Cos(DEGTORAD(i - 1)) * radius));
                    P1.Y = Convert.ToSingle(px_max + ((float)Math.Sin(DEGTORAD(i - 1)) * radius));
                    P2.X = Convert.ToSingle(py_max + ((float)Math.Cos(DEGTORAD(i)) * radius));
                    P2.Y = Convert.ToSingle(px_max + ((float)Math.Sin(DEGTORAD(i)) * radius));
                    d.DrawLine(p, P1, P2);


                }

            }

        }

    

        private void Form2_Load(object sender, EventArgs e)
        {

             imgcopy = new Image<Bgr, byte>(imageBox1.Image.Bitmap);

          

            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";




           



        }

        private void button1_Click(object sender, EventArgs e)
        {

            chart_1();



        }

        public void fillfunction(double Min_2, double Max_2, int area_2,string wyraz)
        {



            label1.Text = "Max Temperatura:" + Math.Round(Max_2, 2);
            label2.Text = wyraz + Math.Round(Min_2, 2);
            label3.Text = "Wysokość:" + Math.Round((Max_2 - Min_2), 2);

            label4.Text = "1/2 wysokości:" + Math.Round((Max_2 - Min_2) / 2, 2);

            label5.Text = "Pole: " + area_2;

            double pr = Math.Round(Math.Sqrt(area_2 / 3.14), 2);

            label6.Text = "Promień: " + pr;

            label7.Text = "Średnica: " + 2 * pr;


        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            imgcopy_2 = imgcopy.Copy();

            imageBox1.Image = imgcopy_2;
            Tuple<int, int, double, double, int, int, int> obi = matrix.maximin_2(imgcopy_2);

            fillfunction(obi.Item3, obi.Item4, obi.Item7, "Min Temperatura:");

            Brush aBrush = (Brush)Brushes.Blue;
            Graphics g = Graphics.FromImage(imgcopy_2.Bitmap);

            g.FillRectangle(aBrush, obi.Item6, obi.Item5, 1, 1);
          

            double radius = 0;
            radius = (Math.Round(Math.Sqrt(obi.Item7 / 3.14), 2));


            d = Graphics.FromImage(imgcopy_2.Bitmap);

            p = new Pen(Brushes.Blue, 0.4f);
            GenerateCirlce(obi.Item5, obi.Item6, imgcopy_2, radius, 360);

           

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            imgcopy_3 = imgcopy.Copy();


            imageBox1.Image = imgcopy_3;
            Tuple<double, int, double, int>obi_1 = matrix.med_ave(imgcopy_3);

            Tuple<int, int, double, double, int, int, int> obi = matrix.maximin_2(imgcopy_3);


            fillfunction(obi_1.Item3, obi.Item4, obi_1.Item4, "Średnia Temperatura:");


            Brush aBrush = (Brush)Brushes.Blue;
            Graphics g = Graphics.FromImage(imgcopy_3.Bitmap);

            g.FillRectangle(aBrush, obi.Item6, obi.Item5, 1, 1);

            double radius = 0;
            radius = (Math.Round(Math.Sqrt(obi_1.Item4 / 3.14), 2));


            d = Graphics.FromImage(imgcopy_3.Bitmap);

            p = new Pen(Brushes.Blue, 0.4f);
            GenerateCirlce(obi.Item5, obi.Item6, imgcopy_3, radius, 360);

            






        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            imgcopy_4 = imgcopy.Copy();
            imageBox1.Image = imgcopy_4;
            Tuple<double, int, double, int> obi_1 = matrix.med_ave(imgcopy_4);

            Tuple<int, int, double, double, int, int, int> obi = matrix.maximin_2(imgcopy_4);


            fillfunction(obi_1.Item1, obi.Item4, obi_1.Item2, "Mediana Temperatur:");


            Brush aBrush = (Brush)Brushes.Blue;
            Graphics g = Graphics.FromImage(imgcopy_4.Bitmap);

            g.FillRectangle(aBrush, obi.Item6, obi.Item5, 1, 1);

            double radius = 0;
            radius = (Math.Round(Math.Sqrt(obi_1.Item2 / 3.14), 2));


            d = Graphics.FromImage(imgcopy_4.Bitmap);

            p = new Pen(Brushes.Blue, 0.4f);
            GenerateCirlce(obi.Item5, obi.Item6, imgcopy_4, radius, 360);

           




        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

            imgcopy_5 = imgcopy.Copy();
            imageBox1.Image = imgcopy_5;

            Tuple<int> obi_2 = matrix.maximin(imgcopy_5);

            
            Tuple<int, int, double, double, int, int, int> obi = matrix.maximin_2(imgcopy_5);

            fillfunction(obi.Item3, obi.Item4, obi_2.Item1, "Min Temperatura:");

            Brush aBrush = (Brush)Brushes.Blue;
            Graphics g = Graphics.FromImage(imgcopy_5.Bitmap);

            g.FillRectangle(aBrush, obi.Item6, obi.Item5, 1, 1);

            double radius = 0;
            radius = (Math.Round(Math.Sqrt(obi_2.Item1 / 3.14), 2));


            d = Graphics.FromImage(imgcopy_5.Bitmap);

            p = new Pen(Brushes.Blue, 0.4f);
            GenerateCirlce(obi.Item5, obi.Item6, imgcopy_5, radius, 360);

           




        }

        public Tuple <string> ins(Image<Bgr,byte>imgcopy_7)
        { 
          
          

            Tuple<int, int, double, double, int, int, int> obi = matrix.maximin_2(imgcopy_7);


            double Max_2 = obi.Item4;
            double Min_2 = obi.Item3;

            Tuple<int, int, int> cho = matrix.choice(imgcopy_7);


            int iron_1 = cho.Item1;
            int szar_1 = cho.Item2;
            int rain_1 = cho.Item3;

            Tuple<int, int, double, double> obi_3_1 = form1.change();

            double Min_2_1 = ((obi_3_1.Item1 - obi_3_1.Item4) / obi_3_1.Item3);
            double Max_2_1 = ((obi_3_1.Item2 - obi_3_1.Item4) / obi_3_1.Item3);


            Tuple<double, double> obi_4 = counturs.contour_1(obi_3_1.Item1, obi_3_1.Item2, imgcopy_7, iron_1, szar_1, rain_1);



            double carea_2 = obi_4.Item1;

            double len_2 = obi_4.Item2;


           
            string inscript = "";

          

           





            if ((Max_2 >= Min_2) && carea_2 >= 10)
            {


                inscript ="Wykryta";

                






            }
            else if ((!(Max_2 >= Min_2)) || carea_2 <= 10)
            {



               inscript ="Nie wykryta";

              




            }


          return (Tuple.Create(inscript)); 
        }





        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

            imgcopy_6 = imgcopy.Copy();
            imageBox1.Image = imgcopy_6;

            Tuple<int, int, double, double, int, int, int> obi = matrix.maximin_2(imgcopy_6);


            double Max_2 = obi.Item4;
            double Min_2 = obi.Item3;

            Tuple<int, int, int> cho = matrix.choice(imgcopy_6);


            int iron_1 = cho.Item1;
            int szar_1 = cho.Item2;
            int rain_1 = cho.Item3;

            Tuple<int, int, double, double> obi_3_1 = form1.change();

            double Min_2_1 = ((obi_3_1.Item1 - obi_3_1.Item4) / obi_3_1.Item3);
            double Max_2_1 = ((obi_3_1.Item2 - obi_3_1.Item4) / obi_3_1.Item3);


            Tuple<double, double> obi_4 = counturs.contour_1(obi_3_1.Item1, obi_3_1.Item2, imgcopy_6, iron_1, szar_1, rain_1);


            counturs.contour_0(obi_3_1.Item1, obi_3_1.Item2, imgcopy_6, iron_1, szar_1, rain_1);


            Brush aBrush = (Brush)Brushes.Blue;
            Graphics g = Graphics.FromImage(imgcopy_6.Bitmap);

            g.FillRectangle(aBrush, obi.Item6, obi.Item5, 1, 1);

             d = Graphics.FromImage(imgcopy_6.Bitmap);

             p = new Pen(Brushes.Blue, 0.4f);


            double carea_2 = obi_4.Item1;

            double len_2 = obi_4.Item2;


            double Mal = 0;

            string inscript = "";

            if (!(carea_2 == 0 || len_2 == 0))
            {
                Mal = Math.Round(2 * Math.Sqrt(3.14 * carea_2) / len_2, 2);
            }
            else
            {
                Mal = 0;
            }

            label1.Text = "Wsp.Malinowskiej:" + Mal;

            label2.Text = "Pole: " + carea_2;

            double pr2 = Math.Round(Math.Sqrt(carea_2 / 3.14), 2);

            label3.Text = "Promień: " + pr2;

            label4.Text = "Średnica:" + 2 * pr2;

            label5.Text = "";

            label6.Text = "Status zmiany: ";





            if ((Max_2 >= Min_2) && carea_2 >= 10)
            {


                inscript = "Wykryta";

                label7.Text = inscript;






            }
            else if ((!(Max_2 >= Min_2)) || carea_2 <= 10)
            {



                inscript = "Nie wykryta";

                label7.Text = inscript;





            }




        }




        public void chart_1()
        {

            Tuple<int[], double[]> draw_2 = matrix.draw(imgcopy.Copy());

            int width = imgcopy.Copy().Width;


            chart1.ChartAreas[0].AxisY.Maximum = double.Parse(form1.textBox2.Text);
            chart1.ChartAreas[0].AxisY.Minimum = double.Parse(form1.textBox1.Text);


            for (int i = 0; i < width; i++)
            {




                   
              
                    chart1.Series[0].Points.AddXY(i, draw_2.Item2[i]);
                
            }


            button1.Enabled = false;

        }

       

        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {


            Point mousePoint = new Point(e.X, e.Y);

            chart1.ChartAreas[0].CursorX.Interval = 0;
            chart1.ChartAreas[0].CursorY.Interval = 0;

            chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, true);
            chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, true);

            label10.Text = "Pozycja X piksela:" + chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X).ToString();
            label11.Text = "Pozycja Y piksela:" + chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Y).ToString();

            HitTestResult result = chart1.HitTest(e.X, e.Y);

            if (result.PointIndex > -1 && result.ChartArea != null)
            {

                label12.Text = "Indeks piksela:" + result.Series.Points[result.PointIndex].XValue.ToString();
                label13.Text = "Temperatura piksela:" + result.Series.Points[result.PointIndex].YValues[0].ToString();


            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }




