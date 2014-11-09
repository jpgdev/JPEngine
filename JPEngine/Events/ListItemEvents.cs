using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public T ListItemChanged;
        public ListItemAction ListItemAction;

        public ListItemEventArgs(T itemChanged, ListItemAction action)
        {
            ListItemChanged = itemChanged;
            ListItemAction = action;
        }
    }

    public class ListItemsEventArgs<T> : EventArgs
    {
        public T[] ListItemsChanged;
        public ListItemAction ListItemAction;

        public ListItemsEventArgs(T[] itemsChanged, ListItemAction action)
        {
            ListItemsChanged = itemsChanged;
            ListItemAction = action;
        }
    }
}
