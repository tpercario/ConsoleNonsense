// Tyler Percario
// ConsoleNonsense 7/12/19
// Menu singleton for displaying the credits page

using System;

using System.Drawing;
using System.IO;

using Console = Colorful.Console;

namespace ConsoleMenuLib
{
    public class CreditsMenu : IMenu
    {
        public const string CREDITS_FILE = "credits.txt";

        public const string CREDITS_MENU_STRING = "Credits";

        private static CreditsMenu TheOnlyCreditsMenu = null; 

        private CreditsMenu()
        {
        }

        public static IMenu GetInstance()
        {
            if (TheOnlyCreditsMenu == null)
            {
                TheOnlyCreditsMenu = new CreditsMenu();
            }

            return TheOnlyCreditsMenu;
        }

        // Prints out the credits as display
        public void Display()
        {
            Console.Clear();

            string[] creditLines = File.ReadAllLines(CREDITS_FILE);

            foreach (string line in creditLines)
            {
                Console.WriteLine(line, Color.White);
            }
        }

        public IMenu GetNextMenu()
        {
            Console.ReadKey();

            // Only way to go is home
            return HomeMenu.GetInstance();
        }
    }
}
