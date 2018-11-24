using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;


namespace C_sharp_learning_console
{

    // i want create a image which i guess is in RGB encoded but i need to be in sRGB linear for 
    //modifing some set up so i use a conveter. After this i try to get the luminance factor.
    // After i try to generate random image by 3 method : encoded rgb / linear s-RGB / #XXXXXX

    //Color Operations
    //Operations that can be done on colors.Note that for best results, these operations need to be carried out with 
    //linear RGB colors rather than encoded RGB colors, unless noted otherwise.


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






        // Converts 0-1 format to N/N/N format as an integer.
        static double ToNNN(byte[] rgb, double scale) // i suppose scale is a double
        {
            double sm1 = scale - 1;
            return Math.Round(rgb[2] * sm1) * scale * scale + Math.Round(rgb[1] * sm1) * scale + Math.Round(rgb[0] * sm1);
        }


        // Converts N/N/N integer format to 0-1 format (where RGB color integers are packed red/green/blue, in that order from lowest to highest bits):
        static double[] FromNNN(double rgb, double scale)
        {


            // PROBLEM NUMBER 1
            // The Math class in .Net does not contain a structure/proerty or function called "Rem", I'm not sure
            // what it is your trying to do here, but "Rem" is not any kind of function I've heard of.  If it's to
            // do with finding the remainder of a formula, then I believe it's the % operator you want.
            // this may help : https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/remainder-operator …







            double sm1 = scale - 1;
            double r = rgb % scale;
            double g = Math.Floor(rgb / scale) % scale;
            double b = Math.Floor(rgb / (scale * scale) % scale);
            return new double[] { r / sm1, g / sm1, b / sm1 };


            
        }


        static double[] From888(double rgb)
        {
            return  FromNNN(rgb, 256);
        }

        static double To888(byte[] rgb)
        {
            return ToNNN(rgb, 256);
        }

        // To generate a random string of characters: (for create a random #XXXXXX Color)
        //
        // Generate a list of the letters, digits, and/or other characters the string can have. Examples are given later in this section.
        //
        // Build a new string whose characters are chosen from that character list. The pseudocode below demonstrates this by creating a list, 
        // rather than a string, where the random characters will be held.
        // It also takes the number of characters as a parameter named size. 
        // (How to convert this list to a text string depends on the programming language and is outside the scope of this page.)
        //
        static ArrayList RandomString(int stringSize = 6)
        {


            int i = 0;
            
            ArrayList characterList = new ArrayList();
            characterList.Add("A");
            characterList.Add("B");
            characterList.Add("C");
            characterList.Add("D");
            characterList.Add("E");
            characterList.Add("F");
            characterList.Add("G");
            characterList.Add("H");
            characterList.Add("I");
            characterList.Add("J");
            characterList.Add("K");
            characterList.Add("L");
            characterList.Add("M");
            characterList.Add("N");
            characterList.Add("O");
            characterList.Add("P");
            characterList.Add("Q");
            characterList.Add("R");
            characterList.Add("S");
            characterList.Add("T");
            characterList.Add("U");
            characterList.Add("V");
            characterList.Add("W");
            characterList.Add("X");
            characterList.Add("Y");
            characterList.Add("Z");
            characterList.Add("0");
            characterList.Add("1");
            characterList.Add("2");
            characterList.Add("3");
            characterList.Add("4");
            characterList.Add("5");
            characterList.Add("6");
            characterList.Add("7");
            characterList.Add("8");
            characterList.Add("9");

            ArrayList word = new ArrayList();


            Random random1 = new Random();
            int randomNumber1 = random1.Next(0, characterList.Count);



            while (i < stringSize)
            {


                // PROBLEM 2
                // There is no Random character function in C#/.NET, you'll need to create your own using the Random class
                // documentation on using the .NET random number generator can be found here :
                // https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netframework-4.7.2 …
                //
                // You'll need to generate a random number from 0 to 255, then use the string functions to turn that into a char









                // Choose a character from the list
                word.Add(characterList[randomNumber1]); //Hope it work now
                // Add the character to the string
                
                i = i + 1;
            }

           return word;
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





        // PROBLEM 3
        // YES, double[] color DOES declare the variable as an Array (Specifically an array of double values [Doubles are 64 bit numbers])
        // your SRGBToLinear function, only takes a single double as it's parameter, and returns a single double as it's result
        // this is why the 'color' has a red squiggle under it, beacuse your trying to pass the wrong type to your function











        // Convert encoded sRGB to luminance factor
        static double LuminanceSRGB(double color) 
        {
            // Convert to linear sRGB
            double[] c = SRGBToLinear(color); // from this i need to deal it to c[0] / c[1] / c[3] for the R / G / B     <----  Help
            // Find the linear sRGB luminance factor
            return c[0] * 0.2126 + c[1] * 0.7152 + c[2] * 0.0722;
        }

        // Convert encoded sRGB (with D50/2 white point)
        // to luminance factor
        static double LuminanceSRGBD50(double color)
        {
            // Convert to linear sRGB
            double[] c = SRGBToLinear(color); //// from this i need to deal it to c[0] / c[1] / c[3] for the R / G / B
            // Find the linear sRGB luminance factor
            return c[0] * 0.2225 + c[1] * 0.7169 + c[2] * 0.0606;
        }








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


            // PROBLEM 5
            // Your trying to pass in the wrong variable types as parameters to the function calls, in the case of 
            // all 3, you have also not declared any variables of those names, so the compiler cannot find them

            double c = 16777216;
            double[] pixel = { 0, 1, 2 };
            double color = 16777216;


            SRGBToLinear(c); //what i put in ?
            SRGBToLinear3(pixel); // i think i put _imageBuffer in.
            LuminanceSRGB(color); //i put in the RGB who was transform into sRGB linear


            Random random1 = new Random();
            int randomNumber1 = random1.Next(0, 16777216);


            // Generating a random 8-bpc encoded RGB color is equivalent to calling this
            From888(randomNumber1); 

            Random random2 = new Random();
            int randomNumber2 = random2.Next(1); // i Hope its this for generate Random number beetween 0 and 1



            // PROBLEM 6
            // You CANNOT declare arrays in this manner in C#, it's not like JavaScript or PHP where you just build the array on the fly
            // if you want to declare the contents of an array, you have to do it using the "new" constructor as follows
            //  double[] randomColor = new double[]{ randomNumber2, randomNumber2, randomNumber2 }



            //Generating a random color in the 0-1 format is equivalent to generating this.
            double[] randomColor =  { randomNumber2, randomNumber2, randomNumber2 }; // don't need new double[] apparently


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


// Further Notes.....................
// Looking at your code, I strikes me that you may be more used to either plain old C/C++ or JavaScript as a lot of the
// mistakes your making are language syntax based mistakes, and are common patterns in C++ and JavaScript.
//
// May I suggest downloading something like : https://www.introprogramming.info/english-intro-csharp-book/downloads/ …
// as this will take you through the differences between what your used to and wahts different.
//
// Remember, C# is a VERY, VERY strict language when it comes to enforcing data types, it's not as forgiving as other languages
// and it will break if you try to do variable assignments it does not like :-)
