using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Windows.Forms;

namespace Program
{
    class Matrix
    {
        Image<Bgr, byte> imgInput;

        Image<Bgr, byte> imgIn;

        OpenFileDialog ofd = new OpenFileDialog();

        public static Form1 form1 = new Form1();





        public Matrix()
        {



        }


        //  Metoda wczytująca obrazy 

        public Tuple<int, int, Image<Bgr, byte>> open()

        {

            int width = 0;
            int height = 0;


           

            if (ofd.ShowDialog() == DialogResult.OK)
            {

              
                imgIn = new Image<Bgr, byte>(ofd.FileName);
                

                width = imgIn.Width;
                height = imgIn.Height;


            }
       


            return (Tuple.Create(width, height, imgIn));
        }


        // Wyodrebnienie składowych  B,G i R na potrzeby wykrywania jakiej palety barw są dane obrazy 
        public Tuple<double[,], double[,], double[,]> color_1(Image<Bgr, byte> imgInput)
        {


            int width = imgInput.Width;
            int height = imgInput.Height;
            int[,] imgInput2 = new int[height, width];
            double[] temp = new double[width * height];
            double[,] b = new double[height, width];
            double[,] g = new double[height, width];
            double[,] r = new double[height, width];
            Bgr color;

            for (int x = 0; x < width; x++)

            {
                for (int y = 0; y < height; y++)
                {

                    color = imgInput[y, x];
                    b[y, x] = color.Blue;
                    g[y, x] = color.Green;
                    r[y, x] = color.Red;



                }

            }


            return (Tuple.Create(b, g, r));
        }


        // Mechanizm wyznaczjący w jakiej skali jest dany obraz 
        public Tuple<int, int, int> choice(Image<Bgr, byte> imgInput)
        {

            int width_1 = imgInput.Width;
            int height_1 = imgInput.Height;
            double[,] b_1 = color_1(imgInput).Item1;
            double[,] g_1 = color_1(imgInput).Item2;
            double[,] r_1 = color_1(imgInput).Item3;
            int[,] imgInput2 = new int[height_1, width_1];
            int iron = 0;
            int rain = 0;
            int szar = 0;
            int iron_r = 0;
            int szar_r = 0;
            int rain_r = 0;


            for (int x = 0; x < width_1; x++)

            {
                for (int y = 0; y < height_1; y++)
                {




                    imgInput2[y, x] = Convert.ToInt32(0.299 * r_1[y, x] + 0.585 * g_1[y, x] + 0.114 * b_1[y, x]);


                    if ((r_1[y, x] == g_1[y, x]) && (b_1[y, x] == r_1[y, x]) && (g_1[y, x] == r_1[y, x]))
                    {
                        szar++;

                    }
                    else if (((r_1[y, x] >= 104 && r_1[y, x] <= 253) && (g_1[y, x] >= 210 && g_1[y, x] <= 225) && (b_1[y, x] >= 0 && b_1[y, x] <= 1)) || ((r_1[y, x] == 255 && (g_1[y, x] >= 76 && g_1[y, x] <= 179) && b_1[y, x] == 0)) || ((r_1[y, x] >= 0 && r_1[y, x] <= 251) && (g_1[y, x] >= 0 && g_1[y, x] <= 255) && (b_1[y, x] >= 4 && b_1[y, x] <= 255)))
                    {


                        rain++;

                    }
                    else if ((r_1[y, x] >= 247 && r_1[y, x] <= 255) && (g_1[y, x] >= 135 && g_1[y, x] <= 200) && (b_1[y, x] >= 0 && b_1[y, x] <= 5))
                    {

                        iron++;
                    }


                }


            }

            if (iron > szar && iron > rain)
            {
                iron_r = 1;

            }

            else if (szar > iron && szar > rain)
            {
                szar_r = 1;


            }
            else if (rain > iron && rain > szar)

            {

                rain_r = 1;



            }


            return (Tuple.Create(iron_r, szar_r, rain_r));
        }


        public Tuple<int[,], double[,], double[,], double[,], int[], double[]> close(Image<Bgr, byte> imgInput)

