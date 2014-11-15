using System;

namespace JPEngine.Events
{
    //source : https://code.google.com/r/jameswalkoski-xnagameconsole-xna4/source/
    //TODO: Not used anymore, remove it?
    public class CharacterEventArgs : EventArgs
    {
        private readonly char _character;
        private readonly int _lParam;

        public CharacterEventArgs(char character, int lParam)
        {
            _character = character;
            _lParam = lParam;
        }

        public char Character
        {
            get { return _character; }
        }

        public int Param
        {
            get { return _lParam; }
        }

        public int RepeatCount
        {
            get { return _lParam & 0xffff; }
        }

        public bool ExtendedKey
        {
            get { return (_lParam & (1 << 24)) > 0; }
        }

        public bool AltPressed
        {
            get { return (_lParam & (1 << 29)) > 0; }
        }

        public bool PreviousState
        {
            get { return (_lParam & (1 << 30)) > 0; }
        }

        public bool TransitionState
        {
            get { return (_lParam & (1 << 31)) > 0; }
        }
    }
}