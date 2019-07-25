using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Colorful;
using System.Drawing;

using Console = Colorful.Console;

using System.Threading;

namespace LineRaceLib
{
    public class Race
    {
        private static Random Rand = new Random();

        private static char[] Symbols = { '-', '=', '+', '_', '~', '>', (char)187, (char)172 };

        private static Color[] Colors = { Color.AntiqueWhite, Color.Aqua, Color.Azure, Color.Blue, Color.BlueViolet, Color.Chartreuse,
                                          Color.Cornsilk, Color.Crimson, Color.DarkGoldenrod, Color.DarkMagenta, Color.DarkRed, Color.DarkSalmon,
                                          Color.DarkTurquoise, Color.DarkViolet, Color.Firebrick, Color.FloralWhite, Color.ForestGreen, Color.Fuchsia,
                                          Color.Gold, Color.Green, Color.Honeydew, Color.HotPink, Color.Indigo, Color.LavenderBlush, Color.LawnGreen,
                                          Color.LemonChiffon, Color.LightCoral, Color.LightPink, Color.LightGreen, Color.Lime, Color.Maroon,
                                          Color.Linen, Color.MediumOrchid, Color.MediumVioletRed, Color.MistyRose, Color.Moccasin, Color.Orange,
                                          Color.OrangeRed, Color.PapayaWhip, Color.Plum, Color.Red, Color.SeaGreen, Color.SkyBlue, Color.Silver,
                                          Color.SteelBlue, Color.Teal, Color.Thistle, Color.Violet, Color.Turquoise };

        public List<Line> RacingLines { private set; get; }

        public Race()
        {
            Console.Clear();

            int maxParticipants = CalculateMaxParticipants();

            Console.WriteLine();
            Console.Write("Enter the number of race participants (max {0}): ", Color.White, maxParticipants);

            int nParticipants = Convert.ToInt32(Console.ReadLine());

            RacingLines = new List<Line>();

            CreateLines(nParticipants);
        }

        private int CalculateMaxParticipants()
        {
            return (Console.WindowHeight / 2) - 1;
        }

        private void CreateLines(int nParticipants)
        {
            int lineHeightSpacing = Console.WindowHeight / nParticipants;

            int currentLineHeight = 2;

            for (int i = 0; i < nParticipants; ++i)
            {
                RacingLines.Add(CreateRandomLineFromHeight(currentLineHeight));

                currentLineHeight += lineHeightSpacing;
            }
        }

        private static Line CreateRandomLineFromHeight(int windowHeight)
        {
            float advanceRate = (float)Rand.NextDouble();

            while (advanceRate == 0.0f)
            {
                advanceRate = (float)Rand.NextDouble();
            }

            ColorAlternatorFactory factory = new ColorAlternatorFactory();
            ColorAlternator alternator = factory.GetAlternator(1, Colors[Rand.Next(0, Colors.Length)],
                Colors[Rand.Next(0, Colors.Length)]);

            char symbol = Symbols[Rand.Next(0, Symbols.Length)];

            return new Line(symbol, advanceRate, alternator, windowHeight, 0);
        }

        private bool ALineWon()
        {
            bool winner = false;

            foreach (Line line in RacingLines)
            {
                if (line.WindowWidthPos >= Console.WindowWidth)
                {
                    winner = true;
                }
            }

            return winner;
        }

        private Line GetWinningLine()
        {
            Line winner = null;

            foreach (Line line in RacingLines)
            {
                if (line.WindowWidthPos >= Console.WindowWidth)
                {
                    winner = line;
                }
            }

            return winner;
        }

        public void Start()
        {
            Console.Clear();

            while (!ALineWon())
            {
                for (int i = 0; i < RacingLines.Count; ++i)
                {
                    RacingLines[i].TryToRun();
                }

                Thread.Sleep(20);
            }

            Line winner = GetWinningLine();

            Console.Clear();

            int consoleHalfWidth = Console.WindowWidth / 2;
            string winningLineMsg = "The Winning Line is: ";

            Console.SetCursorPosition(consoleHalfWidth - (winningLineMsg.Length / 2), (Console.WindowHeight / 2) - 5);
            Console.WriteLine(winningLineMsg, Color.White);
            Console.SetCursorPosition((Console.WindowWidth / 2) - 4, (Console.WindowHeight / 2) - 3);
            Console.Write(winner.Symbol, winner.LineColors.Colors[0]);
            Console.Write(winner.Symbol, winner.LineColors.Colors[1]);
            Console.Write(winner.Symbol, winner.LineColors.Colors[0]);
            Console.Write(winner.Symbol, winner.LineColors.Colors[1]);
            Console.Write(winner.Symbol, winner.LineColors.Colors[0]);
            Console.Write(winner.Symbol, winner.LineColors.Colors[1]);

            Console.ForegroundColor = Color.White;
            Console.ReadKey();
        }
    }
}
