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
                    image.setPixel(value[5], i+1, j+1);
                }
            }*/
            
            uint[] value = image.readMask(9, 9, 0, 0);
            for (int i = 0; i < 81; i++)
            {
                
                image.setPixel(value[41], 3, 3);
                Console.WriteLine(value[i]);
            }
            Console.WriteLine("ik verkloot nu de gegevens jeej");
            Array.Sort(value);
            for (int i = 0; i < 81; i++)
            {

                image.setPixel(value[41], 3, 3);
                Console.WriteLine(value[i]);
            }
            return image.getImage();
        }
    }
}
