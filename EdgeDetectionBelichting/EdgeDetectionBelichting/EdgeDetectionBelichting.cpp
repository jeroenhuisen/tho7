// EdgeDetectionBelichting.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "ImageV2.h"
#include "EdgeDetectionFilter.h"

int main(int argc, char* argv[])
{
	EdgeDetectionFilter edf;
	Image img(argv[1]);
	Image target(img.Width(), img.Height(), img.GetChannels());
	for (int y = 0; y < target.Height(); y++){
		for (int x = 0; x < target.Width(); x++){
			*target.Data(x, y, 0) = 0;
			*target.Data(x, y, 1) = 0;
			*target.Data(x, y, 2) = 0;
		}
	}
	edf.SobelEdgeDetector(img, target, 150);
	return 0;
}

