using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Week3
{
    class Median3x3Algorithm : VisionAlgorithm
    {
        public Median3x3Algorithm(String name) : base(name) { }
        public override System.Drawing.Bitmap DoAlgorithm(System.Drawing.Bitmap sourceImage)
        {
            Image image = new Image(sourceImage);

            for (int j = 0; j < sourceImage.Height-2; j++)
            {
                for (int i = 0; i < sourceImage.Width-2; i++)
                {
                    uint[] value = image.readMask(3, 3, i, j);
                    // 0 t/m 2 is eerste 3
                    // 3 t/m 5 is middelste
                    // 6 t/m 8 is onderste
                    uint[] red = new uint[9];
                    uint[] green = new uint[9];
                    uint[] blue = new uint[9];
                    for (int z = 0; z < 9; z++ )
                    {
                        red[z] = (value[z] & 0xFF0000) >> 16;
                        green[z] = (value[z] & 0xFF00) >> 8;
                        blue[z] = value[z] & 0xFF;
                    }
                    Array.Sort(red);
                    Array.Sort(green);
                    Array.Sort(blue);
                    uint red5 = red[4] << 16;
                    uint green5 = green[4] << 8;
                    uint blue5 = blue[4];
                    uint total = red5+green5+blue5;
                    //middelste waarde staat op 5 nu;
                    image.setPixel(total, i+1, j+1);
                }
            }
           
            return image.getImage();
        }
    }
}
