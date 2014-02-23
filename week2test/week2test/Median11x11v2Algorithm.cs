//Jeroen Huisen & Hendrik Cornelisse
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace week2test
{
    class Median11x11v2Algorithm : VisionAlgorithm
    {
        public Median11x11v2Algorithm(String name) : base(name) { }
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
                uint* p2 = (uint*)ptrSource2.ToPointer();
                uint* p = (uint*)ptrSource.ToPointer();
                uint[] input = new uint[121];

                for (int i = sourceImage.Height - 5; i > 5; --i)
                {
                    for (int j = sourceImage.Width - 5; j > 5; --j)
                    {
                        uint test = 0;
                        for (int x = 0; x < 11; x++)
                        {
                            input[test] = *(p2 - 5 + ((uint)sourceData.Stride / 4) *x);//read 11 pixels
                            input[test+1] = *(p2 - 4 + ((uint)sourceData.Stride / 4) * x);
                            input[test+2] = *(p2 - 3 + ((uint)sourceData.Stride / 4) * x);
                            input[test+3] = *(p2 - 2 +((uint)sourceData.Stride / 4)*x);
                            input[test + 4] = *(p2 - 1 + ((uint)sourceData.Stride / 4) * x);
                            input[test + 5] = *(p2 + ((uint)sourceData.Stride / 4)*x);
                            input[test +6] = *(p2 + 1 + ((uint)sourceData.Stride / 4) * x);
                            input[test + 7] = *(p2 + 2 + ((uint)sourceData.Stride / 4) * x);
                            input[test + 8] = *(p2 + 3 + ((uint)sourceData.Stride / 4) * x);
                            input[test + 9] = *(p2 + 4 + ((uint)sourceData.Stride / 4) * x);
                            input[test + 10] = *(p2 + 5 + ((uint)sourceData.Stride / 4) * x);
                            test += 11;
                        }//read 11 lines
                        Array.Sort(input);//sort
                        *p++ = input[60]; //Write pixel to file
                        p2++;
                    }
                    p += (sourceData.Stride / 4 - sourceImage.Width - 10);//new line - sourceImage.Width so it starts at the beginning
                    p2 += (sourceData.Stride / 4 - sourceImage.Width - 10);
                    
                }
                sourceImage.UnlockBits(sourceData);
                returnImage.UnlockBits(imageData);
            }
            return returnImage;
        }
    }
}
