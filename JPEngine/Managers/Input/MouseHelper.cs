using System;
using JPEngine.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JPEngine.Managers.Input
{
    public class MouseHelper : IMouseHelper
    {
        private MouseState _oldMouseState;
        private MouseState _currentMouseState;

        public Point MousePosition
        {
            get { return _currentMouseState.Position; }
        }

        public int ScrollWheelValue
        {
            get { return _currentMouseState.ScrollWheelValue; }
        }

        public void Update()
        {
            _oldMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
        }

        public bool IsButtonClicked(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return (_currentMouseState.LeftButton == ButtonState.Pressed &&
                            _oldMouseState.LeftButton == ButtonState.Released);

                case MouseButton.Right:
                    return (_currentMouseState.RightButton == ButtonState.Pressed &&
                            _oldMouseState.RightButton == ButtonState.Released);

                case MouseButton.Middle:
                    return (_currentMouseState.MiddleButton == ButtonState.Pressed &&
                            _oldMouseState.MiddleButton == ButtonState.Released);

                default:
                    throw new NotImplementedException(string.Format("The button {0} is not implemented", button));
            }
        }

        public bool IsButtonReleased(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return (_currentMouseState.LeftButton == ButtonState.Released &&
                            _oldMouseState.LeftButton == ButtonState.Pressed);

                case MouseButton.Right:
                    return (_currentMouseState.RightButton == ButtonState.Released &&
                            _oldMouseState.RightButton == ButtonState.Pressed);

                case MouseButton.Middle:
                    return (_currentMouseState.MiddleButton == ButtonState.Released &&
                            _oldMouseState.MiddleButton == ButtonState.Pressed);

                default:
                    throw new NotImplementedException(string.Format("The button {0} is not implemented", button));
            }
        }

        public bool IsButtonDown(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return _currentMouseState.LeftButton == ButtonState.Pressed;

                case MouseButton.Right:
                    return _currentMouseState.RightButton == ButtonState.Pressed;

                case MouseButton.Middle:
                    return _currentMouseState.MiddleButton == ButtonState.Pressed;

                default:
                    throw new NotImplementedException(string.Format("The button {0} is not implemented", button));
            }
        }
    }
}
