// Tyler Percario
// ConsoleNonsense 7/12/19
// Hangman Menu singleton for handling the start and dealings with handman 

using System;
using System.Drawing;

using HangmanLib;

using Console = Colorful.Console;

using NumberGuessingGame;

namespace ConsoleMenuLib
{
    public class NumberGuessingGameMenu : IMenu
    {
        public const int START_HANGMAN_CHOICE = 1;
        public const int BACK_TO_HOME_CHOICE = 2;

        public const string NUMBER_GUESSING_GAME_MENU_STRING = "Number Guessing Game";

        private static NumberGuessingGameMenu TheOnlyNumberGuessingGameMenu = null; 

        private NumberGuessingGameMenu()
        {
        }

        public static NumberGuessingGameMenu GetInstance()
        {
            if (TheOnlyNumberGuessingGameMenu == null)
            {
                TheOnlyNumberGuessingGameMenu = new NumberGuessingGameMenu();
            }

            return TheOnlyNumberGuessingGameMenu;
        }
        public void Display()
        {
            string header = "Welcome to the Number Guessing Game";

            ArrowedMenu.GetInstance().Load(header, true, "Start the Number Guessing Game", HomeMenu.HOME_MENU_STRING);
            ArrowedMenu.GetInstance().Display();
        }

        public IMenu GetNextMenu()
        {
            IMenu nextMenu = ArrowedMenu.GetInstance().GetNextMenu();

            if (nextMenu == null)
            {
                StartNumberGuessingGame();

                nextMenu = NumberGuessingGameMenu.GetInstance();
            }

            return nextMenu;
        }

        public void StartNumberGuessingGame()
        {
            GuessingGame numberGuessing = new GuessingGame();

            numberGuessing.Start();
        }
    }
}

