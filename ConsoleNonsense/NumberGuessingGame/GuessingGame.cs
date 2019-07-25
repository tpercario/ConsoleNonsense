using System;
using System.Collections.Generic;

using System.Drawing;

using Console = Colorful.Console;

namespace NumberGuessingGame
{
    public class GuessingGame
    {
        private struct Guess
        {
            public enum GuessType { High, Low, Correct };

            public ulong GuessNumber { set; get; }
            public GuessType UserGuessAnswer { set; get; }

            public Guess(ulong guessNumber, GuessType userAnswer)
            {
                GuessNumber = guessNumber;

                UserGuessAnswer = userAnswer;
            }
        }

        private const int N_GUESSES = 10;
        private const int MAX_VALUE = 1000;
        private const int MIN_VALUE = 0;

        private List<Guess> ComputerGuesses;

        private ulong GuessRangeMin;
        private ulong GuessRangeMax;

        public GuessingGame()
        {
            ComputerGuesses = new List<Guess>();

            GuessRangeMin = MIN_VALUE;
            GuessRangeMax = MAX_VALUE;
        }

        private ulong GetGuessRangeMidPoint()
        {
            return (GuessRangeMin + GuessRangeMax) / 2;
        }

        private ConsoleKey GetUserInput()
        {
            ConsoleKey returnKey = ConsoleKey.A;

            do
            {
                returnKey = Console.ReadKey().Key;

            } while (returnKey != ConsoleKey.UpArrow && returnKey != ConsoleKey.DownArrow &&
                     returnKey != ConsoleKey.Enter);

            return returnKey;
        }

        private Guess.GuessType GetUserAnswer()
        {
            ConsoleKey inputKey = GetUserInput();
            Guess.GuessType returnGuessType = Guess.GuessType.Correct;

            if (inputKey == ConsoleKey.UpArrow)
            {
                returnGuessType = Guess.GuessType.High;
            }
            else if (inputKey == ConsoleKey.DownArrow)
            {
                returnGuessType = Guess.GuessType.Low;
            }

            return returnGuessType;
        }

        private ulong GetLastGuess()
        {
            return ComputerGuesses[ComputerGuesses.Count - 1].GuessNumber;
        }

        // Determines the next computer guess based on the userInput
        private ulong GetNextComputerGuess(Guess.GuessType guessType)
        {
            ulong guess = 0;

            if (guessType == Guess.GuessType.High)
            {
                GuessRangeMax = GetLastGuess();
            }
            else
            {
                GuessRangeMin = GetLastGuess();

            }

            if (guessType == Guess.GuessType.Low && GetLastGuess() == MAX_VALUE - 1)
            {
                guess = MAX_VALUE;
            }
            else
            {
                guess = GetGuessRangeMidPoint();
            }

            return guess; 
        }

        private bool IsGameOver(Guess.GuessType userAnswer)
        {
            return userAnswer == Guess.GuessType.Correct || ComputerGuesses.Count >= N_GUESSES ||
                GuessRangeMin == GuessRangeMax;
        }

        public void Start()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Think of a number between {0}-{1}. I will guess it in no more than {2} guesses.",
                Color.White, MIN_VALUE, MAX_VALUE, N_GUESSES);

            Console.ReadKey();
            Console.WriteLine();

            Console.WriteLine("Press Up arrow if my guess is too high.", Color.White);
            Console.WriteLine("Down arrow for too low.", Color.White);
            Console.WriteLine("Enter for just right.", Color.White);

            Console.WriteLine();

            ulong currentComputerGuess = MAX_VALUE / 2;
            Guess.GuessType userAnswer = Guess.GuessType.Low;

            Console.SetCursorPosition(1, Console.CursorTop);

            while (!IsGameOver(userAnswer))
            {
                Console.WriteLine("Guess Number {0}: {1}", Color.White, ComputerGuesses.Count + 1, currentComputerGuess);

                userAnswer = GetUserAnswer();

                ComputerGuesses.Add(new Guess(currentComputerGuess, userAnswer));

                currentComputerGuess = GetNextComputerGuess(userAnswer);
            }

            Console.WriteLine();

            if (userAnswer == Guess.GuessType.Correct)
            {
                Console.WriteLine("I have guessed correctly!", Color.White);
            }
            else if (GuessRangeMin == GuessRangeMax)
            {
                Console.WriteLine("You LIE!... It is {0}", Color.White, GetLastGuess());
            }
            else
            {
                Console.WriteLine("I have failed.", Color.White);
            }

            Console.ReadKey();
        }
    }
}
