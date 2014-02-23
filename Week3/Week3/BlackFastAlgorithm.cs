using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Week3 
{
    class BlackFastAlgorithm : VisionAlgorithm
    {
        public BlackFastAlgorithm(String name) : base(name) { }
        public override System.Drawing.Bitmap DoAlgorithm(System.Drawing.Bitmap sourceImage)
        {
            Bitmap returnImage = new Bitmap(sourceImage.Width, sourceImage.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            return returnImage;
            //return new Bitmap(sourceImage.Width, sourceImage.Height, sourceImage.PixelFormat);
        }
    }
}
