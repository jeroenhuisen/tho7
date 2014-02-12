using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace THO7AlgorithmTimerApplication
{

    class InvertAlgorithm : VisionAlgorithm
    {
        private byte[] rgbValues;
        private int rgbSize;
        public InvertAlgorithm(String name) : base(name) { }
        public override System.Drawing.Bitmap DoAlgorithm(System.Drawing.Bitmap sourceImage)
        {
           //Bitmap returnImage = new Bitmap(sourceImage);
            /*
             for (int i = 0; i < returnImage.Width; i++ )//forloop aftellend zijn sneller dan optellend volgens bart goes.
            {
                for (int y = 0; y < returnImage.Height; y++)
                {
                    Color toInvert = returnImage.GetPixel(i, y);
                    returnImage.SetPixel(i, y, Color.FromArgb(toInvert.A, 255 - toInvert.R, 255 - toInvert.G, 255 - toInvert.B));
                }
            }*/
            /*Color toInvert;
           
            for (int i = returnImage.Width - 1; i >= 0; i--)//forloop aftellend zijn sneller dan optellend volgens bart goes.
            {
                for (int y = returnImage.Height - 1; y >= 0; y--)
                {
                    toInvert = returnImage.GetPixel(i, y);
                    returnImage.SetPixel(i, y, Color.FromArgb(toInvert.A, 255 - toInvert.R, 255 - toInvert.G, 255 - toInvert.B));
                }
            }
            return returnImage;
            }*/
/*
             * //Based on http://msdn.microsoft.com/en-us/library/5ey6h79d(v=vs.110).aspx

            // Create a new bitmap.
            Bitmap bmp = new Bitmap(sourceImage);

            // Lock the bitmap's bits.  
            Rectangle rect =new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap. 
            int bytes  = Math.Abs(bmpData.Stride) * bmp.Height;
           byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            // Set every third value to 255. A 24bpp bitmap will look red.   
            for (int counter = rgbValues.Length -1; counter >= 0; counter -= 4)
            {
                    rgbValues[counter-1] = (byte)(255 - rgbValues[counter-1]);
                    rgbValues[counter-2] = (byte)(255 - rgbValues[counter-2]);
                    rgbValues[counter-3] = (byte)(255 - rgbValues[counter-3]);
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            return bmp;*/
            /* for (int counter = rgbValues.Length - 1; counter >= 0; counter -= 4)
            {
                rgbValues[counter] = (byte)(255 - rgbValues[counter]);
                rgbValues[counter - 1] = (byte)(255 - rgbValues[counter - 1]);
                rgbValues[counter - 2] = (byte)(255 - rgbValues[counter - 2]);
                rgbValues[counter - 3] = (byte)(255 - rgbValues[counter - 3]);
            }*/
            //Based on http://msdn.microsoft.com/en-us/library/5ey6h79d(v=vs.110).aspx
            // Create a new bitmap.
            Bitmap bmp = new Bitmap(sourceImage.Width, sourceImage.Height, sourceImage.PixelFormat);
            //Console.WriteLine(sourceImage.PixelFormat);

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);

            System.Drawing.Imaging.BitmapData sourceImageData =
                sourceImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                sourceImage.PixelFormat);

            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly,
                sourceImage.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = sourceImageData.Scan0;
            IntPtr ptr2 = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap. 
            int bytes = Math.Abs(sourceImageData.Stride) * sourceImage.Height;
            byte[] rgbValues1 = new byte[bytes];

            rgbValues = rgbValues1;
            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            

            // Set every third value to 255. A 24bpp bitmap will look red.   
            if (sourceImage.PixelFormat.ToString().Equals("Format24bppRgb"))
            {
                rgbSize = 3;
            }
            else
            {
                rgbSize = 4;
            }
            Thread thread1 = new Thread(new ThreadStart(test1v2));
            Thread thread2 = new Thread(new ThreadStart(test2v2));
            Thread thread3 = new Thread(new ThreadStart(test3v2));
            Thread thread4 = new Thread(new ThreadStart(test4v2));
            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();




           

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr2, bytes);

            // Unlock the bits.
            bmp.UnlockBits(sourceImageData);
            sourceImage.UnlockBits(sourceImageData);

            return bmp;
        }
        private void test1v2()
        {
            for (int counter = rgbValues.Length / 4 - 1; counter >= 0; counter -= rgbSize)
            {
                rgbValues[counter - rgbSize + 3] = (byte)(255 - rgbValues[counter - rgbSize + 3]);
                rgbValues[counter - rgbSize + 2] = (byte)(255 - rgbValues[counter - rgbSize + 2]);
                rgbValues[counter - rgbSize + 1] = (byte)(255 - rgbValues[counter - rgbSize + 1]);
            }
        }
        private void test2v2()
        {
            for (int counter = rgbValues.Length / 2 - 1; counter >= rgbValues.Length / 4; counter -= rgbSize)
            {
                rgbValues[counter - rgbSize + 3] = (byte)(255 - rgbValues[counter - rgbSize + 3]);
                rgbValues[counter - rgbSize + 2] = (byte)(255 - rgbValues[counter - rgbSize + 2]);
                rgbValues[counter - rgbSize + 1] = (byte)(255 - rgbValues[counter - rgbSize + 1]);
            }
        }
        private void test3v2()
        {
            for (int counter = rgbValues.Length / 4 * 3 - 1; counter >= rgbValues.Length / 2; counter -= rgbSize)
            {
                rgbValues[counter - rgbSize + 3] = (byte)(255 - rgbValues[counter - rgbSize + 3]);
                rgbValues[counter - rgbSize + 2] = (byte)(255 - rgbValues[counter - rgbSize + 2]);
                rgbValues[counter - rgbSize + 1] = (byte)(255 - rgbValues[counter - rgbSize + 1]);
            }
        }
        private void test4v2()
        {
            for (int counter = rgbValues.Length - 1; counter >= rgbValues.Length / 4 * 3; counter -= rgbSize)
            {
                rgbValues[counter - rgbSize + 3] = (byte)(255 - rgbValues[counter - rgbSize + 3]);
                rgbValues[counter - rgbSize + 2] = (byte)(255 - rgbValues[counter - rgbSize + 2]);
                rgbValues[counter - rgbSize + 1] = (byte)(255 - rgbValues[counter - rgbSize + 1]);
            }
        }
        public void test1()
        {
            for (int counter = rgbValues.Length/4 - 1; counter >= 0; counter -= 3)
            {
                rgbValues[counter] = (byte)(255 - rgbValues[counter]);
                rgbValues[counter - 1] = (byte)(255 - rgbValues[counter - 1]);
                rgbValues[counter - 2] = (byte)(255 - rgbValues[counter - 2]);
            }
        }
        public void test1x()
        {
            for (int counter = rgbValues.Length / 4 - 1; counter >= 0; counter -= 4)
            {
                rgbValues[counter - 1] = (byte)(255 - rgbValues[counter - 1]);
                rgbValues[counter - 2] = (byte)(255 - rgbValues[counter - 2]);
                rgbValues[counter - 3] = (byte)(255 - rgbValues[counter - 3]);
            }
        }
        public void test2()
        {
            for (int counter = rgbValues.Length / 4 * 3 - 1; counter >= rgbValues.Length / 2; counter -= 3)
            {
                rgbValues[counter] = (byte)(255 - rgbValues[counter]);
                rgbValues[counter - 1] = (byte)(255 - rgbValues[counter - 1]);
                rgbValues[counter - 2] = (byte)(255 - rgbValues[counter - 2]);
            }
        }
        public void test2x()
        {
            for (int counter = rgbValues.Length / 4 * 3 - 1; counter >= rgbValues.Length / 2; counter -= 4)
            {
                rgbValues[counter - 1] = (byte)(255 - rgbValues[counter - 1]);
                rgbValues[counter - 2] = (byte)(255 - rgbValues[counter - 2]);
                rgbValues[counter - 3] = (byte)(255 - rgbValues[counter - 3]);
            }
        }
        public void test3()
        {
            for (int counter = rgbValues.Length / 2 - 1; counter >= rgbValues.Length / 4; counter -= 3)
            {
                rgbValues[counter] = (byte)(255 - rgbValues[counter]);
                rgbValues[counter - 1] = (byte)(255 - rgbValues[counter - 1]);
                rgbValues[counter - 2] = (byte)(255 - rgbValues[counter - 2]);
            }
        }
        public void test3x()
        {
            for (int counter = rgbValues.Length / 2 - 1; counter >= rgbValues.Length / 4; counter -= 4)
            {
                //rgbValues[counter] = (byte)(255 - rgbValues[counter]);
                rgbValues[counter - 1] = (byte)(255 - rgbValues[counter - 1]);
                rgbValues[counter - 2] = (byte)(255 - rgbValues[counter - 2]);
                rgbValues[counter - 3] = (byte)(255 - rgbValues[counter - 3]);
            }
        }
        public void test4()
        {
            int counter;
            for (counter = rgbValues.Length - 1; counter >= rgbValues.Length / 4 * 3; counter -= 3)
            {
                rgbValues[counter] = (byte)(255 - rgbValues[counter]);
                
                rgbValues[counter-1] = (byte)(255 - rgbValues[counter - 1]);
                rgbValues[counter - 2] = (byte)(255 - rgbValues[counter - 2]);
                //rgbValues[counter - 3] = (byte)(255 - rgbValues[counter - 3]);
            }
        }
        public void test4x()
        {
            int counter;
            for (counter = rgbValues.Length - 1; counter >= rgbValues.Length / 4 * 3; counter -= 4)
            {
                //rgbValues[counter] = (byte)(255 - rgbValues[counter]);

                rgbValues[counter-1] = (byte)(255 - rgbValues[counter - 1]);
                rgbValues[counter - 2] = (byte)(255 - rgbValues[counter - 2]);
                rgbValues[counter - 3] = (byte)(255 - rgbValues[counter - 3]);
            }
        }

    }
}
