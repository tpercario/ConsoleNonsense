// Tyler Percario
// ConsoleNonsense 7/12/19
// LineRace Menu singleton for handling the start and dealings with line race

using System;
using System.Drawing;

using LineRaceLib;

using Console = Colorful.Console;

namespace ConsoleMenuLib
{
    public class LineRaceMenu : IMenu
    {
        public const int START_LINE_RACE_CHOICE = 1;
        public const int BACK_TO_HOME_CHOICE = 2;

        public const string LINE_RACE_MENU_STRING = "LineRace";

        private static LineRaceMenu TheOnlyLineRaceMenu = null;

        private LineRaceMenu()
        {

        }

        public static IMenu GetInstance()
        {
            if (TheOnlyLineRaceMenu == null)
            {
                TheOnlyLineRaceMenu = new LineRaceMenu();
            }

            return TheOnlyLineRaceMenu;
        }

        public void Display()
        {
            string header = "Welcome to Line Race";

            ArrowedMenu.GetInstance().Load(header, true, "Start Line Race", HomeMenu.HOME_MENU_STRING);
            ArrowedMenu.GetInstance().Display();
        }

        public IMenu GetNextMenu()
        {
            IMenu nextMenu = ArrowedMenu.GetInstance().GetNextMenu();

            // If it is null then quit was selected
            if (nextMenu == null)
            {
                StartLineRace();

                nextMenu = LineRaceMenu.GetInstance();
            }

            return nextMenu;
        }

        private void StartLineRace()
        {
            Race lineRace = new Race(); 

            lineRace.Start();
        }
    }
}
