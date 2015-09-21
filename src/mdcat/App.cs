using System;
using System.Collections.Generic;
using System.IO;

namespace mdcat
{
    public class App
    {
        public void Run(string filename)
        {
            ProcessLines(File.ReadAllLines(filename));
        }

        private void ProcessLines(string[] lines)
        {
            var consoleLines = ConvertToConsoleLines(lines);
            EchoConsoleLines(consoleLines);
        }

        public List<ConsoleLine> ConvertToConsoleLines(string[] lines)
        {
            var consoleLines = new List<ConsoleLine>();
            var previousParseMode = ParseMode.None;

            foreach (var currentLine in lines)
            {
                var parseMode = ParseMode.None;

                if (currentLine.StartsWith("#"))
                {
                    parseMode = ParseMode.Header;
                }
                else if (currentLine.StartsWith(">"))
                {
                    parseMode = ParseMode.Quote;
                }
                else if (currentLine.StartsWith("    ") || currentLine.StartsWith("\t"))
                {
                    parseMode = ParseMode.Code;
                }
                else if (currentLine.StartsWith("```"))
                {
                    parseMode = previousParseMode == ParseMode.Code ? ParseMode.None : ParseMode.Code;
                }
                else if (String.IsNullOrEmpty(currentLine))
                {
                    parseMode = ParseMode.None;
                }
                else if (previousParseMode == ParseMode.Code)
                {
                    parseMode = ParseMode.Code;
                }

                var mode = parseMode;
                if (currentLine.StartsWith("```"))
                {
                    // There is always a special....
                    mode = ParseMode.None;
                }
                else if (consoleLines.Count > 0 && (currentLine.StartsWith("===") || currentLine.StartsWith("---")))
                {
                    // There is always another special....
                    mode = parseMode = ParseMode.Header;
                    consoleLines[consoleLines.Count - 1].SetMode(ParseMode.Header);
                }

                consoleLines.Add(new ConsoleLine(currentLine, mode));

                previousParseMode = parseMode;
            }

            return consoleLines;
        }

        public void EchoConsoleLines(List<ConsoleLine> consoleLines)
        {
            foreach (var consoleLine in consoleLines)
            {
                foreach (var consoleCharacter in consoleLine.ConsoleCharacterList)
                {
                    switch (consoleCharacter.Mode)
                    {
                        case ParseMode.Header:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case ParseMode.Quote:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;

                        case ParseMode.Code:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;

                        default:
                            Console.ResetColor();
                            break;
                    }

                    Console.Write(consoleCharacter.Character);
                }
                Console.WriteLine();
            }
        }

        public void Help()
        {
            ProcessLines(Properties.Resources.Help.Split('\n'));
        }
    }
}