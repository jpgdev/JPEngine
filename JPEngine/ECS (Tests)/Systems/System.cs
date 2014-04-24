using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEngine.ECS
{
    internal abstract class ISystem
    {
        public abstract void Update(GameTime gameTime, EntityManager manager);
    }
}
