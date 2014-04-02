using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Week3
{
    class Class1 : VisionAlgorithm
    {
        public Class1(String name) : base(name) { }
        [DllImport("TestDll.dll")]
        private static extern void hello();
        [DllImport("TestDll.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void editImage(IntPtr source, IntPtr target, int height, int width, int stride);
         [DllImport("TestDll.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void filterHistogram(IntPtr source, IntPtr target, int value, int height, int width, int stride);
        public override System.Drawing.Bitmap DoAlgorithm(System.Drawing.Bitmap sourceImage)
        {
            Bitmap returnImage = new Bitmap(sourceImage.Width, sourceImage.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);

            System.Drawing.Imaging.BitmapData sourceImageData =
                sourceImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                sourceImage.PixelFormat);

            System.Drawing.Imaging.BitmapData bmpData =
                returnImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly,
                sourceImage.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = sourceImageData.Scan0;
            IntPtr ptr2 = bmpData.Scan0;
            int nrOfInts = (Math.Abs(bmpData.Stride) * returnImage.Height) / 4;
            //editImage(ptr, ptr2, sourceImage.Height, sourceImage.Width, Math.Abs(sourceImageData.Stride));
            filterHistogram(ptr, ptr2, 11, sourceImage.Height, sourceImage.Width, Math.Abs(sourceImageData.Stride));
            sourceImage.UnlockBits(sourceImageData);
            returnImage.UnlockBits(bmpData);
            return returnImage;
        }
    }
}
