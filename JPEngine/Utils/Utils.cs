using System;
using Microsoft.Xna.Framework;

namespace JPEngine.Utils
{
    public static class Utils
    {
        public static OpenTK.GameWindow GetForm(this GameWindow gameWindow)
        {
            // FIXME : Fix this, currently OpenTKGameWindow is private, check this
//            Type type = typeof(OpenTKGameWindow);
//            System.Reflection.FieldInfo field = type.GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//            if (field != null)
//                return field.GetValue(gameWindow) as OpenTK.GameWindow;

            return null;
        }
    }
}