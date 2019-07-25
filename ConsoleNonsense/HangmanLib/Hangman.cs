using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System.Drawing;

using Console = Colorful.Console;

namespace HangmanLib
{
    public class Hangman
    {
        private const string WORD_LIST = "words.txt";

        private static Random Rand = new Random();

        public const int NUMBER_GUESSES = 7;

        private const string USER_INPUT_MSG = "Enter your guess (lower case): ";

        public string Word { private set; get; }

        private char[] SplitWord;
        private char[] UserWord;

        private List<char> GuessedCharacters;

        private int NumberMissedCharacters;

        public Hangman()
        {
            Word = GetRandomWord();
            Word.ToLower();

            SplitWord = Word.ToCharArray();

            UserWord = new char[SplitWord.Length];

            for (int i = 0; i < UserWord.Length; ++i)
            {
                UserWord[i] = '-';
            }

            GuessedCharacters = new List<char>();

            NumberMissedCharacters = 0;
        }

        public static string GetRandomWord()
        {
            string[] words = File.ReadAllLines(WORD_LIST);

            return words[Rand.Next(0, words.Length)];
        }

        public bool WordContainsCharacter(char character)
        {
            return Word.Contains(character);
        }

        public void AddUserCharacter(char userChar)
        {
            for (int i = 0; i < SplitWord.Length; ++i)
            {
                if (userChar == SplitWord[i])
                {
                    UserWord[i] = userChar;
                }
            }
        }

        private bool UserWon()
        {
            bool userWon = true;

            for (int i = 0; i < SplitWord.Length && userWon; ++i)
            {
                if (SplitWord[i] != UserWord[i])
                {
                    userWon = false;
                }
            }

            return userWon;
        }

        private bool IsGameOver()
        {
            bool over = false;

            if (NumberMissedCharacters >= NUMBER_GUESSES)
            {
                over = true;
            }
            else if (UserWon())
            {
                over = true;
            }

            return over;
        }

        private void PrintGuessedWord()
        {
            int consoleMiddleWidth = Console.WindowWidth / 2;

            Console.SetCursorPosition(consoleMiddleWidth - (Word.Length / 2), 5);

            foreach (char currentChar in UserWord)
            {
                Console.Write(currentChar, Color.Black);
            }
        }

        private void PrintGuessedLetters()
        {
            int consoleMiddleWidth = Console.WindowWidth / 2;
            string guessedLettersMessage = "You have guessed the letters:";

            Console.SetCursorPosition(consoleMiddleWidth - (guessedLettersMessage.Length / 2), 7);

            Console.Write(guessedLettersMessage, Color.Black);

            Console.SetCursorPosition(consoleMiddleWidth - (GuessedCharacters.Count / 2), 8);

            foreach (char currentCharacter in GuessedCharacters)
            {
                Console.Write(currentCharacter, Color.Black);
            }
        }

        private string GetHeadLevelString()
        {
            string headLevel = "|     ";

            if (NumberMissedCharacters > 0)
            {
                headLevel = "|     O";
            }

            return headLevel;
        }

        private string GetArmLevelString()
        {
            string armLevel = "|     ";

            if (NumberMissedCharacters >= 2)
            {
                armLevel = "|     |";
            }

            if (NumberMissedCharacters >= 3)
            {
                armLevel = "|    /|";
            }
            
            if (NumberMissedCharacters >= 4)
            {
                armLevel = "|    /|\\";
            }

            return armLevel;
        }

        private string GetBellyLevelString()
        {
            string bellyLevel = "|     ";

            if (NumberMissedCharacters >= 5)
            {
                bellyLevel = "|     |";
            }

            return bellyLevel;
        }

        private string GetLegLevelString()
        {
            string legLevel = "|     ";

            if (NumberMissedCharacters >= 6)
            {
                legLevel = "|    / ";
            }

            if (NumberMissedCharacters >= 7)
            {
                legLevel = "|    / \\";
            }

            return legLevel;
        }

