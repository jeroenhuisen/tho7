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
DLLDIR void hello();
//	DLLDIR static void helloStatic();

//};