#pragma once
#include <memory>
#include "ImageV2.h"


/*
Made by Jeroen Huisen
*/
class EdgeDetectionFilter
{
	
	//std::unique_ptr<Image> CornerDetector(const Image & source, int threshold, int value);
	//std::unique_ptr<Image> CornerDetectorV2(const Image & source, int threshold, int value);
public: 
	EdgeDetectionFilter(){}
	void SobelEdgeDetector(Image source, Image target, int threshold);
};
