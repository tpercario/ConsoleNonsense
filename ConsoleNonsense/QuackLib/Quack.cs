using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Diagnostics;

using System.Drawing;
using Console = Colorful.Console;

namespace QuackLib
{
    public class Quack
    {
        public const int SECONDS_TO_MILLISECONDS = 1000;

        private static Random Rand = new Random();

        public int SecondsToRun { private set; get; }

        public Quack()
        {
        }

        public void Start()
        {
            Console.Clear();
            Console.WriteLine();

            Console.Write("Enter the duration of quacking (seconds): ", Color.White);
            SecondsToRun = Convert.ToInt32(Console.ReadLine());

            int sleepTime = SECONDS_TO_MILLISECONDS;
            int millisecondsToRun = SecondsToRun * SECONDS_TO_MILLISECONDS;

            Stopwatch timer = new Stopwatch();
            timer.Start();

            while (timer.ElapsedMilliseconds < millisecondsToRun)
            {
                Console.Clear();

                Console.SetCursorPosition(Rand.Next(0, Console.WindowWidth), 
                    Rand.Next(0, Console.WindowHeight));

                Console.Write("Quack", Color.Yellow);

                Thread.Sleep(sleepTime);
            }
        }
    }
}
