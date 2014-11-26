using System;
using System.Collections.Generic;
using JPEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Components
{

    public class SpriteAnimation
    {
        //private string _name;
        private double _timeThisFrame;
        private int _currentFrame;
        private readonly int _frameWidth;
        private readonly int _frameHeight;
        private readonly int _totalFrames;
        private readonly int _totalWidth;
        private readonly int _totalHeight;

        private Vector2 _origin;

        private readonly int _xOffset;
        private readonly int _yOffset;

        //TODO: Test the vertical flow
        private readonly bool _isFlowHorizontal = true;

        private Rectangle _currentRect;

        public bool IsActive;
        public bool IsLooping = true;
        public float FrameDuration = 0.15f;

        public Rectangle CurrentFrame
        {
            get { return _currentRect; }
        }

        public int FrameWidth
        {
            get { return _frameWidth; }
        }

        public int FrameHeight
        {
            get { return _frameHeight; }
        }

        public Vector2 Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        public SpriteAnimation(int frameWidth, int frameHeight, int totalFrames, int totalWidth, int totalHeight, int xOffset = 0, int yOffset = 0, bool isFlowHorizontal = true)
        {
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _totalFrames = totalFrames;
            _totalWidth = totalWidth;
            _totalHeight = totalHeight;

            _yOffset = yOffset;
            _xOffset = xOffset;

            _isFlowHorizontal = isFlowHorizontal;

            _currentRect = new Rectangle(xOffset, yOffset, frameWidth, frameHeight);
            _origin = new Vector2(_frameWidth/2, _frameHeight/2);
        }

        public void Reset()
        {
            _currentFrame = 0;
            _timeThisFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (!IsActive)
                return;

            _timeThisFrame += gameTime.ElapsedGameTime.TotalSeconds;
            if (_timeThisFrame >= FrameDuration)
            {
                if (IsLooping)
                    _currentFrame = (_currentFrame + 1) % _totalFrames;
                else
                    _currentFrame = Math.Min(_currentFrame + 1, _totalFrames);

                _timeThisFrame = 0;

                //TODO: Check if flow is vertical or horizontal
                int xPos;
                int yPos;
                if (_isFlowHorizontal)
                {
                    xPos = (_currentFrame * _frameWidth) % _totalWidth;
                    yPos = (_currentFrame * _frameWidth) / _totalWidth;
                }
                else
                {
                    xPos = (_currentFrame * _frameHeight) / _totalHeight;
                    yPos = (_currentFrame * _frameHeight) % _totalHeight;
                }

                _currentRect.X = _xOffset + xPos;
                _currentRect.Y = _yOffset + yPos;
                _currentRect.Width = _frameWidth;
                _currentRect.Height = _frameHeight;
            }
        }
    }


    public class AnimatedSpriteComponent : SpriteComponent
    {
        private readonly Dictionary<string, SpriteAnimation> _animations = new Dictionary<string, SpriteAnimation>();
        private SpriteAnimation _currentAnimation;
        
        public SpriteAnimation CurrentAnimation
        {
            get { return _currentAnimation; }
        }

        //public new Rectangle? DrawnPortion
        //{
        //    //get { return _currentAnimation.CurrentFrame; }
        //    get
        //    {
        //        if (_currentAnimation == null)
        //            return null;

        //        return _currentAnimation.CurrentFrame;
        //    }
        //}

        public AnimatedSpriteComponent(Entity gameObject, Texture2D texture)
            : base(gameObject, texture)
        {
        }


        public bool AddAnimation(string name, SpriteAnimation animation)
        {
            if (_animations.ContainsKey(name))
                return false;
            
            _animations.Add(name, animation);
            
            return true;
        }

        public bool RemoveAnimation(string name)
        {
            return _animations.Remove(name);
        }

        public bool SetCurrentAnimation(string name)
        {
            if (!_animations.ContainsKey(name))
                return false;

            _currentAnimation = _animations[name];
            _currentAnimation.IsActive = true;

            Origin = _currentAnimation.Origin;

            return true;
        }

        public override void Update(GameTime gameTime)
        {
            if(_currentAnimation != null)
                _currentAnimation.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (_currentAnimation != null)
                DrawnPortion = _currentAnimation.CurrentFrame;

            //base.Draw(spriteBatch, gameTime);
            base.Draw(gameTime);
        }


        //public static float GetZDelta()
        //{
        //    return (float)_rand.Next(1, 10000) / 1000000.0f;
        //}

        //private void UpdateZIndex()
        //{
        //    float z = 0.0f; //Note: 0.0f = front, 1.0f = back.
        //    z = 1.0f - (GameObject.Transform.Position.Y + Height);
        //    //z = 1.0f - ((float)(GameObject.Transform.Position.Y + Height) / (float)(TileMap.Height * Engine.TileHeight));

        //    z += _zDelta;
        //    ZIndex = Math.Min(z, 0.999f);
        //}
    }
}