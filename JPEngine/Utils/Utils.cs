using System;
using Microsoft.Xna.Framework;

namespace JPEngine.Utils
{
    public static class Utils
    {
        public static OpenTK.GameWindow GetForm(this GameWindow gameWindow)
        {

            throw new NotImplementedException("OpenTK.GameWindow.GetForm is not working the same way since the MonoGame 3.5 updates, so it is longer working for now.");

//            Type type = typeof(OpenTKGameWindow);
//
//            System.Reflection.FieldInfo field = type.GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//            if (field != null)
//                return field.GetValue(gameWindow) as OpenTK.GameWindow;
//
//            return null;
        }
    }
}