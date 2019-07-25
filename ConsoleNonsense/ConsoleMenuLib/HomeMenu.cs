// Tyler Percario
// ConsoleNonsense 7/12/19
// Home/Main Menu singleton, main way to access all features 

using System;

using System.Drawing;

using Console = Colorful.Console;

namespace ConsoleMenuLib
{
    public class HomeMenu : IMenu
    {
        public const string HOME_MENU_STRING = "Home";

        private static HomeMenu TheOnlyHomeMenu = null;

        private HomeMenu()
        {
        }

        public static IMenu GetInstance()
        {
            if (TheOnlyHomeMenu == null)
            {
                TheOnlyHomeMenu = new HomeMenu();
            }

            return TheOnlyHomeMenu;
        }

        public void Display()
        {
            string header = "This is the Home Page";
            string footer = "Up/Down arrows cycle options. Right/Left arrows cycle pages. \n" +
                "Enter selects an option. F/U favorites/unfavorites an option. Escape goes to escape menu.";

            ArrowedMenu.GetInstance().Load(header, footer, QuackMenu.QUACK_MENU_STRING, LineRaceMenu.LINE_RACE_MENU_STRING, 
                HangmanMenu.HANGMAN_MENU_STRING, NumberGuessingGameMenu.NUMBER_GUESSING_GAME_MENU_STRING);

            ArrowedMenu.GetInstance().Display();
        }

        public IMenu GetNextMenu()
        {
            return ArrowedMenu.GetInstance().GetNextMenu();
        }
    }
}
