using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace JPEngine.Utils
{
    [Serializable]
    public class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, INotifyCollectionChanged,
        INotifyPropertyChanged
    {
        public ObservableDictionary()
        {
        }

        public ObservableDictionary(int capacity) : base(capacity)
        {
        }

        public ObservableDictionary(IEqualityComparer<TKey> comparer) : base(comparer)
        {
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary)
        {
        }

        public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer)
        {
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
            : base(dictionary, comparer)
        {
        }

        public ObservableDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public new TValue this[TKey key]
        {
            get { return base[key]; }
            set
            {
                TValue oldValue;
                bool exist = base.TryGetValue(key, out oldValue);
                var oldItem = new KeyValuePair<TKey, TValue>(key, oldValue);
                base[key] = value;
                var newItem = new KeyValuePair<TKey, TValue>(key, value);
                if (exist)
                {
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace,
                        newItem, oldItem, base.Keys.ToList().IndexOf(key)));
                }
                else
                {
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItem,
                        base.Keys.ToList().IndexOf(key)));
                    OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                }
            }
        }

        [field: NonSerialized]
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public new void Add(TKey key, TValue value)
        {
            if (!base.ContainsKey(key))
            {
                var item = new KeyValuePair<TKey, TValue>(key, value);
                base.Add(key, value);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item,
                    base.Keys.ToList().IndexOf(key)));
                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            }
        }

        public new bool Remove(TKey key)
        {
            TValue value;
            if (base.TryGetValue(key, out value))
            {
                var item = new KeyValuePair<TKey, TValue>(key, base[key]);
                bool result = base.Remove(key);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item,
                    base.Keys.ToList().IndexOf(key)));
                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                return result;
            }
            return false;
        }

        public new void Clear()
        {
            base.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
    }
}