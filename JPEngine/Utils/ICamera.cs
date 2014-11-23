using System;
using JPEngine.Events;
using Microsoft.Xna.Framework;

namespace JPEngine
{
    public interface ICamera
    {
        string Tag { get; set; }

        Vector2 Position { get; set; }

        float Rotation { get; set; }

        Vector2 Scale { get; set; }

        Vector2 Origin { get; }

        Matrix TransformMatrix { get; }

        event EventHandler<ValueChangedEventArgs<string>> TagChanged;
    }
}
