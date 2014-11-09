using JPEngine.Events;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEngine.Managers
{

    public abstract class Setting
    {
        private string _name;
        private object _value;

        public event EventHandler<EventArgs> ValueChanged;

        public string Name
        {
            get { return _name; }
        }

        public virtual Type ValueType 
        { 
            get { return typeof(object); }
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

        public Setting(string name, object value)
        {
            _name = name;
            _value = value;
        }
    }


    public class Setting<T> : Setting
    {
        //public new T Value
        //{
        //    get { return (T)base.Value; }
        //    set { base.Value = value; }
        //}

        public override Type ValueType
        {
            get { return typeof(T); }
        }

        public Setting(string name, T value)
            : base(name, value)
        {
        }
    }
}
