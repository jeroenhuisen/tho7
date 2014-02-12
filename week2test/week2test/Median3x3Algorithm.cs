using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace week2test
{
    class Median3x3Algorithm : VisionAlgorithm
    {
        public Median3x3Algorithm(String name) : base(name) { }
        public override System.Drawing.Bitmap DoAlgorithm(System.Drawing.Bitmap sourceImage)
        {
            Image i = new Image(sourceImage);
            sourceImage = i.invert();
            sourceImage = i.size(100,1000);
            uint[] value = i.mask(100, 1000, 0, 0);

            return sourceImage;
            //return i.getImage();
        }
    }
}
