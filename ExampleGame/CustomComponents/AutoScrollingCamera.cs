using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JPEngine.Components;
using JPEngine.Entities;
using Microsoft.Xna.Framework;

namespace ExampleGame.CustomComponents
{
    internal class AutoScrollingCamera : BaseComponent
    {

        public float Speed = 10;
        public bool IsHorizontal = true; //TODO: Make an enum...

        public bool IsScrolling = true;

        public AutoScrollingCamera(Entity entity) 
            : base(entity)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (IsScrolling)
            {
                float deltaSinceLastUpdate = gameTime.ElapsedGameTime.Milliseconds / 1000f;

                if (IsHorizontal)
                    Transform.Position.X += Speed * deltaSinceLastUpdate;
                else
                    Transform.Position.Y += Speed * deltaSinceLastUpdate;
            }
        }
    }
}
