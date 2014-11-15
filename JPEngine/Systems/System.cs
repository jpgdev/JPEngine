using Microsoft.Xna.Framework;
using System;
namespace JPEngine.Entities.Systems
{
    public abstract class System : ISystem
    {
        public virtual void Update(GameTime gameTime) { }
    }
}