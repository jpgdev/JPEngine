using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEngine.Events
{
    public enum ItemAction
    {
        Added,
        Updated,
        Removed
    }

    public class ItemEventArgs<T> : EventArgs
    {
        public T ItemChanged;
        public ItemAction ItemAction;

        public ItemEventArgs(T itemChanged, ItemAction action)
        {
            ItemChanged = itemChanged;
            ItemAction = action;
        }
    }

    public class ItemsEventArgs<T> : EventArgs
    {
        public T[] ItemsChanged;
        public ItemAction ItemAction;

        public ItemsEventArgs(T[] itemsChanged, ItemAction action)
        {
            ItemsChanged = itemsChanged;
            ItemAction = action;
        }
    }
}
