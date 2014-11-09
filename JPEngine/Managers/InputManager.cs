using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEngine.Managers
{
    public class InputManager : Manager
    {
        private KeyboardState newKBState;
        private KeyboardState oldKBState;
        private MouseState oldMouseState;
        private MouseState newMouseState;
        
        internal InputManager()
        {
        }
        
        internal override void Update(GameTime gameTime)
        {
            oldKBState = newKBState;
            newKBState = Keyboard.GetState();

            oldMouseState = newMouseState;
            newMouseState = Mouse.GetState();
        }

        public Vector2 MousePosition
        {
            get { return new Vector2(newMouseState.X, newMouseState.Y); }
        }
        
        public bool IsKeyClicked(Keys key)
        {
            return (newKBState.IsKeyDown(key) && oldKBState.IsKeyUp(key));
        }

        public bool IsKeyDown(Keys key)
        {
            return newKBState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return newKBState.IsKeyUp(key);
        }

        public bool LeftMouseButtonClicked()
        {
            return (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released);
        }

        public bool LeftMouseButtonDown()
        {
            return (newMouseState.LeftButton == ButtonState.Pressed);
        }

        public bool RightMouseButtonClicked()
        {
            return (newMouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released);
        }
        public bool RightMouseButtonDown()
        {
            return (newMouseState.RightButton == ButtonState.Pressed);
        }
    }
}

