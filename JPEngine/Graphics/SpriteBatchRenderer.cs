using System;
using System.Collections.Generic;
using JPEngine.Enums;
using JPEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Graphics
{
    public class SpriteBatchRenderer : Manager, ISpriteRenderer<Texture2D, SpriteFont>
    {
        private readonly List<ISprite<Texture2D>> _sprites = new List<ISprite<Texture2D>>();
        private readonly List<IText<SpriteFont>> _texts = new List<IText<SpriteFont>>();

        private readonly int _numberOfLayers;

        private readonly SpriteBatch _spriteBatch;
        private bool _isBegan;

        public SpriteBatchRenderer(GraphicsDevice graphicsDevice)
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _numberOfLayers = Enum.GetValues(typeof (DrawingLayer)).Length;
        }

        public void Begin(Matrix? transformMatrix)
        {
            if (_isBegan)
                throw new Exception("The Begin has already been called.");

            if(transformMatrix.HasValue)
                _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, transformMatrix.Value);
            else
                _spriteBatch.Begin(SpriteSortMode.Immediate, null);

            _isBegan = true;
        }

        public void DrawString(IText<SpriteFont> text)
        {
            //TODO: Sort
            _texts.Add(text);
        }

        public void Draw(ISprite<Texture2D> sprite)
        {
            //TODO: Sort
            _sprites.Add(sprite);
        }

        public void End()
        {
            if(!_isBegan)
                throw new Exception("The Begin needs to be called before the End() is called.");
            
            //TODO: Sort here maybe?

            foreach (ISprite<Texture2D> sprite in _sprites)
            {
                if (sprite.Bounds != Rectangle.Empty)
                {
                    _spriteBatch.Draw(
                       sprite.Texture,
                       sprite.Bounds,
                       sprite.DrawnPortion,
                       sprite.Color,
                       sprite.Rotation,
                       sprite.Origin,
                       SpriteEffects.None,
                       (int)sprite.Layer / _numberOfLayers);
                }
                else
                {
                    _spriteBatch.Draw(
                       sprite.Texture,
                       sprite.Position,
                       sprite.DrawnPortion,
                       sprite.Color,
                       sprite.Rotation,
                       sprite.Origin,
                       sprite.Scale,
                       SpriteEffects.None,
                       (int)sprite.Layer / _numberOfLayers);
                }
            }

            foreach (IText<SpriteFont> text in _texts)
            {
                _spriteBatch.DrawString(
                    text.Font,
                    text.Text,
                    text.Position,
                    text.Color);

                //_spriteBatch.DrawString(
                //    text.Font,
                //    text.Text,
                //    text.Position,
                //    text.Color,
                //    text.Rotation,
                //    text.Origin,
                //    text.Scale,
                //    SpriteEffects.None,
                //    (int)text.Layer / _numberOfLayers);
            }

            _spriteBatch.End();

            _isBegan = false;

            _sprites.Clear();
            _texts.Clear();
        }

        public void Dispose()
        {
            //TODO: Dispose...
            _sprites.Clear();
            _texts.Clear();
        }
    }
}
