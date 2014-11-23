using JPEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Components
{
    public class RectCollider : BaseComponent
    {
        public Vector2 Offset { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public bool IsSolid { get; set; }

        //TODO: Handle rotation + scale??
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    (int)(Transform.Position.X + Offset.X) - (Width / 2),
                    (int)(Transform.Position.Y + Offset.Y) - (Height / 2),
                    Width,
                    Height);
            }
        }

        public RectCollider(Entity entity) 
            : base(entity)
        {
            Offset = Vector2.Zero;
            Width = 10;
            Height = 10;
            IsSolid = true;
        }

        protected override void StartCore()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}