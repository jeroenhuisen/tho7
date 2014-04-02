#include "StdAfx.h"
#include "test.h"
#include <iostream>
#include <algorithm>

using namespace std;

/*test::test(void)
{
}

test::~test(void)
{
}*/

void hello()
{
	cout << "Hello World of DLL" << endl;
}

void helloStatic()
{
	cout << "Hello World of DLL static" << endl;
}


int* getPixelPointerDinges(int* target, int height, int stride, int x, int y){
	int *p = target;
	p += x;// width
	p += stride / 4 * y; // height
	return p;
}

void editImage(int * source, int * target, int height, int width, int stride){
	cout << "Should do something...";
	unsigned int * input = new unsigned int[121];

	unsigned int row = stride / 4;
	int total = 0;
	for (int i = 5; i < height - 5; i++)
	{
		total++;
		for (int j = 5; j < width - 5; j++)
		{
			total++;
			unsigned int test = 0;
			for (int x = 0; x < 11; x++)
			{
				input[test] = *(source - 5 + row *x);//read 11 pixels
				input[test + 1] = *(source - 4 + row * x);
				input[test + 2] = *(source - 3 + row * x);
				input[test + 3] = *(source - 2 + row * x);
				input[test + 4] = *(source - 1 + row * x);
				input[test + 5] = *(source + row * x);
				input[test + 6] = *(source + 1 + row * x);
				input[test + 7] = *(source + 2 + row * x);
				input[test + 8] = *(source + 3 + row * x);
				input[test + 9]	 = *(source + 4 + row * x);
				input[test + 10] = *(source + 5 + row * x);
				test += 11;
			}//read 11 lines
			//Array.Sort(input);//sort
			//std::sort(0, 121);
			*target++ = input[60]; //Write pixel to file
			source++;
		}
		target += (stride / 4 - width - 5);//new line - sourceImage.Width so it starts at the beginning
		source += (stride / 4 - width - 5);
	}
	cout <<"\n" << total << "\n";
}

void filterHistogram(int * source, int * target, int value, int height, int width, int stride){//size of filter
	unsigned char* filterRed = new unsigned char[value * value];
	unsigned char* filterGreen = new unsigned char[value * value];
	unsigned char* filterBlue = new unsigned char[value * value];

	unsigned char* filterRedHistogram = new unsigned char[256];
	unsigned char* filterGreenHistogram = new unsigned char[256];
	unsigned char* filterBlueHistogram = new unsigned char[256];

	for (int i = 0; i < 256; i++){
		filterRedHistogram[i] = 0;
		filterGreenHistogram[i] = 0;
		filterBlueHistogram[i] = 0;
	}

	int filterNumber = 0;
	for (int y = (value - 1) / 2; y < height - (value - 1) / 2; y++){
		for (int x = (value - 1) / 2; x < width - (value - 1) / 2; x++){
			filterNumber = 0;
			for (int filterY = -(value - 1) / 2; filterY <= (value - 1) / 2; filterY++){
				for (int filterX = -(value - 1) / 2; filterX <= (value - 1) / 2; filterX++){
					int *p = source;
					p += x + filterX;// width
					p += stride / 4 * (y + filterY); // height
					//cout << ((*p & 0xFF0000) >> 16) << '\n';
					filterRedHistogram[(*p & 0xFF0000) >> 16] ++;
					filterGreenHistogram[(*p & 0x00FF00) >> 8] ++;
					filterBlueHistogram[*p & 0x0000FF] ++;
					//filterGreenHistogram[image(x + filterX, y + filterY, 0, 1)] ++;
					//filterBlueHistogram[image(x + filterX, y + filterY, 0, 2)] ++;
				}
				//filterNumber += value;
			}
			//Histogram
			int foundRed = 0;
			int foundGreen = 0;
			int foundBlue = 0;
			int searchRed = 0;
			int searchGreen = 0;
			int searchBlue = 0;
			for (int i = 0; i < 256; i++){
				if (foundRed <= (value * value - 1) / 2){
					foundRed += filterRedHistogram[i];
					searchRed++;
				}
				if (foundGreen <= (value * value - 1) / 2){
					foundGreen += filterGreenHistogram[i];
					searchGreen++;
				}
				if (foundBlue <= (value * value - 1) / 2){
					foundBlue += filterBlueHistogram[i];
					searchBlue++;
				}
				filterRedHistogram[i] = 0;
				filterGreenHistogram[i] = 0;
				filterBlueHistogram[i] = 0;
			}
			int *p = target;
			p += x;// width
			p += stride / 4 * y; // height
			int value = searchRed << 16 + searchGreen << 8 + searchBlue;
			*p = value;
			//*getPixelPointerDinges(target, height, stride, x, y) = searchRed;
			//image(x, y, 0, 0) = searchRed;
			//image(x, y, 0, 1) = searchGreen;
			//image(x, y, 0, 2) = searchBlue;
		}
	}
	//image.save("medianHistogram.bmp");
}
