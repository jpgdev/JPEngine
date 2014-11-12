using Microsoft.Xna.Framework;

namespace JPEngine.ECS.Components
{
    public class RectCollider : EntityComponent
    {
        //TODO: Debug purposes
        private RectRenderer _rectRenderer;

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

        public override void Start()
        {
            //TODO: To debug
            _rectRenderer = GameObject.GetComponent<RectRenderer>();
            if (_rectRenderer == null)
            {
                _rectRenderer = new RectRenderer(GameObject, Bounds, Engine.Window.CreateTexture(1, 1));
                GameObject.AddComponent(_rectRenderer);
            }
            //
        }

        public override void Update(GameTime gameTime)
        {
            //TODO: To debug
            _rectRenderer.RectangleToRender = Bounds;
        }
    }
}