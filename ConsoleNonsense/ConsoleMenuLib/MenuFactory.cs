// Tyler Percario
// ConsoleNonsense 7/16/19
// Class that gives the correct IMenu object from given input

using System;

namespace ConsoleMenuLib
{
    public class MenuFactory
    {
        public static IMenu GetMenuFromString(string menuString)
        {
            IMenu returnMenu = null;

            switch (menuString)
            {
                case HomeMenu.HOME_MENU_STRING:
                    {
                        returnMenu = HomeMenu.GetInstance();
                        break;
                    }
                case HangmanMenu.HANGMAN_MENU_STRING:
                    {
                        returnMenu = HangmanMenu.GetInstance();
                        break;
                    }
                case LineRaceMenu.LINE_RACE_MENU_STRING:
                    {
                        returnMenu = LineRaceMenu.GetInstance();
                        break;
                    }
                case QuackMenu.QUACK_MENU_STRING:
                    {
                        returnMenu = QuackMenu.GetInstance();
                        break;
                    }
                case CreditsMenu.CREDITS_MENU_STRING:
                    {
                        returnMenu = CreditsMenu.GetInstance();
                        break;
                    }
                case FavoritesMenu.FAVORITES_MENU_STRING:
                    {
                        returnMenu = FavoritesMenu.GetInstance();
                        break;
                    }
                case EscapeMenu.ESCAPE_MENU_STRING:
                    {
                        returnMenu = EscapeMenu.GetInstance();
                        break;
                    }
                case NumberGuessingGameMenu.NUMBER_GUESSING_GAME_MENU_STRING:
                    {
                        returnMenu = NumberGuessingGameMenu.GetInstance();
                        break;
                    }
                default:
                    {
                        returnMenu = null;
                        break;
                    }
            }

            return returnMenu;
        }
    }
}
