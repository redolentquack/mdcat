using System;

namespace mdcat
{
    public class ConsoleCharacter
    {
        public ConsoleCharacter(char character, ParseMode mode)
        {
            Character = character;
            Mode = mode;
        }

        public char Character { get; private set; }
        public ParseMode Mode { get; private set; }

        public void SetMode(ParseMode mode)
        {
            Mode = mode;
        }
    }
}