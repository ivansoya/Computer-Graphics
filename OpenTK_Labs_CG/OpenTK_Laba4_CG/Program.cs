using OpenTK;
using System;
using System.Windows;

namespace OpenTK_Laba4_CG
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var window = new Window((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight, "Hello World"))
            {
                window.WindowState = OpenTK.WindowState.Fullscreen;
                window.Run(60.0);
            }
        }
    }
}
