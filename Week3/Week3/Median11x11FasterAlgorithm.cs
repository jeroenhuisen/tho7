using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Week3
{
    class Median11x11FasterAlgorithm : VisionAlgorithm
    {
        public Median11x11FasterAlgorithm(String name) : base(name) { }
        public override System.Drawing.Bitmap DoAlgorithm(System.Drawing.Bitmap sourceImage)
        {
            Image image = new Image(sourceImage);
            uint[] input = new uint[sourceImage.Height * sourceImage.Width];

            uint[] red = new uint[121];
            uint[] green = new uint[121];
            uint[] blue = new uint[121];

            uint red60 = 0;
            uint green60 = 0;
            uint blue60 = 0;

            uint total = 0;
            int amount = 0;

            for (int j = 0; j < sourceImage.Height - 10; j++) //- 10 because filter is 11 and we want to stay within the image 
            {
                for (int i = 0; i < sourceImage.Width - 10; i++)
                {
                    uint[] value = image.readMask(11, 11, i, j);
                    // 0 t/m 2 is top 3
                    // 3 t/m 5 is mid
                    // 6 t/m 8 is bot
                    //Seperate colors.
                    for (int z = 0; z < 121; z++)
                    {
                        red[z] = (value[z] & 0xFF0000) >> 16;
                        green[z] = (value[z] & 0xFF00) >> 8;
                        blue[z] = value[z] & 0xFF;
                    }
                    Array.Sort(red);
                    Array.Sort(green);
                    Array.Sort(blue);
                    //mid value is on 5 now;
                    red60 = red[60] << 16;
                    green60 = green[60] << 8;
                    blue60 = blue[60];
                    //Added colors to 1 value
                    total = red60 + green60 + blue60;
                    //Set 1 pixel
                    input[amount] = total;
                    amount++;
                    //image.setPixel(total, i+5, j+5);//Cost a lot of time accessing it every single time
                }
            }
            //write once
            image.write(input, sourceImage.Width - 10, sourceImage.Height - 10, 0, 0);


            return image.getImage();
        }
    }
}
