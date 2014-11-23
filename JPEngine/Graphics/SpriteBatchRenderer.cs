using System;
using JPEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Graphics
{
    public class SpriteBatchRenderer : ISpriteRenderer
    {
        //private struct Sprite
        //{
        //    public ISprite Sprite;
        //}

        private readonly SpriteBatch _spriteBatch;
        private bool _isBegan = false;

        public SpriteBatchRenderer(GraphicsDevice graphicsDevice)
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void Begin(Matrix? transformMatrix)
        {
            if (_isBegan)
                return;

            if(transformMatrix.HasValue)
                _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, transformMatrix.Value);
            else
                _spriteBatch.Begin(SpriteSortMode.Immediate, null);

            _isBegan = true;
        }

        public void Draw(ISprite sprite)
        {
            //TODO: Add the sprite to the sorted list of sprites
            throw new NotImplementedException();
        }

        public void End()
        {
            //TODO: Draw all the sprites
            throw new NotImplementedException();
            
        }
    }
}
