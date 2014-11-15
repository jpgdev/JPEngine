﻿using JPEngine.Components;
using JPEngine.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JPEngine.Managers
{

    //TODO: Make this class a SpirteBatch wrapper, replace all the SpriteBatch.Draw() methods and remove the Z-Index to make sure it is well done
    //TODO: Implement SpriteBatch.DrawString too?
    public class SpriteBatchManager : Manager
    {
        private const int STEPS_PER_LAYER = 2048;
        private int _numberOfLayers;
        private readonly SpriteBatch _spriteBatch;

        internal SpriteBatchManager(GraphicsDevice graphicsDevice)
        {
            if (graphicsDevice == null)
                throw new NullReferenceException("The graphicsDevice cannot be null.");

            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        protected override bool InitializeCore()
        {
            _numberOfLayers = Enum.GetNames(typeof(DrawingLayer)).Length;

            return true;
        }

        public float GetZIndex(DrawableSpriteComponent drawableSprite)
        {
            //TODO: make this work with the Z, Y axis and the Layers

            //float lowestPosition = (drawableSprite.Transform.Position.Y +
            //                        drawableSprite.Texture.Height * drawableSprite.Transform.Scale.Y);
            ////Math.Max(drawableSprite.Texture.Width * drawableSprite.Transform.Scale.X, drawableSprite.Texture.Height * drawableSprite.Transform.Scale.Y)); //TODO: Update this with the rotation and scale...
            //float zIndex = MathHelper.Clamp(1f, 0f, 1.0f - (lowestPosition - Engine.Cameras.Current.Transform.Position.Y) / Engine.WindowManager.ScreenHeight);

            //float min = (MathHelper.Max(0, (int)drawableSprite.Layer - 1) * STEPS_PER_LAYER);
            //float max = (int)drawableSprite.Layer * STEPS_PER_LAYER;
            //////float zIndex = MathHelper.Min(max, min - drawableSprite.Transform.Position.Z) / (_numberOfLayers * STEPS_PER_LAYER);
            //zIndex +=  MathHelper.Min(max, min - drawableSprite.Transform.Position.Z) / (_numberOfLayers * STEPS_PER_LAYER);


            float min = (MathHelper.Max(0, (int)drawableSprite.Layer - 1) * STEPS_PER_LAYER);
            float max = (int)drawableSprite.Layer * STEPS_PER_LAYER;
            ////float zIndex = MathHelper.Min(max, min - drawableSprite.Transform.Position.Z) / (_numberOfLayers * STEPS_PER_LAYER);
            float zIndex = MathHelper.Min(max, min - drawableSprite.Transform.Position.Z) / (_numberOfLayers * STEPS_PER_LAYER);


            return MathHelper.Clamp(1f, 0f, zIndex);
        }

        //TODO: Draw Backlayer, MidLayer then FrontLayer
        //TODO: GUILayer = another Begin, Draw, End loop, can be the same batch, but does not follow the camera

        internal SpriteBatch Begin()
        {
            //TODO: Add a validation that it can be started (begin has not been called already this frame)
            
            CameraComponent camera = Engine.Cameras.Current;
            if (camera != null)
            {
                _spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.TransformMatrix);
                //throw new NullReferenceException("The Engine current camera has not been set.");
            }
            else
            {
                _spriteBatch.Begin(SpriteSortMode.BackToFront, null);
            }
                
                
            //_spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, DepthStencilState.Default, null, null, camera.TransformMatrix);
            //_spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.TransformMatrix);

            return _spriteBatch;
        }

        internal void End()
        {
            //TODO: If I want to sort them manually it would be either here or when they are added
            _spriteBatch.End();
        }
    }
}