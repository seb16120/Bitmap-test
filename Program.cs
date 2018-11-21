﻿using System;
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
        List<double> SRGBToLinear3(List<double> c)
        {
            return new List<double>() { SRGBToLinear(c[0]), SRGBToLinear(c[1]), SRGBToLinear(c[2])};
        }

        // Convert a color from linear to encoded sRGB
        static  List<double>   SRGBFromLinear3(List<double> c)
        {
            return new List<double>() { SRGBFromLinear(c[0]), SRGBFromLinear(c[1]), SRGBFromLinear(c[2]) };

        }






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