        private void PrintHangedMan()
        {
            int consoleMiddleWidth = Console.WindowWidth / 2;

            string gallowsTopBar = "_______";
            string gallowsRope = "|     |";
            string gallowsBottomBar = "========";

            int baseGallowsHeight = 10;

            Console.SetCursorPosition(consoleMiddleWidth - (gallowsBottomBar.Length / 2), baseGallowsHeight++);

            Console.Write(gallowsTopBar, Color.Brown);

            Console.SetCursorPosition(consoleMiddleWidth - (gallowsBottomBar.Length / 2), baseGallowsHeight++);

            Console.Write(gallowsRope, Color.Brown);

            Console.SetCursorPosition(consoleMiddleWidth - (gallowsBottomBar.Length / 2), baseGallowsHeight++);

            Console.Write(GetHeadLevelString(), Color.Brown);

            Console.SetCursorPosition(consoleMiddleWidth - (gallowsBottomBar.Length / 2), baseGallowsHeight++);

            Console.Write(GetArmLevelString(), Color.Brown);

            Console.SetCursorPosition(consoleMiddleWidth - (gallowsBottomBar.Length / 2), baseGallowsHeight++);

            Console.Write(GetBellyLevelString(), Color.Brown);

            Console.SetCursorPosition(consoleMiddleWidth - (gallowsBottomBar.Length / 2), baseGallowsHeight++);

            Console.Write(GetLegLevelString(), Color.Brown);

            Console.SetCursorPosition(consoleMiddleWidth - (gallowsBottomBar.Length / 2), baseGallowsHeight++);

            Console.Write(gallowsBottomBar, Color.Brown);

            baseGallowsHeight++;
            Console.SetCursorPosition(consoleMiddleWidth - (USER_INPUT_MSG.Length / 2), baseGallowsHeight++);
        }

        private void PrintEndMessage()
        {
            string winMsg = "Congrats, you correctly guessed {0}";
            string loseMsg = "Haha loser, you couldn't guess {0}";

            Console.Clear();

            if (UserWon())
            {
                PrintMsgCenter(String.Format(winMsg, Word), Color.Black);
            }
            else
            {
                PrintMsgCenter(String.Format(loseMsg, Word), Color.Black);
            }
        }

        private void Print()
        {
            Console.Clear();

            PrintGuessedWord();
            PrintGuessedLetters();
            PrintHangedMan();
        }

        private char GetUserInput()
        {
            Console.Write(USER_INPUT_MSG, Color.Black);
            return Console.ReadKey().KeyChar;
        }

        private bool LetterAlreadyGuessed(char letter)
        {
            bool alreadyGuessed = false;

            foreach (char currentChar in GuessedCharacters)
            {
                if (currentChar == letter)
                {
                    alreadyGuessed = true;
                }
            }

            return alreadyGuessed;
        }

        private void PrintMsgCenter(string message, Color printColor)
        {
            int centerHigh = Console.WindowHeight / 2;

            int centerWide = (Console.WindowWidth / 2) - (message.Length / 2);

            Console.SetCursorPosition(centerWide, centerHigh);

            Console.Write(message, printColor);
        }

        private bool IsLowerLetter(char letter)
        {
            const int ASCII_LOWER_A = 97;
            const int ASCII_LOWER_Z = 122;

            string stringLetter = letter.ToString();

            char lowerLetter = stringLetter.ToLower().ToCharArray()[0];

            int asciiLetterInt = (int)lowerLetter;

            return (asciiLetterInt >= ASCII_LOWER_A && asciiLetterInt <= ASCII_LOWER_Z);
        }

        private bool ProcessUserGuess(char userGuess)
        {
            bool retry = false;

            if (!IsLowerLetter(userGuess))
            {
                retry = true;

                Console.Clear();
                PrintMsgCenter("You didn't enter a letter...", Color.Black);
                Console.ReadKey();

                Print();
            }
            else if (LetterAlreadyGuessed(userGuess))
            {
                retry = true;

                Console.Clear();
                PrintMsgCenter("You already guessed that letter...", Color.Black);
                Console.ReadKey();

                Print();
            }

            return retry;
        }

        public void Start()
        {
            Console.BackgroundColor = Color.SkyBlue;
            Console.ForegroundColor = Color.Black;

            while (!IsGameOver())
            {
                Print();

                char userGuess = '~';
                bool retry = false;

                do
                {
                    try
                    {
                        userGuess = GetUserInput();

                        retry = ProcessUserGuess(userGuess);
                    }
                    catch (Exception e)
                    {
                        retry = true;
                    }

                } while (retry);

                GuessedCharacters.Add(userGuess);

                if (WordContainsCharacter(userGuess))
                {
                    AddUserCharacter(userGuess);
                }
                else
                {
                    ++NumberMissedCharacters;
                }
            }

            Print();
            Console.ReadKey();
            PrintEndMessage();

            Console.ReadKey();

            Console.BackgroundColor = Color.Black;
            Console.ForegroundColor = Color.White;
        }
    }
}
