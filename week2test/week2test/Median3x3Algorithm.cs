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
            Image image = new Image(sourceImage);

            /*for (int j = 0; j < sourceImage.Height-2; j++)
            {
                for (int i = 0; i < sourceImage.Width-2; i++)
                {
                    uint[] value = image.readMask(3, 3, i, j);
                    // 0 t/m 2 is eerste 3
                    // 3 t/m 5 is middelste
                    // 6 t/m 8 is onderste
                    Array.Sort(value);
                    //middelste waarde staat op 5 nu;
                    image.write(value[5], i+1, j+1);
                }
            }*/
            uint[] value = image.readMask(3, 3, 0, 0);
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine(value[i]);
            }
            return image.getImage();
        }
    }
}
