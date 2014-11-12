using Microsoft.Xna.Framework;
using System;
namespace JPEngine.ECS.Systems
{
    public abstract class System : ISystem
    {
        public virtual void Update(GameTime gameTime) { }
    }
}