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

        // Convert a color from encoded to linear sRGB
        static double[] SRGBToLinear3(double[] pixel)
        {
            return new double[] { SRGBToLinear(pixel[0]), SRGBToLinear(pixel[1]), SRGBToLinear(pixel[2]) };

        }


        // Convert a color from linear to encoded sRGB
        static double[] SRGBFromLinear3(double[] pixel)
        {
            return new double[] { SRGBFromLinear(pixel[0]), SRGBFromLinear(pixel[1]), SRGBFromLinear(pixel[2]) };

        }
        // hope its work now 











        // Convert encoded sRGB to luminance factor
        static double LuminanceSRGB(double[] color) //not sure if color is a array
        {
            // Convert to linear sRGB
            double[] c = SRGBToLinear(color);
            // Find the linear sRGB luminance factor
            return c[0] * 0.2126 + c[1] * 0.7152 + c[2] * 0.0722;
        }

        // Convert encoded sRGB (with D50/2 white point)
        // to luminance factor
        static double LuminanceSRGBD50(double color)
        {
            // Convert to linear sRGB
            double[] c = SRGBToLinear(color);
            // Find the linear sRGB luminance factor
            return c[0] * 0.2225 + c[1] * 0.7169 + c[2] * 0.0606;
        }

















        //Color Operations
        //Operations that can be done on colors.Note that for best results, these operations need to be carried out with linear RGB colors rather than encoded RGB colors, unless noted otherwise.




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


            SRGBToLinear(c); //what i put in ?
            SRGBToLinear3(pixel); // i think i put _imageBuffer in.
            LuminanceSRGB(color); //i put in the RGB who was transform into sRGB linear







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
