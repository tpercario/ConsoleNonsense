// Tyler Percario
// ConsoleNonsense 7/12/19
// Hangman Menu singleton for handling the start and dealings with handman 

using System;
using System.Drawing;

using HangmanLib;

using Console = Colorful.Console;

namespace ConsoleMenuLib
{
    public class HangmanMenu : IMenu
    {
        public const int START_HANGMAN_CHOICE = 1;
        public const int BACK_TO_HOME_CHOICE = 2;

        public const string HANGMAN_MENU_STRING = "Hangman";

        private static HangmanMenu TheOnlyHangmanMenu = null; 

        private HangmanMenu()
        {
        }

        public static IMenu GetInstance()
        {
            if (TheOnlyHangmanMenu == null)
            {
                TheOnlyHangmanMenu = new HangmanMenu();
            }

            return TheOnlyHangmanMenu;
        }
        public void Display()
        {
            string header = "Welcome to Hangman";

            ArrowedMenu.GetInstance().Load(header, true, "Start Hangman", HomeMenu.HOME_MENU_STRING);
            ArrowedMenu.GetInstance().Display();
        }

        public IMenu GetNextMenu()
        {
            IMenu nextMenu = ArrowedMenu.GetInstance().GetNextMenu();

            // If it is null then quit was selected
            if (nextMenu == null)
            {
                StartHangman();

                nextMenu = HangmanMenu.GetInstance();
            }

            return nextMenu;
        }

        public void StartHangman()
        {
            Hangman hangman = new Hangman();

            hangman.Start();
        }
    }
}
