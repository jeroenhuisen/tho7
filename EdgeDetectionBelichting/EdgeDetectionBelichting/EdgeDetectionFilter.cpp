#include "stdafx.h"
#include "EdgeDetectionFilter.h"
#include <iostream>
#include <vector>


void EdgeDetectionFilter::SobelEdgeDetector(Image source, Image target, int threshold){
	for (int y = 1; y < source.Height()-1; y++){
		const unsigned char * src_offset = source.Data(0, y, 0); //pointer to first red value of source image
		const unsigned char * src_offset1 = source.Data(0, y+1, 0);
		const unsigned char * src_offset2 = source.Data(0, y-1, 0);
		unsigned char * target_offset = (unsigned char *) target.Data(0, y, 0);
		for (int x = 1; x < source.Width()-1; x++){
			int xGradient = *(src_offset1 + x - 1) + *(src_offset + x -1 ) * 2 + *(src_offset2 + x - 1) - *(src_offset1 + x + 1) - *(src_offset + x + 1) * 2 - *(src_offset2 + x + 1);
			int yGradient = *(src_offset2 + x - 1) + *(src_offset2 + x) * 2 + *(src_offset2 + x + 1) - *(src_offset1 + x + 1) - *(src_offset1 + x) * 2 - *(src_offset1 + x - 1);

			int sum = abs(xGradient) + abs(yGradient);
			//sum = sum > 255 ? 255 : sum;
			//sum = sum < 0 ? 0 : sum;
			if (sum > 255){
				sum = 255;//edge?
			}
			if (sum < 0){
				sum = 0; //not possible because of abs right? not sure..
			}
			if (sum >= threshold){
				*(target_offset + x) = sum;
			}
		}

	}
	target.SaveImage("test.bmp");
	//cornerDetector(target, target, treshold, 10);
}
