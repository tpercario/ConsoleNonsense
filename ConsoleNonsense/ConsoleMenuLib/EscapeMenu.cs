using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenuLib
{
    public class EscapeMenu : IMenu
    {
        public const string ESCAPE_MENU_STRING = "Escape";

        private static EscapeMenu TheOneEscapeMenu = null;

        private EscapeMenu()
        {
        }

        public static EscapeMenu GetInstance()
        {
            if (TheOneEscapeMenu == null)
            {
                TheOneEscapeMenu = new EscapeMenu();
            }

            return TheOneEscapeMenu;
        }

        public void Display()
        {
            string header = "Escape Menu";

            ArrowedMenu.GetInstance().Load(header, true, HomeMenu.HOME_MENU_STRING, CreditsMenu.CREDITS_MENU_STRING,
                FavoritesMenu.FAVORITES_MENU_STRING, "Quit");
            ArrowedMenu.GetInstance().Display();
        }

        public IMenu GetNextMenu()
        {
            return ArrowedMenu.GetInstance().GetNextMenu();
        }
    }
}
