using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JPEngine.Managers
{
    public class InputManager : Manager
    {
        private KeyboardState newKBState;
        private MouseState newMouseState;
        private KeyboardState oldKBState;
        private MouseState oldMouseState;

        internal InputManager()
        {
        }

        public Vector2 MousePosition
        {
            get { return new Vector2(newMouseState.X, newMouseState.Y); }
        }

        internal override void Update(GameTime gameTime)
        {
            oldKBState = newKBState;
            newKBState = Keyboard.GetState();

            oldMouseState = newMouseState;
            newMouseState = Mouse.GetState();
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
            return (newMouseState.RightButton == ButtonState.Pressed &&
                    oldMouseState.RightButton == ButtonState.Released);
        }

        public bool RightMouseButtonDown()
        {
            return (newMouseState.RightButton == ButtonState.Pressed);
        }
    }
}