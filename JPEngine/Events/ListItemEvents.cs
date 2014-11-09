using System;

namespace JPEngine.Events
{
    public enum ListItemAction
    {
        Added,
        Updated,
        Removed
    }

    public class ListItemEventArgs<T> : EventArgs
    {
        public ListItemAction ListItemAction;
        public T ListItemChanged;

        public ListItemEventArgs(T itemChanged, ListItemAction action)
        {
            ListItemChanged = itemChanged;
            ListItemAction = action;
        }
    }

    public class ListItemsEventArgs<T> : EventArgs
    {
        public ListItemAction ListItemAction;
        public T[] ListItemsChanged;

        public ListItemsEventArgs(T[] itemsChanged, ListItemAction action)
        {
            ListItemsChanged = itemsChanged;
            ListItemAction = action;
        }
    }
}