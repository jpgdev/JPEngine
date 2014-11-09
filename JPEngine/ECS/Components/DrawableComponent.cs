using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEngine.ECS.Components
{
    public class DrawableComponent : EntityComponent, IEntityDrawable
    {

        private bool _visible = true;
        private int _drawOrder;

        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> DrawOrderChanged;

#region Properties

        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    if (VisibleChanged != null)
                        VisibleChanged(this, EventArgs.Empty);
                }
            }
        }

        //TODO: Rename to Layer?
        public int DrawOrder
        {
            get { return _drawOrder; }
            set
            {
                if (_drawOrder != value)
                {
                    _drawOrder = value;
                    if (DrawOrderChanged != null)
                        DrawOrderChanged(this, EventArgs.Empty);
                }
            }
        }

#endregion

        public DrawableComponent(Entity gameObject)
            : base(gameObject)
        {
            VisibleChanged += OnVisibleChanged;
            DrawOrderChanged += OnDrawOrderChanged;
        }
        
        //TODO: Remove the NotImplementedException
        public virtual void Draw() { throw new NotImplementedException(); }

#region  Event Handler methods

        protected virtual void OnVisibleChanged(object sender, EventArgs e) { }

        protected virtual void OnDrawOrderChanged(object sender, EventArgs e) { }

#endregion

    }
}
