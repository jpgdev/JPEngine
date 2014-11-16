﻿using System;
using JPEngine.Events;

namespace JPEngine.Components
{
    public interface IComponent
    {
        bool Enabled { get; }

        string Tag { get; }

        void Initialize();

        void Start();

        event EventHandler<ValueChangedEventArgs<bool>> EnabledChanged;
    }
}