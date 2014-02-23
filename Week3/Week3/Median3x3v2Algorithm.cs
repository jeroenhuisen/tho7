//Jeroen Huisen & Hendrik Cornelisse
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Week3
{
    class Median3x3v2Algorithm : VisionAlgorithm
    {
        public Median3x3v2Algorithm(String name) : base(name) { }
        public override System.Drawing.Bitmap DoAlgorithm(System.Drawing.Bitmap sourceImage)
        {

            Bitmap returnImage = new Bitmap(sourceImage.Width, sourceImage.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Rectangle rect = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
            System.Drawing.Imaging.BitmapData imageData = returnImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Imaging.BitmapData sourceData = sourceImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            IntPtr ptrSource2 = sourceData.Scan0;
            IntPtr ptrSource = imageData.Scan0;

            unsafe
            {
                uint[] input = new uint[9];
                uint* p2 = (uint*)ptrSource2.ToPointer();
                uint* p = (uint*)ptrSource.ToPointer();

                for (int i = sourceImage.Height - 1; i > 1; --i)
                {
                    for (int j = sourceImage.Width - 1; j > 1; --j)
                    {
                        input[0] = *(p2 - 1 + (uint)sourceData.Stride / 4);//read all 9 pixels
                        input[1] = *(p2 + (uint)sourceData.Stride / 4);
                        input[2] = *(p2 + 1 + (uint)sourceData.Stride / 4);
                        input[3] = *(p2 - 1);
                        input[4] = *p2;
                        input[5] = *(p2 + 1);
                        input[6] = *(p2 - 1 - (uint)sourceData.Stride / 4);
                        input[7] = *(p2 - (uint)sourceData.Stride / 4);
                        input[8] = *(p2 + 1 - (uint)sourceData.Stride / 4);
                        Array.Sort(input);
                        *p++ = input[4]; //Write pixel to file
                        p2++;
                    }
                    p += (sourceData.Stride / 4 - sourceImage.Width - 2);//new line - sourceImage.Width so  it starts at the beginning
                    p2 += (sourceData.Stride / 4 - sourceImage.Width - 2);

                }
                sourceImage.UnlockBits(sourceData);
                returnImage.UnlockBits(imageData);
            }
            return returnImage;
        }
    }
}
