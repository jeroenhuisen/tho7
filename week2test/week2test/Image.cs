using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace week2test
{
    class Image
    {
        private Bitmap image;
        private Bitmap imageOld;
        public Image(System.Drawing.Bitmap sourceImage)
        {
            image = sourceImage;
            imageOld = sourceImage;
        }

        public Bitmap invert(){
            
            Bitmap returnImage = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            System.Drawing.Imaging.BitmapData sourceData = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Imaging.BitmapData bmpData = returnImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            IntPtr ptr = bmpData.Scan0;
            IntPtr ptrSource = sourceData.Scan0;

            int nrOfInts = (Math.Abs(sourceData.Stride) * returnImage.Height) / 4;

            unsafe
            {
                uint* p = (uint*)ptr.ToPointer();
                uint* p2 = (uint*)ptrSource.ToPointer();
                uint* p2End = p2 + nrOfInts;
                while (p2 != p2End)
                {
                    *p++ = ~(*p2++);
                }
            }

            returnImage.UnlockBits(bmpData);
            image.UnlockBits(sourceData);
            return returnImage;
        }
        public Bitmap size(int x, int y)
        {

            Bitmap returnImage = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Rectangle rect = new Rectangle(0, 0, x, y);
            System.Drawing.Imaging.BitmapData sourceData = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Imaging.BitmapData bmpData = returnImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            IntPtr ptr = bmpData.Scan0;
            IntPtr ptrSource = sourceData.Scan0;

            Console.WriteLine(bmpData.Stride);
            int nrOfInts = (Math.Abs(sourceData.Stride) * returnImage.Height) / 4;

            unsafe
            {
                uint* p = (uint*)ptr.ToPointer();
                uint* p2 = (uint*)ptrSource.ToPointer();
                uint* p2End = p2 + nrOfInts;
                while (p2 != p2End)
                {
                    *p++ = ~(*p2++);
                }
                //
                uint[] array = new uint[x*y];
                int amount = 0;
                p2 = (uint*)ptrSource.ToPointer();
                for(int i = 0; i < x; i++){
                    for(int j = 0; j < y; j++){
                        array[amount] = *p2++;
                        amount++;
                    }
                    p2 += (sourceData.Stride/4 - y);
                }
                //
                p = (uint*)ptr.ToPointer();
                amount = 0;
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        *p++ = array[amount];
                        amount++;
                    }
                    p += (sourceData.Stride/4 - y);
                }
               


             
                //Console.WriteLine(*p);
            }

            returnImage.UnlockBits(bmpData);
            image.UnlockBits(sourceData);
            return returnImage;
        }


    }
}
