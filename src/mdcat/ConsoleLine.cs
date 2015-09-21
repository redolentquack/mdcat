using System;

namespace mdcat
{
    public class ConsoleLine
    {
        private readonly ConsoleCharacterList _characterList;

        public ConsoleLine(string text, ParseMode mode)
        {
            _characterList = new ConsoleCharacterList();
            _characterList.AddRange(text.ToCharArray(), mode);
            Mode = mode;
        }

        public string Text
        {
            get { return _characterList.ToString(); }
        }

        public ConsoleCharacterList ConsoleCharacterList
        {
            get { return _characterList; }
        }

        public ParseMode Mode { get; private set; }

        public void SetMode(ParseMode mode)
        {
            Mode = mode;
            foreach (ConsoleCharacter consoleCharacter in _characterList)
            {
                consoleCharacter.SetMode(mode);
            }
        }
    }
}