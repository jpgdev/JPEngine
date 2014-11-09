using System;

namespace JPEngine.Events
{
    public class ValueChangedEventArgs<T> : EventArgs
    {
        public T NewValue;
        public T OldValue;

        public ValueChangedEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}