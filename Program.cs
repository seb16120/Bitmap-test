using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace C_sharp_learning_console
{
   
    


    class Program
    
    {


        private static readonly byte[] _imageBuffer = new byte[102400]; //(4*256)*100

        static void PlotPixel(int x, int y, byte redValue, byte greenValue, byte blueValue)
        {
            int offset = ((256 * 4) * y) + (x * 4);
            _imageBuffer[offset] = blueValue;
            _imageBuffer[offset + 1] = greenValue;
            _imageBuffer[offset + 2] = redValue;
            // Fixed alpha value (No transparency)
            _imageBuffer[offset + 3] = 255;
        }






        // Convert a color component from encoded to linear sRGB
        // NOTE: This is not gamma decoding; it's similar to, but
        // not exactly, c^2.2.  This function was designed "to
        // allow for invertability in integer math", according to
        // the sRGB proposal.
        static  double SRGBToLinear(double c)
        {
            // NOTE: Threshold here would more properly be
            // 12.92 * 0.0031308 = 0.040449936, but 0.04045
            // is what the IEC standard uses
            if (c <= 0.04045)
            {
                return c / 12.92;
            }
            else {
                return Math.Pow((0.055 + c) / 1.055, 2.4);
                 }

        }

        // Convert a color component from linear to encoded sRGB
        // NOTE: This is not gamma encoding; it's similar to, but
        // not exactly, c^(1/2.2).
        static double SRGBFromLinear(double c)
        {
            if (c <= 0.0031308)
            {
                return 12.92 * c;
            }
            else
            {
                return Math.Pow(c, 1.0 / 2.4) * 1.055 - 0.055;
            }

        }
        //byte[] _imageBuffer = new byte[102400]
        // Convert a color from encoded to linear sRGB
        double[] SRGBToLinear3(double[] pixel)
        {
            return new double[] { SRGBToLinear(pixel[0]), SRGBToLinear(pixel[1]), SRGBToLinear(pixel[2])};

        }

        // Convert a color from linear to encoded sRGB (not use)
        static List<double> TTTSRGBFromLinear3(List<double> pixel)
        {
            return new List<double>() { SRGBFromLinear(pixel[0]), SRGBFromLinear(pixel[1]), SRGBFromLinear(pixel[2]) };

        }
        // ok but its a arrayList i need but i keep the example



        // Convert a color from linear to encoded sRGB
        static double[] SRGBFromLinear3(double[] pixel)
        {
            return new double[] { SRGBFromLinear(pixel[0]), SRGBFromLinear(pixel[1]), SRGBFromLinear(pixel[2]) };

        }
        // hope its work now 






        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>




        static void Main(string[] args)
        {
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 256; x++)
                {
                    byte val = (byte)x;
                    byte val2 = (byte)((byte)x / (byte)2);
                    byte val3 = (byte)((byte)x / (byte)3);
                   
                    PlotPixel(x, y, val, val2, val3);
                }
            }

            unsafe
            {
                fixed (byte* ptr = _imageBuffer)
                {
                    
                    using ( Bitmap image = new Bitmap(256, 100, 256 * 4, PixelFormat.Format32bppRgb, new IntPtr(ptr)) )
                    {
                        image.Save(@"greyscale3.png");
                    }
                }
            }


            Console.ReadKey();

        }
    }
}
