using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Program
{
    class Counturs

    {
        public void contour_0(int min, int max, Image<Bgr, byte> imgInput, int iron_r, int szar_r, int rain_r)
        {


            if (iron_r == 1)
            {



                Image<Gray, byte> imgOut = imgInput.Convert<Gray, byte>().InRange(new Gray(min), new Gray(max));

                Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint();
                Mat hier = new Mat();

                CvInvoke.FindContours(imgOut, contours, hier, Emgu.CV.CvEnum.RetrType.Ccomp, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
                CvInvoke.DrawContours(imgInput, contours, -1, new MCvScalar(255, 0, 0), 1);





            }

            // kontur, pole i obwód dla palety barw Rain

            else if (rain_r == 1)
            {



                Image<Gray, byte> imgOut = imgInput.Convert<Gray, byte>().ThresholdBinaryInv(new Gray(min), new Gray(max));



                Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint();
                Mat hier = new Mat();

                CvInvoke.FindContours(imgOut, contours, hier, Emgu.CV.CvEnum.RetrType.Ccomp, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
                CvInvoke.DrawContours(imgInput, contours, -1, new MCvScalar(255, 0, 0), 1);

              
             


            }

            // kontur, pole i obwód dla skali szarości 
            else if (szar_r == 1)
            {




                Image<Gray, byte>[] channels = imgInput.Split();

                Image<Gray, byte> imgOut = channels[0];


                Image<Gray, byte> imgOut_1 = imgOut.InRange(new Gray(min), new Gray(max));




                Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint();
                Mat hier = new Mat();

                CvInvoke.FindContours(imgOut_1, contours, hier, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
                CvInvoke.DrawContours(imgInput, contours, -1, new MCvScalar(255, 0, 0), 1);




               


            }




        }




        public Tuple<double, double> contour_1(int min, int max, Image<Bgr, byte> imgInput, int iron_r, int szar_r, int rain_r)
        {

            double carea = 0;
            double len = 0;
            double pre_len = 0;
            double pre_carea = 0;

            // kontur, pole i obwód dla palety barw Iron

            if (iron_r == 1)
            {



                Image<Gray, byte> imgOut = imgInput.Convert<Gray, byte>().InRange(new Gray(min), new Gray(max));

                Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint();
                Mat hier = new Mat();

                CvInvoke.FindContours(imgOut, contours, hier, Emgu.CV.CvEnum.RetrType.Ccomp, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
              



                for (int i = 0; i < contours.Size; i++)
                {

                    pre_carea = CvInvoke.ContourArea(contours[i]);

                    carea = carea + pre_carea;

                    pre_len = CvInvoke.ArcLength(contours[i], true);

                    len = len + pre_len;



                }


            }

            // kontur, pole i obwód dla palety barw Rain

            else if (rain_r == 1)
            {



                Image<Gray, byte> imgOut = imgInput.Convert<Gray, byte>().ThresholdBinaryInv(new Gray(min), new Gray(max));



                Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint();
                Mat hier = new Mat();

                CvInvoke.FindContours(imgOut, contours, hier, Emgu.CV.CvEnum.RetrType.Ccomp, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
              

                for (int i = 0; i < contours.Size; i++)
                {



                    pre_carea = CvInvoke.ContourArea(contours[i]);

                    carea = carea + pre_carea;

                    pre_len = CvInvoke.ArcLength(contours[i], true);

                    len = len + pre_len;



                }


            }

            // kontur, pole i obwód dla skali szarości 
            else if (szar_r == 1)
            {




                Image<Gray, byte>[] channels = imgInput.Split();

                Image<Gray, byte> imgOut = channels[0];


                Image<Gray, byte> imgOut_1 = imgOut.InRange(new Gray(min), new Gray(max));




                Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint();
                Mat hier = new Mat();

                CvInvoke.FindContours(imgOut_1, contours, hier, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
               




                for (int i = 0; i < contours.Size; i++)
                {

                    pre_carea = CvInvoke.ContourArea(contours[i]);

                    carea = carea + pre_carea;

                    pre_len = CvInvoke.ArcLength(contours[i], true);

                    len = len + pre_len;


                }


            }


            return (Tuple.Create(carea, len));

        }




    }
}
