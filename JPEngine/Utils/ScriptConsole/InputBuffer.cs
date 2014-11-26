using System;
using System.Text;

namespace JPEngine.Utils.ScriptConsole
{
    internal class InputBuffer
    {
        private int _cursorPos;

        private int _selectionStart;
        private int _selectionEnd;
        private bool _isSelecting;

        private readonly OutputLine _outputLine;

        internal bool IsSelecting
        {
            get { return _isSelecting; }
            set
            {
                if (!value)
                    _selectionStart = _selectionEnd = _cursorPos;

                _isSelecting = value;
            }
        }

        internal int CursorPosition
        {
            get { return _cursorPos; }
            set
            {
                if (value < 0 || value > _outputLine.Output.Length)
                    return;

                //TODO: Reformat this part, way too many IFs
                if (IsSelecting)
                {
                    if (SelectionEnd < value && SelectionStart < value)
                    {
                        SelectionEnd = value;
                    }

                    if (SelectionEnd > value && SelectionStart < value)
                    {
                        if (CursorPosition < value)
                            SelectionStart = value;
                        else
                            SelectionEnd = value;
                    }

                    if (SelectionEnd > value && SelectionStart > value)
                    {
                        SelectionStart = value;
                    }
                }
                else
                    _selectionEnd = _selectionStart = value;

                _cursorPos = value;
            }
        }

        internal int SelectionStart
        {
            get { return _selectionStart; }
            private set
            {
                if (value < 0 || value > _outputLine.Output.Length || value > _selectionEnd)
                    return;

                _selectionStart = value;
            }
        }

        internal int SelectionEnd
        {
            get { return _selectionEnd; }
            private set
            {
                if (value < 0 || value > _outputLine.Output.Length || value < _selectionStart)
                    return;

                _selectionEnd = value;
            }
        }

        internal int SelectionLength
        {
            get { return SelectionEnd - SelectionStart; }
        }

        internal string Selection
        {
            get { return _outputLine.Output.Substring(_selectionStart, _selectionEnd - _selectionStart); }
        }

        public string Value
        {
            get { return _outputLine.Output; }
        }

        internal InputBuffer()
        {
            _outputLine = new OutputLine("", OutputLineType.Command);
        }

        internal void Add(string text)
        {
            _outputLine.Output = _outputLine.Output.Insert(_cursorPos, text);
            _cursorPos += text.Length;
        }

        internal void ReplaceSelection(string text)
        {
            var outputStringBuilder = new StringBuilder(_outputLine.Output);
            outputStringBuilder.Remove(SelectionStart, SelectionEnd - SelectionStart);
            outputStringBuilder.Insert(SelectionStart, text);

            _outputLine.Output = outputStringBuilder.ToString();
            _cursorPos = SelectionEnd = SelectionStart;
        }

        internal void RemoveBeforeCursor(int amount)
        {
            int start = Math.Max(0, _cursorPos - amount);
            _outputLine.Output = _outputLine.Output.Remove(start, _cursorPos - start);
            _cursorPos = start;
        }

        internal void RemoveAfterCursor(int amount)
        {
            int end = Math.Min(_outputLine.Output.Length, _cursorPos + amount);
            _outputLine.Output = _outputLine.Output.Remove(_cursorPos, end - _cursorPos);
        }

        internal void Set(string text)
        {
            _outputLine.Output = text;
            _cursorPos = text.Length;
        }

        internal void Clear()
        {
            _outputLine.Output = string.Empty;
            _cursorPos = 0;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
