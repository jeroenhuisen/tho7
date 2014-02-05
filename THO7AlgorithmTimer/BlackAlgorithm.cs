using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace THO7AlgorithmTimerApplication
{
    class BlackAlgorithm : VisionAlgorithm
    {
        public BlackAlgorithm(String name) : base(name) { }
       /* public override System.Drawing.Bitmap DoAlgorithm(System.Drawing.Bitmap sourceImage)
        {
            Bitmap returnImage = new Bitmap(sourceImage);
            for (int i = 0; i < returnImage.Width; i++)
            {
                for (int y = 0; y < returnImage.Height; y++)
                {
                    returnImage.SetPixel(i, y, Color.Black);
                }
            }
            return returnImage;
        }*/
        public override System.Drawing.Bitmap DoAlgorithm(System.Drawing.Bitmap sourceImage)
        {
            //Bitmap bmp2 = new Bitmap(sourceImage.Width, sourceImage.Height);
            Bitmap bmp = new Bitmap(sourceImage.Width, sourceImage.Height);
            
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle ImageSize = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
                graph.FillRectangle(Brushes.Black, ImageSize);
            }
            return bmp;
        }
    }
}
