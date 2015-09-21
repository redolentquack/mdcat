using System;
using System.Collections.Generic;
using System.Text;

namespace mdcat
{
    public class ConsoleCharacterList : List<ConsoleCharacter>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var consoleChar in this)
            {
                sb.Append(consoleChar.Character);
            }
            return sb.ToString();
        }

        public void AddRange(char[] chars, ParseMode defaultMode)
        {
            var lineMode = defaultMode;
            var parseMode = defaultMode;

            foreach (var ch in chars)
            {
                switch (lineMode)
                {
                    case ParseMode.Header:
                    case ParseMode.Code:
                    case ParseMode.Quote:
                        // If one of these line styles we keep that throughout the line
                        break;

                    default:
                        if (ch == '`')
                        {
                            parseMode = parseMode == defaultMode ? ParseMode.Code : defaultMode;
                        }
                        break;
                }

                var mode = parseMode;
                if (ch == '`')
                {
                    // There is always a special....
                    mode = defaultMode;
                }
                Add(new ConsoleCharacter(ch, mode));
            }
        }
    }
}