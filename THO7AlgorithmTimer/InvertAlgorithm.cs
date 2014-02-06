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
        public InvertAlgorithm(String name) : base(name) { }
        public override System.Drawing.Bitmap DoAlgorithm(System.Drawing.Bitmap sourceImage)
        {

           //Bitmap returnImage = new Bitmap(sourceImage);
            
           /* for (int i = 0; i < returnImage.Width; i++ )//forloop aftellend zijn sneller dan optellend volgens bart goes.
            {
                for (int y = 0; y < returnImage.Height; y++)
                {
                    Color toInvert = returnImage.GetPixel(i, y);
                    returnImage.SetPixel(i, y, Color.FromArgb(toInvert.A, 255 - toInvert.R, 255 - toInvert.G, 255 - toInvert.B));
                }
            }*/
            /*Color toInvert;
           
            for (int i = returnImage.Width -1 ; i >= 0; i--)//forloop aftellend zijn sneller dan optellend volgens bart goes.
            {
                for (int y = returnImage.Height -1; y >= 0; y--)
                {
                    toInvert = returnImage.GetPixel(i, y);
                    returnImage.SetPixel(i, y, Color.FromArgb(toInvert.A, 255 - toInvert.R, 255 - toInvert.G, 255 - toInvert.B));
                }
            }*/
/*
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
            // Create a new bitmap.
            Bitmap bmp = new Bitmap(sourceImage.Width, sourceImage.Height, sourceImage.PixelFormat);

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
            System.Drawing.Imaging.BitmapData sourceImageData = sourceImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, sourceImage.PixelFormat);

            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = sourceImageData.Scan0;
            IntPtr ptr2 = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap. 
            int bytes = Math.Abs(sourceImageData.Stride) * sourceImage.Height;
            //int bytes1 = Math.Abs(bmpData.Stride) * bmpData.Height;
            byte[] rgbValues = new byte[bytes];
           // byte[] rgbValues1 = new byte[bytes1];

            // Copy the RGB values into the array.
          //  System.Runtime.InteropServices.Marshal.Copy(ptr2, rgbValues, 0, bytes);
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            

            // Set every third value to 255. A 24bpp bitmap will look red.   
            for (int counter = rgbValues.Length - 1; counter >= 0; counter -= 4)
            {
                rgbValues[counter] = (byte)(255 - rgbValues[counter]);
                rgbValues[counter - 1] = (byte)(255 - rgbValues[counter - 1]);
                rgbValues[counter - 2] = (byte)(255 - rgbValues[counter - 2]);
                rgbValues[counter - 3] = (byte)(255 - rgbValues[counter - 3]);
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr2, bytes);
            //System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(sourceImageData);
            sourceImage.UnlockBits(sourceImageData);

            return bmp;
        }
    }
}
