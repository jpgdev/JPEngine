using System;
using Microsoft.Xna.Framework.Input;

namespace JPEngine.Events
{
    public class KeyEventArgs : EventArgs
    {
        public Keys Key { get; set; }
        public bool Control { get; set; }
        public bool Shift { get; set; }
        public bool Alt { get; set; }
        public bool Caps { get; set; }

        public KeyEventArgs()
            :this (Keys.None)
        {
        }

        public KeyEventArgs(Keys key, bool control = false, bool shift = false, bool alt = false, bool caps = false)
        {
            Key = key;
            Control = control;
            Shift = shift;
            Alt = alt;
            Caps = caps;
        }
    }
}