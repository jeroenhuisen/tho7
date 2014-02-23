using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Week3
{
    class Median11x11Algorithm : VisionAlgorithm
    {
        public Median11x11Algorithm(String name) : base(name) { }
        public override System.Drawing.Bitmap DoAlgorithm(System.Drawing.Bitmap sourceImage)
        {
            Image image = new Image(sourceImage);

            for (int j = 0; j < sourceImage.Height-10; j++) //- 10 because filter is 11 and we want to stay within the image 
            {
                for (int i = 0; i < sourceImage.Width-10; i++)
                {
                    uint[] value = image.readMask(11, 11, i, j);
                    // 0 t/m 2 is top 3
                    // 3 t/m 5 is mid
                    // 6 t/m 8 is bot
                    uint[] red = new uint[121];
                    uint[] green = new uint[121];
                    uint[] blue = new uint[121];
                    //Seperate colors.
                    for (int z = 0; z < 121; z++ )
                    {
                        red[z] = (value[z] & 0xFF0000) >> 16;
                        green[z] = (value[z] & 0xFF00) >> 8;
                        blue[z] = value[z] & 0xFF;
                    }
                    Array.Sort(red);
                    Array.Sort(green);
                    Array.Sort(blue);
                    //mid value is on 5 now;
                    uint red5 = red[60] << 16;
                    uint green5 = green[60] << 8;
                    uint blue5 = blue[60];
                    //Added colors to 1 value
                    uint total = red5+green5+blue5;
                    //Set 1 pixel
                    image.setPixel(total, i+5, j+5);//Cost a lot of time accessing it every single time
                }
            }
           
            return image.getImage();
        }
    }
}
