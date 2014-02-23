#pragma once

//more about this in reference 1
#ifdef DLLDIR_EX
#define DLLDIR  __declspec(dllexport)   // export DLL information

#else
#define DLLDIR  __declspec(dllimport)   // import DLL information

#endif 

/*class DLLDIR test
{
public:
	test(void);
	~test(void);
	*/
extern "C"{
	DLLDIR void hello();
	DLLDIR void editImage(int * source, int * target, int height, int width, int stride);
}
//	DLLDIR static void helloStatic();

//};