using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.ECS.Components
{
    public class DrawableComponent : EntityComponent, IEntityDrawable
    {
        private int _drawOrder;
        private bool _visible = true;

        public DrawableComponent(Entity gameObject)
            : base(gameObject)
        {
            VisibleChanged += OnVisibleChanged;
            DrawOrderChanged += OnDrawOrderChanged;
        }

        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> DrawOrderChanged;

        //TODO: Remove the NotImplementedException
        //public virtual void Draw() { throw new NotImplementedException(); }
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        #region  Event Handler methods

        protected virtual void OnVisibleChanged(object sender, EventArgs e)
        {
        }

        protected virtual void OnDrawOrderChanged(object sender, EventArgs e)
        {
        }

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

        //TODO: Rename to Layer?
        //TODO: Need a list of layers or a Max of layers for the Z-Index (see DrawableSpriteComponent.Draw())
        //TODO: Actually use it with the Z-Index?
        //TODO: Create a List of layers ? ex. Dictionnary<int, string> Layers;
        //TODO: OR Create a static enum that contains the layers: enum Layers { Foreground1 = 0, Foreground2 = 1, Game1 = 2, Game2 = 3, Background1 = 4, Background2 = 5 }

        public int DrawOrder
        {
            get { return _drawOrder; }
            set
            {
                if (_drawOrder != value)
                {
                    _drawOrder = Math.Max(0, value);
                    if (DrawOrderChanged != null)
                        DrawOrderChanged(this, EventArgs.Empty);
                }
            }
        }

        #endregion
    }
}