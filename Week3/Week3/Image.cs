using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Week3
{
    class Image
    {
        private Bitmap image;
        private Bitmap imageOld;
        public Image(System.Drawing.Bitmap sourceImage)
        {
            image = new Bitmap(sourceImage);
            imageOld = new Bitmap(image);
        }

        public Bitmap invert()
        {

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

            //Console.WriteLine(bmpData.Stride);
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
                uint[] array = new uint[x * y];
                int amount = 0;
                p2 = (uint*)ptrSource.ToPointer();
                for (int i = y; i > 0; --i)
                {
                    for (int j = x; j > 0; --j)
                    {
                        array[amount] = *p2++;
                        amount++;
                    }
                    p2 += (sourceData.Stride / 4 - x);
                }
                //
                p = (uint*)ptr.ToPointer();
                amount = 0;
                for (int i = y; i > 0; --i)
                {
                    for (int j = x; j > 0; --j)
                    {
                        *p++ = array[amount];
                        amount++;
                    }
                    p += (sourceData.Stride / 4 - x);
                }




                //Console.WriteLine(*p);
            }

            returnImage.UnlockBits(bmpData);
            image.UnlockBits(sourceData);
            return returnImage;
        }
        public uint[] readMask(int x, int y, int xOffset, int yOffset)
        {
            Rectangle rect = new Rectangle(xOffset, yOffset, x, y);
            System.Drawing.Imaging.BitmapData sourceData = imageOld.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            IntPtr ptrSource = sourceData.Scan0;
            unsafe
            {
                uint* p2 = (uint*)ptrSource.ToPointer();
                uint[] array = new uint[x * y];
                int amount = 0;
                for (int i = y; i > 0; --i)
                {
                    for (int j = x; j > 0; --j)
                    {
                        array[amount] = *p2++;
                        //Console.WriteLine(*p2);
                        amount++;
                    }
                    //Console.WriteLine("done");
                    p2 += (sourceData.Stride / 4 - x);
                }
                //Console.WriteLine("done");
                imageOld.UnlockBits(sourceData);
                return array;
            }
        }
        public void write(uint[] input, int x, int y, int xOffset, int yOffset)
        {
            //under construction
            Rectangle rect = new Rectangle(xOffset, yOffset, x, y);
            System.Drawing.Imaging.BitmapData sourceData = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            IntPtr ptrSource = sourceData.Scan0;

            unsafe
            {
                uint* p2 = (uint*)ptrSource.ToPointer();
                //p2 += sourceData.Stride / 4 * yOffset;
                int amount = 0;
                for (int i = y; i > 0; --i)
                {
                    for (int j = x; j > 0; --j)
                    {
                        *p2++ = input[amount];
                        amount++;
                    }
                    p2 += (sourceData.Stride / 4 - x);
                }
                image.UnlockBits(sourceData);
            }
        }
        public void setPixel(uint input, int xOffset, int yOffset)
        {
            //under construction
            Rectangle rect = new Rectangle(xOffset, yOffset, 1, 1);
            System.Drawing.Imaging.BitmapData sourceData = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            IntPtr ptrSource = sourceData.Scan0;

            unsafe
            {
                uint* p = (uint*)ptrSource.ToPointer();
                //p2 += sourceData.Stride / 4 * yOffset;
                *p = input;
                image.UnlockBits(sourceData);
            }
        }
        public Bitmap getImage()
        {
            return image;
        }
        public Bitmap getNewImage()
        {
            return new Bitmap(image);
        }
    }
}
