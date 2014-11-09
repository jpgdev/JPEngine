using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Managers
{
    public class SpriteBatchManager : Manager
    {
        private readonly SpriteBatch _spriteBatch;

        internal SpriteBatchManager(GraphicsDevice graphicsDevice)
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        internal void Update(GameTime gameTime)
        {
        }

        //internal void Draw(GameTime gameTime)
        //{
        //    _spriteBatch.Begin();

        //    DrawCore(_spriteBatch, gameTime);

        //    _spriteBatch.End();
        //}

        //private void DrawCore(SpriteBatch _spriteBatch, GameTime gameTime)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Draw(DrawableComponent component, Texture2D texture, Vector2 position)
        //{
        //    //TODO: Create a list of Sprites to draw and go trhough them all in the Draw call?

        //    //TODO: Get correct z-index etc...
        //    float z_index = 1f;

        //}

        internal SpriteBatch Begin()
        {
            //todo: Validation that it has not already Began

            _spriteBatch.Begin(SpriteSortMode.BackToFront, null);
            //_spriteBatch.Begin(SpriteSortMode.BackToFront, null);
            //Not implemented : Layer use this with the Camera Matrix
            //_spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, DepthStencilState.Default, null, null, new Matrix());

            return _spriteBatch;
        }

        internal void End()
        {
            //todo: Validation that it has already bagan
            _spriteBatch.End();
        }
    }
}