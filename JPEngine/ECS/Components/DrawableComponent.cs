using System;
using JPEngine.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.ECS.Components
{
    public class DrawableComponent : EntityComponent, IEntityDrawable
    {
        private DrawingLayer _drawingLayer = DrawingLayer.Default;
        private bool _visible = true;

        public DrawableComponent(Entity gameObject)
            : base(gameObject)
        {
            VisibleChanged += OnVisibleChanged;
            LayerChanged += OnLayerChanged;
        }


        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> LayerChanged;

        //TODO: Remove the NotImplementedException
        //public virtual void Draw() { throw new NotImplementedException(); }
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        #region  Event Handler methods

        protected virtual void OnVisibleChanged(object sender, EventArgs e) { }

        protected virtual void OnLayerChanged(object sender, EventArgs e) { }

        #endregion

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
        
        //public int DrawOrder
        //{
        //    get { return _drawOrder; }
        //    set
        //    {
        //        if (_drawOrder != value)
        //        {
        //            _drawOrder = Math.Max(0, value);
        //            if (DrawOrderChanged != null)
        //                DrawOrderChanged(this, EventArgs.Empty);
        //        }
        //    }
        //}

        public DrawingLayer Layer
        {
            get { return _drawingLayer; }
            set
            {
                if (_drawingLayer != value)
                {
                    //Check that the layer is a valid one
                    if (!Enum.IsDefined(typeof (DrawingLayer), value)) 
                        return;

                    //_drawingLayer = (DrawingLayer)MathHelper.Clamp(0, (int)value);
                    //_drawingLayer = Math.Max(0, (int)value);
                    _drawingLayer = value;
                    if (LayerChanged != null)
                        LayerChanged(this, EventArgs.Empty);
                }
            }
        }

        #endregion
    }
}