// Tyler Percario
// Console Nonsense 7/16/19
// Main 

using System;

using System.Threading;
using System.Drawing;
using Console = Colorful.Console;

using ConsoleMenuLib;

namespace MainProgram
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.CursorVisible = false;
            IMenu currentMenu = WelcomeMenu.GetInstance();

            bool running = true;

            while (running)
            {
                currentMenu.Display();

                try
                {
                    currentMenu = currentMenu.GetNextMenu();

                    if (currentMenu == null)
                    {
                        running = false;
                    }
                }
                catch (Exception e)
                {
                }
            }

            Console.CursorVisible = true;
        }
    }
}
