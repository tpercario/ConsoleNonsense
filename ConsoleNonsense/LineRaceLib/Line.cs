using System;

using Colorful;
using System.Drawing;

using Console = Colorful.Console;

namespace LineRaceLib
{
    public class Line
    {
        private static Random Rand = new Random();

        public char Symbol { private set; get; }

        public float LineAdvanceRate { private set; get; }

        public ColorAlternator LineColors { private set; get; }

        public int WindowHeightPos { private set; get; }
        public int WindowWidthPos { private set; get; }

        public Line(char symbol, float advanceRate, ColorAlternator alternator, int height, int width)
        {
            Symbol = symbol;

            LineAdvanceRate = advanceRate;

            LineColors = alternator;

            WindowHeightPos = height;
            WindowWidthPos = width;
        }

        public void TryToRun()
        {
            Console.SetCursorPosition(WindowWidthPos, WindowHeightPos);

            if (Rand.NextDouble() < LineAdvanceRate)
            {
                Console.WriteAlternating(Symbol, LineColors);

                WindowWidthPos++;
            }
        }
    }
}
