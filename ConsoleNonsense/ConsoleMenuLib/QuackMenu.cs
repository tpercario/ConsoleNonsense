// Tyler Percario
// ConsoleNonsense 7/12/19
// Quack Menu singleton for handling the start and dealings with quack 

using System;
using System.Drawing;

using QuackLib;

using Console = Colorful.Console;

namespace ConsoleMenuLib
{
    public class QuackMenu : IMenu
    {
        public const int START_QUACK_CHOICE = 1;
        public const int BACK_TO_HOME_CHOICE = 2;

        public const string QUACK_MENU_STRING = "Quack";

        private static QuackMenu TheOnlyQuackMenu = null;

        private QuackMenu()
        {

        }

        public static IMenu GetInstance()
        {
            if (TheOnlyQuackMenu == null)
            {
                TheOnlyQuackMenu = new QuackMenu();
            }

            return TheOnlyQuackMenu;
        }

        public void Display()
        {
            string header = "Welcome to Quack";

            ArrowedMenu.GetInstance().Load(header, true, "Start Quack", HomeMenu.HOME_MENU_STRING);
            ArrowedMenu.GetInstance().Display();
        }

        public IMenu GetNextMenu()
        {
            IMenu nextMenu = ArrowedMenu.GetInstance().GetNextMenu();

            // If it is null then quit was selected
            if (nextMenu == null)
            {
                StartQuack();

                nextMenu = QuackMenu.GetInstance();
            }

            return nextMenu;
        }

        private void StartQuack()
        {
            Quack runQuack = new Quack();

            runQuack.Start();
        }
    }
}
