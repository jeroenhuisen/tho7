using System;
using System.Runtime.InteropServices;

class Example
{
    // Use DllImport to import the Win32 MessageBox function.
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);
    [DllImport("test.dll", CharSet = CharSet.Unicode)]
    public extern class test();

    static void Main()
    {
        // Call the MessageBox function using platform invoke.
        MessageBox(new IntPtr(0), "Hello World!", "Hello Dialog", 0);
        test t = new test();
    }
}