using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEngine
{
    public class Configuration
    {
        public int ScreenWidth;
        public int ScreenHeight;


        public Configuration(int screenWidth, int screenHeight)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }
    }
}