        {

            
            Tuple<int, int, double, double> obi_3_1 = form1.change();
            

            int width = imgInput.Width;
            int height = imgInput.Height;
            int[,] imgInput2 = new int[height, width];
            int[] number = new int[width * height];
            double[] temp = new double[width * height];
            double[,] b = new double[height, width];
            double[,] g = new double[height, width];
            double[,] r = new double[height, width];
            Bgr color;
            int c = 0;
            int rain = choice(imgInput).Item3;
            int iron = choice(imgInput).Item1;
            int szar = choice(imgInput).Item2;




            if (choice(imgInput).Item1 == 1)

            {


                for (int x = 0; x < width; x++)

                {
                    for (int y = 0; y < height; y++)
                    {

                        color = imgInput[y, x];
                        b[y, x] = color.Blue;
                        g[y, x] = color.Green;
                        r[y, x] = color.Red;




                        imgInput2[y, x] = Convert.ToInt32(0.299 * r[y, x] + 0.585 * g[y, x] + 0.114 * b[y, x]);




                        number[c] = imgInput2[y, x];


                        temp[c] = Math.Round(((number[c] - obi_3_1.Item4) / obi_3_1.Item3), 2);

                        c++;

                       

                    }

                }

            }
            else if (choice(imgInput).Item3 == 1)
            {


                for (int x = 0; x < width; x++)

                {
                    for (int y = 0; y < height; y++)
                    {


                        color = imgInput[y, x];
                        b[y, x] = color.Blue;
                        g[y, x] = color.Green;
                        r[y, x] = color.Red;


                        imgInput2[y, x] = Convert.ToInt32(1.035 * r[y, x] - 0.447 * g[y, x] + 0 * b[y, x]);


                        number[c] = imgInput2[y, x];

                        temp[c] = Math.Round(((number[c] - obi_3_1.Item4) / obi_3_1.Item3), 2);

                        

                        c++;

                    }

                }

            }
            else if (choice(imgInput).Item2 == 1)
            {


                for (int x = 0; x < width; x++)

                {
                    for (int y = 0; y < height; y++)
                    {


                        color = imgInput[y, x];
                        b[y, x] = color.Blue;
                        g[y, x] = color.Green;
                        r[y, x] = color.Red;




                        imgInput2[y, x] = Convert.ToInt32(b[y, x]);

                        number[c] = imgInput2[y, x];

                        temp[c] = Math.Round(((number[c] - obi_3_1.Item4) / obi_3_1.Item3), 2);

                       

                        c++;

                    }

                }

            }







               return (Tuple.Create(imgInput2, b, g, r, number, temp));
            }



        // Wyliczene pola dla metody bez progu

        public Tuple<int, int, double, double, int, int, int> maximin_2(Image<Bgr, byte> imgInput)
        {


            int width_1 = imgInput.Width;
            int height_1 = imgInput.Height;

            int[] number_1 = close(imgInput).Item5;
            double[] temp_1 = close(imgInput).Item6;
            int[,] imgInput2_1 = close(imgInput).Item1;

            int rain = choice(imgInput).Item3;
            int iron = choice(imgInput).Item1;
            int szar = choice(imgInput).Item2;



            int Min_1 = number_1[0];
            int Max_1 = number_1[0];

            for (int k = 0; k < width_1 * height_1; k++)
            {


                if (Min_1 > number_1[k])
                    Min_1 = number_1[k];
                else if (Max_1 < number_1[k])
                    Max_1 = number_1[k];

            }




            double Min2_1 = temp_1[0];
            double Max2_1 = temp_1[0];

            for (int k = 0; k < width_1 * height_1; k++)

            {


                if (Min2_1 > temp_1[k])
                    Min2_1 = temp_1[k];
                else if (Max2_1 < temp_1[k])
                    Max2_1 = temp_1[k];


            }


            int xc1 = 0;
            int yc1 = 0;


            for (int x = 0; x < width_1; x++)

            {


                for (int y = 0; y < height_1; y++)
                {

                    if (imgInput2_1[y, x] == Max_1)
                    {

                        xc1 = x;
                        yc1 = y;
                    }

                }


            }

            int area = 0;


            if (choice(imgInput).Item1 == 1)

            {


                for (int i = 0; i < width_1 * height_1; i++)
                {



                    if (temp_1[i] >= Math.Round(Min2_1 + (Max2_1 - Min2_1) / 2, 2))

                    {       

                        area++;

                    }



                }

            }
            else if (choice(imgInput).Item3 == 1)

            {

                for (int i = 0; i < width_1 * height_1; i++)
                {

                    if (temp_1[i] >= Math.Round(Min2_1 + (Max2_1 - Min2_1) / 2, 2))

                    {

                        area++;

                    }
                }


            }
            else if (choice(imgInput).Item2 == 1)


            {

                for (int i = 0; i < width_1 * height_1; i++)
                {


                    if (temp_1[i] >= Math.Round(Min2_1 + (Max2_1 - Min2_1) / 2, 2))

                    {

                        area++;

                    }


                }


            }





            return (Tuple.Create(Min_1, Max_1, Min2_1, Max2_1, xc1, yc1, area));
        }



        // Wyliczenie pola dla metody z progiem  

