using System;
using System.Collections.Generic;

using System.IO;

namespace ConsoleMenuLib
{
    public class FavoritesMenu : IMenu
    {
        public const string FAVORITES_MENU_STRING = "Favorites";

        private const string FAVORITES_FILE = "favorites.txt";

        private static FavoritesMenu TheOneFavoritesMenu = null;

        private List<String> Favorites;

        private FavoritesMenu()
        {
            Favorites = new List<string>();

            LoadFavorites();
        }

        ~FavoritesMenu()
        {
            WriteFavorites();
        }

        public static FavoritesMenu GetInstance()
        {
            if (TheOneFavoritesMenu == null)
            {
                TheOneFavoritesMenu = new FavoritesMenu();
            }

            return TheOneFavoritesMenu;
        }

        // Writes all the current favorites to the file, called on destruction
        private void WriteFavorites()
        {
            Favorites.Remove(HomeMenu.HOME_MENU_STRING);

            using (StreamWriter writer = new StreamWriter(File.Open(FAVORITES_FILE, FileMode.Truncate)))
            {
                foreach (string favorite in Favorites)
                {
                    writer.WriteLine(favorite);
                }
            }
        }

        // Reads the text file containing the current favorites into the Favorites list
        // Creates the file if it does not exist
        // The home menu is automatically added to the list
        private void LoadFavorites()
        {
            Favorites.Add(HomeMenu.HOME_MENU_STRING);

            if (File.Exists(FAVORITES_FILE))
            {
                string[] readFavorites = File.ReadAllLines(FAVORITES_FILE); 

                foreach (string favorite in readFavorites)
                {
                    Favorites.Add(favorite);
                }
            }
            else
            {
                File.Create(FAVORITES_FILE);
            }
        }

        public void AddFavorite(string option)
        {
            // The favorites cant be added to the favorites
            if (!Favorites.Contains(option) && 
                !option.Equals(FavoritesMenu.FAVORITES_MENU_STRING) &&
                !option.Equals(HomeMenu.HOME_MENU_STRING) &&
                !option.Equals(CreditsMenu.CREDITS_MENU_STRING) &&
                !option.Equals(WelcomeMenu.WELCOME_MENU_STRING) &&
                !option.Equals(EscapeMenu.ESCAPE_MENU_STRING))
            {
                Favorites.Add(option);
            }
        }

        public void RemoveFavorite(string option)
        {
            // The home menu cannot be removed from the list
            if (Favorites.Contains(option) && !option.Equals(HomeMenu.HOME_MENU_STRING))
            {
                Favorites.Remove(option);
            }
        }

        public void Display()
        {
            string header = "Favorites";

            ArrowedMenu.GetInstance().Load(header, true, Favorites.ToArray());
            ArrowedMenu.GetInstance().Display();
        }

        public IMenu GetNextMenu()
        {
            return ArrowedMenu.GetInstance().GetNextMenu();
        }
    }
}
