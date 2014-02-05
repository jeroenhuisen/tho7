#include "stdafx.h"
#include "test.h"

int _tmain(int argc, _TCHAR* argv[])
{

	test test;
	test.hello();
	test::helloStatic();

	getchar();

	return 0;
}