        public Tuple<int> maximin(Image<Bgr, byte> imgInput)
        {


            int width_1 = imgInput.Width;
            int height_1 = imgInput.Height;

            int[] number_1 = close(imgInput).Item5;
            double[] temp_1 = close(imgInput).Item6;
            int[,] imgInput2_1 = close(imgInput).Item1;

            int rain = choice(imgInput).Item3;
            int iron = choice(imgInput).Item1;
            int szar = choice(imgInput).Item2;


            double Min2 = temp_1[0];
            double Max2 = temp_1[0];

            for (int k = 0; k < width_1 * height_1; k++)

            {


                if (Min2 > temp_1[k])
                    Min2 = temp_1[k];
                else if (Max2 < temp_1[k])
                    Max2 = temp_1[k];


            }




            int area = 0;


            if (choice(imgInput).Item1 == 1)

            {


                for (int i = 0; i < width_1 * height_1; i++)
                {



                    if (temp_1[i] >= Math.Round(Min2 + (Max2 - Min2) / 2, 2) && number_1[i] <= 255 && number_1[i] >= 185)

                    {       //

                        area++;

                    }



                }

            }
            else if (choice(imgInput).Item3 == 1)

            {

                for (int i = 0; i < width_1 * height_1; i++)
                {

                    if (temp_1[i] >= Math.Round(Min2 + (Max2 - Min2) / 2, 2) && number_1[i] <= 255 && number_1[i] >= 175)

                    {

                        area++;

                    }
                }


            }
            else if (choice(imgInput).Item2 == 1)


            {

                for (int i = 0; i < width_1 * height_1; i++)
                {


                    if (temp_1[i] >= Math.Round(Min2 + (Max2 - Min2) / 2, 2) && number_1[i] <= 255 && number_1[i] >= 195)

                    {

                        area++;

                    }


                }


            }





            return (Tuple.Create(area));
        }



        // Wyliczenia pola - dla metody z medianą i średnią 

        public Tuple<double, int, double, int> med_ave(Image<Bgr, byte> imgInput)
        {


            int width_1 = imgInput.Width;
            int height_1 = imgInput.Height;

            int[] number_1 = close(imgInput).Item5;
            double[] temp_1 = close(imgInput).Item6;
            int[,] imgInput2_1 = close(imgInput).Item1;

            int rain = choice(imgInput).Item3;
            int iron = choice(imgInput).Item1;
            int szar = choice(imgInput).Item2;

            double Max2_2 = maximin_2(imgInput).Item4;


            int numberCount = temp_1.Count();
            int halfIndex = temp_1.Count() / 2;
            var sortedNumbers = temp_1.OrderBy(n => n);

            double median;

            if ((numberCount % 2) == 0)
            {
                median = ((sortedNumbers.ElementAt(halfIndex) +
                    sortedNumbers.ElementAt((halfIndex - 1))) / 2);
            }
            else
            {
                median = sortedNumbers.ElementAt(halfIndex);
            }



            double mean = Math.Round(temp_1.Average(), 2);


            int area_1 = 0;


            int area_2 = 0;


            if (choice(imgInput).Item1 == 1)

            {


                for (int i = 0; i < width_1 * height_1; i++)
                {



                    if (temp_1[i] >= Math.Round(median + (Max2_2 - median) / 2, 2))

                    {      

                        area_2++;

                    }



                    if (temp_1[i] >= Math.Round(mean + (Max2_2 - mean) / 2, 2))

                    {       

                        area_1++;

                    }






                }

            }
            else if (choice(imgInput).Item3 == 1)

            {

                for (int i = 0; i < width_1 * height_1; i++)
                {

                    if (temp_1[i] >= Math.Round(median + (Max2_2 - median) / 2, 2))

                    {

                        area_2++;

                    }

                    if (temp_1[i] >= Math.Round(mean + (Max2_2 - mean) / 2, 2))

                    {

                        area_1++;

                    }


                }


            }
            else if (choice(imgInput).Item2 == 1)


            {

                for (int i = 0; i < width_1 * height_1; i++)
                {


                    if (temp_1[i] >= Math.Round(median + (Max2_2 - median) / 2, 2))

                    {

                        area_2++;

                    }



                    if (temp_1[i] >= Math.Round(mean + (Max2_2 - mean) / 2, 2))

                    {

                        area_1++;

                    }




                }


            }

            return (Tuple.Create(median, area_2, mean, area_1));

        }


        // Stworzenie tabeli, w której ulokwane są dane pod potrzeby wykreślenia profili  

        public Tuple<int[], double[]> draw(Image<Bgr, byte> imgInput)

        {
           
            Tuple<int, int, double, double> obi_3_1 = form1.change();

            int width_1 = imgInput.Width;

            int yc1_1 = maximin_2(imgInput).Item6;
            int[,] imgInput2_1 = close(imgInput).Item1;

            int[] date = new int[width_1];
            double[] date2 = new double[width_1];


            for (int i = 0; i < width_1; i++)
            {

                date[i] = imgInput2_1[yc1_1, i];


             

                date2[i] = Math.Round(((date[i] - obi_3_1.Item4) / obi_3_1.Item3), 2);

               
            }



            return (Tuple.Create(date, date2));
        }







    }
}
