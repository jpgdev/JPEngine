using System;
//using Newtonsoft.Json;

namespace JPEngine.Managers
{
    public abstract class Setting
    {
        private readonly string _name;
        private object _value;

        protected Setting(string name, object value)
        {
            _name = name;
            _value = value;
        }

        public string Name
        {
            get { return _name; }
        }

        //[JsonIgnore]
        public virtual Type ValueType
        {
            get { return typeof (object); }
        }

        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (ValueChanged != null)
                    ValueChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler<EventArgs> ValueChanged;
    }


    public class Setting<T> : Setting
    {
        //public new T Value
        //{
        //    get { return (T)base.Value; }
        //    set { base.Value = value; }
        //}

        public Setting(string name, T value)
            : base(name, value)
        {
        }

        public override Type ValueType
        {
            get { return typeof (T); }
        }
    }
}