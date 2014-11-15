using System;
using System.Windows.Forms;
using JPEngine;
using JPEngine.Graphics;
using UnitTestProject.ManualTests;

namespace UnitTestProject
{

    #if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ManualTestsCore.FormFullscreenTest();
        }
    }
#endif
}
