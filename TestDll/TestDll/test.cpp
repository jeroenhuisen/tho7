#include "StdAfx.h"
#include "test.h"
#include <iostream>

using namespace std;

test::test(void)
{
}

test::~test(void)
{
}

void test::hello()
{
	cout << "Hello World of DLL" << endl;
}

void test::helloStatic()
{
	cout << "Hello World of DLL static" << endl;
}