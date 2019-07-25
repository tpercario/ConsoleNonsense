using System;
using System.Drawing;

using Console = Colorful.Console;

namespace ConsoleMenuLib
{
    public class WelcomeMenu : IMenu
    {
        public const string WELCOME_MENU_STRING = "Welcome";

        private static WelcomeMenu TheOneWelcomeMenu = null;

        private WelcomeMenu()
        {

        }

        public static WelcomeMenu GetInstance()
        {
            if (TheOneWelcomeMenu == null)
            {
                TheOneWelcomeMenu = new WelcomeMenu();
            }

            return TheOneWelcomeMenu;
        }

        public void Display()
        {
            // This page, like the credits page, will just display stuff so no
            // need for arrowed menu

            string welcome = "Welcome to Console Nonsense!";
            Console.SetCursorPosition((Console.WindowWidth / 2) - (welcome.Length / 2), (Console.WindowHeight / 2) - 5);

            Console.Write(welcome, Color.White);
        }

        public IMenu GetNextMenu()
        {
            Console.ReadKey();

            return HomeMenu.GetInstance();
        }
    }
}
