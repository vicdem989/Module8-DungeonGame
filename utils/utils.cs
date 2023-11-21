namespace Utils
{
    public static class HelperFunctions
    {

        public static char[][] Create2DArrayFromMultiLineString(string input)
        {
            string[] lines = input.Split('\n');
            char[][] output = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                output[i] = lines[i].ToCharArray();
            }

            return output;
        }

        public static int WrappAround(int source, int min, int max)
        {
            int output = source;

            if (source < min)
            {
                output = max;
            }
            else if (source > max)
            {
                output = min;
            }
            return output;
        }

        public static Func<int> Oscillate(int min, int max, int delta = 1)
        {
            int current = min;
            int direction = 1;

            return () =>
            {
                current += direction * delta;
                if (current >= max)
                {
                    current = max;
                    direction = -1;
                }
                else if (current <= min)
                {
                    current = min;
                    direction = 1;
                }

                return current;
            };
        }

        public static string ReadFile(string filePath)
        {
            string content = "";
            try
            {
                content = File.ReadAllText(filePath);
            }
            catch (System.Exception ex)
            {
                Console.Error.WriteLine(ex);
                // Deal with the problem upstream?
            }
            return content;
        }
    }


    public static class ANSICodes
    {
        public const string Reset = "\u001b[0m";
        public const string HideCursor = "\u001b[?25l";
        public const string ShowCursor = "\u001b[?25h";

        public static class Colors
        {
            public const string Black = "\u001b[30m";
            public const string Red = "\u001b[31m";
            public const string Green = "\u001b[32m";
            public const string Yellow = "\u001b[33m";
            public const string Blue = "\u001b[34m";
            public const string Magenta = "\u001b[35m";
            public const string Cyan = "\u001b[36m";
            public const string White = "\u001b[37m";
        }

        public static class BgColors
        {
            public const string Black = "\u001b[40m";
            public const string Red = "\u001b[41m";
            public const string Green = "\u001b[42m";
            public const string Yellow = "\u001b[43m";
            public const string Blue = "\u001b[44m";
            public const string Magenta = "\u001b[45m";
            public const string Cyan = "\u001b[46m";
            public const string White = "\u001b[47m";
        }

        public static class Effects
        {
            public const string Bold = "\u001b[1m";
            public const string Underline = "\u001b[4m";
            public const string Reversed = "\u001b[7m";
        }

        public static class Positioning
        {
            public static string SetCursorPos(int row, int col) => $"\u001b[{row};{col}H";
            public const string CursorUp = "\u001b[A";
            public const string CursorDown = "\u001b[B";
            public const string CursorForward = "\u001b[C";
            public const string CursorBackward = "\u001b[D";
            public const string ClearScreen = "\u001b[2J";
            public const string ClearLine = "\u001b[K";
            public const string SaveCursorPosition = "\u001b[s";
            public const string RestoreCursorPosition = "\u001b[u";
        }



    }
}