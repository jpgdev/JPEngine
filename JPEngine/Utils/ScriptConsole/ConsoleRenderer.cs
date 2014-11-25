using System;
using System.Collections.Generic;
using System.Linq;
using JPEngine.Enums;
using JPEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Utils.ScriptConsole
{
    internal class ConsoleRenderer : IDisposable
    {
        private enum State
        {
            Opened,
            Opening,
            Closed,
            Closing
        }
        
        private readonly ConsoleInputProcessor _consoleInputProcessor;
        private readonly ConsoleOptions _options;
        private readonly Texture2D _pixel;

        private readonly float _oneCharacterWidth;
        private readonly int _maxCharactersPerLine;

        private Vector2 _position;
        private Vector2 _firstCommandPositionOffset = Vector2.Zero;

        private Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)_position.X, (int)_position.Y, _options.Width - (_options.Margin * 2), _options.Height);
            }
        }

        private Rectangle InnerBounds
        {
            get
            {
                return new Rectangle(Bounds.X + _options.Padding, Bounds.Y + _options.Padding, Bounds.Width - _options.Padding, Bounds.Height);
            }
        }

        private Vector2 FirstCommandPosition
        {
            get
            {
                return new Vector2(InnerBounds.X, InnerBounds.Y) + _firstCommandPositionOffset;
            }
        }

        internal ConsoleRenderer(ConsoleInputProcessor consoleInputProcessor, ConsoleOptions options)
        {
            _consoleInputProcessor = consoleInputProcessor;
            _options = options;

            
            _pixel = new Texture2D(Engine.Window.GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });

            _position = new Vector2(_options.Margin, 0);

            _oneCharacterWidth = _options.Font.MeasureString("X").X;
            _maxCharactersPerLine = (int)((Bounds.Width - _options.Padding * 2) / _oneCharacterWidth);
        }

        private static IEnumerable<string> SplitCommand(string command, int max)
        {
            var lines = new List<string>();
            while (command.Length > max)
            {
                var splitCommand = command.Substring(0, max);
                lines.Add(splitCommand);
                command = command.Substring(max, command.Length - max);
            }
            lines.Add(command);
            return lines;
        }

        private void ValidateFirstCommandPosition(float nextCommandY)
        {
            if (!IsInBounds(nextCommandY))
            {
                _firstCommandPositionOffset.Y -= _options.Font.LineSpacing;
            }
        }

        private bool IsInBounds(float yPosition)
        {
            return yPosition < _options.Height;
        }
        
        #region Drawing Methods

        internal void Draw(GameTime gameTime)
        {
            if (!_consoleInputProcessor.IsActive) return;

            Engine.SpriteRenderer.Begin();

            //spriteBatch.Begin();

            //TODO: Keep only one sprite and change it, do not recreate one
            Sprite backGroundSprite = new Sprite(_pixel, Bounds, _options.BackgroundColor, DrawingLayer.Foreground1);

            Engine.SpriteRenderer.Draw(backGroundSprite);
            //spriteBatch.Draw(_pixel, Bounds, _options.BackgroundColor);

            Vector2 nextCommandPosition = DrawCommands(_consoleInputProcessor.Out, FirstCommandPosition);
            nextCommandPosition = DrawPrompt(nextCommandPosition);
            DrawSelection(nextCommandPosition);
            var bufferPosition = DrawCommand(_consoleInputProcessor.Buffer.Value, nextCommandPosition,
                _options.BufferColor); //Draw the buffer
            
            DrawCursor(bufferPosition, gameTime);

            Engine.SpriteRenderer.End();

            //spriteBatch.End();
        }

        private Vector2 DrawPrompt(Vector2 pos)
        {
            RenderedText text = new RenderedText
            {
                Font = _options.Font,
                Text = _options.Prompt,
                Position = pos,
                Color = _options.PromptColor,
                Layer = DrawingLayer.Foreground2
            };

            Engine.SpriteRenderer.DrawString(text);

            //spriteBatch.DrawString(_options.Font, _options.Prompt, pos, _options.PromptColor);
            pos.X += _oneCharacterWidth * _options.Prompt.Length + _oneCharacterWidth;

            return pos;
        }

        private Vector2 DrawCommand(string command, Vector2 pos, Color color)
        {
            //Take into account the prompt character and the space left right after
            int maxCommandLength = _maxCharactersPerLine - _options.Prompt.Length - 1;

            var splitLines = command.Length > maxCommandLength
                ? SplitCommand(command, maxCommandLength)
                : new[] {command};
            foreach (var line in splitLines)
            {
                if (IsInBounds(pos.Y))
                {
                    RenderedText text = new RenderedText
                    {
                        Font = _options.Font,
                        Text = line,
                        Position = pos,
                        Color = color,
                        Layer = DrawingLayer.Foreground2
                    };

                    Engine.SpriteRenderer.DrawString(text);

                    //spriteBatch.DrawString(_options.Font, line, pos, color);
                }

                ValidateFirstCommandPosition(pos.Y + _options.Font.LineSpacing);
                pos.Y += _options.Font.LineSpacing;
            }
            return pos;
        }

        /// <summary>
        /// Draws the specified collection of commands and returns the position of the next command to be drawn
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private Vector2 DrawCommands(IEnumerable<OutputLine> lines, Vector2 pos)
        {
            var originalX = pos.X;
            foreach (var command in lines)
            {
                if (command.Type == OutputLineType.Command)
                {
                    pos = DrawPrompt(pos);
                }
                //position.Y = DrawCommand(command.ToString(), position, GameConsoleOptions.Options.FontColor).Y;
                pos.Y =
                    DrawCommand(command.ToString(), pos,
                        command.Type == OutputLineType.Command
                            ? _options.PastCommandColor
                            : _options.PastCommandOutputColor).Y;
                pos.X = originalX;
            }
            return pos;
        }

        private void DrawCursor(Vector2 pos, GameTime gameTime)
        {
            if (!IsInBounds(pos.Y))
                return;

            int max = Math.Min(_consoleInputProcessor.Buffer.CursorPosition, _consoleInputProcessor.Buffer.Value.Length);
            string subString = _consoleInputProcessor.Buffer.Value.Substring(0, max );

            pos.X += _options.Font.MeasureString(subString).X;
            pos.Y -= _options.Font.LineSpacing;


            RenderedText text = new RenderedText();
            text.Font = _options.Font;
            text.Text = (int)(gameTime.TotalGameTime.TotalSeconds / _options.CursorBlinkSpeed) % 2 == 0
                            ? _options.Cursor
                            : "";
            text.Position = pos;
            text.Color = _options.CursorColor;

            Engine.SpriteRenderer.DrawString(text);
            
            //spriteBatch.DrawString(_options.Font,
            //    (int) (gameTime.TotalGameTime.TotalSeconds/_options.CursorBlinkSpeed)%2 == 0
            //        ? _options.Cursor
            //        : "", pos, _options.CursorColor);
        }

        private void DrawSelection(Vector2 pos)
        {
            int selectionLength = _consoleInputProcessor.Buffer.SelectionEnd -
                                  _consoleInputProcessor.Buffer.SelectionStart;
            
            Rectangle rect = 
                new Rectangle(
                    (int)(pos.X + _consoleInputProcessor.Buffer.SelectionStart * _oneCharacterWidth),
                    (int)pos.Y,
                    (int)(selectionLength * _oneCharacterWidth),
                    _options.Font.LineSpacing
                );

            //TODO: Keep only one sprite and change it, do not recreate one
            Sprite selectionSprite = new Sprite(_pixel, rect, _options.SelectionColor, DrawingLayer.Foreground3);

            Engine.SpriteRenderer.Draw(selectionSprite);
            //spriteBatch.Draw(_pixel, rect, null, _options.SelectionColor);
        }

        #endregion

        public void Dispose()
        {
            //TODO: Dispose...
            _pixel.Dispose();
        }
    }
}
