using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEngine.ECS.Components
{
    public interface IEntityUpdateable
    {
        bool Enabled { get; }

        int UpdateOrder { get; }

        event EventHandler<EventArgs> EnabledChanged;

        event EventHandler<EventArgs> UpdateOrderChanged;

        void Update(GameTime gameTime);
    }
}
