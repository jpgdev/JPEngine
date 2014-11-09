using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEngine.ECS.Components
{
    public interface IEntityDrawable
    {
        bool Visible { get; }

        int DrawOrder { get; }

        event EventHandler<EventArgs> VisibleChanged;

        event EventHandler<EventArgs> DrawOrderChanged;

        //void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        void Draw();
    }
}
