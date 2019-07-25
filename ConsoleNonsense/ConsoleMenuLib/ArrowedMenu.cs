// Tyler Percario
// Console Nonsense 7/12/19
// Class for handling menu option selection using arrow keys

using System;
using System.Collections.Generic;
using System.Drawing;

using Console = Colorful.Console;

namespace ConsoleMenuLib
{
    public class ArrowedMenu
    {
        // Struct containing the console window coordinates for menu options 
        private struct ArrowedMenuOption
        {
            public int XLocation { set; get; }
            public int YLocation { set; get; }

            public string OptionString { set; get; }

            public ArrowedMenuOption(int x, int y, string option)
            {
                XLocation = x;
                YLocation = y;

                OptionString = option;
            }
        }

        // Class for handling multiple pages of options
        private class ArrowedMenuPage
        {
            public ArrowedMenuOption[] PageOptions { private set; get; }
            public int PageBaseXCoord { private set; get; }
            public int PageBaseYCoord { private set; get; }

            private int NextOpenOptionSlotIndex;

            public ArrowedMenuPage(int nOptions, int baseX, int baseY)
            {
                PageOptions = new ArrowedMenuOption[nOptions];

                NextOpenOptionSlotIndex = 0;

                PageBaseXCoord = baseX;
                PageBaseYCoord = baseY;
            }

            // Returns whether the option was successfully added to the page.
            // Returns false if the page is full
            public bool AddNewOption(string optionString) 
            {
                bool successful = false;

                if (NextOpenOptionSlotIndex < PageOptions.Length) 
                {
                    int newOptionX = PageBaseXCoord;
                    int newOptionY = PageBaseYCoord + (NextOpenOptionSlotIndex * LINE_SPACING);

                    PageOptions[NextOpenOptionSlotIndex] = new ArrowedMenuOption(newOptionX, newOptionY, optionString);

                    ++NextOpenOptionSlotIndex;

                    successful = true;
                }

                return successful;
            }
        }

        // The arrowed menu is a singleton
        private static ArrowedMenu TheOnlyArrowedMenu = null;

        private static Color HOVER_COLOR = Color.Aqua;
        private const int MAX_PAGE_SIZE = 7;

        private const int LINE_SPACING = 1;

        private int CursorX; 
        private int CursorY; 

        private int HoveredOptionIndex;
        private int CurrentlyOpenPageIndex;

        private List<ArrowedMenuPage> Pages;

        private string Header;
        private string Footer;

        private ArrowedMenu()
        {
            Reset();
        }

        // Returns the instance of the arrowed menu, creates it if it hasnt been yet
        public static ArrowedMenu GetInstance()
        {
            if (TheOnlyArrowedMenu == null)
            {
                TheOnlyArrowedMenu = new ArrowedMenu();
            }

            return TheOnlyArrowedMenu;
        }

        private void Reset()
        {
            CursorX = 0;
            CursorY = 0;

            Header = "";
            Footer = "";

            HoveredOptionIndex = 0;
            CurrentlyOpenPageIndex = 0;

            Pages = new List<ArrowedMenuPage>();
        }

        // Finds the number of pages that are needed to accomodate the number of options
        private int DetermineNumberOfPages(int nOptions)
        {
            int nPages = nOptions / MAX_PAGE_SIZE;

            if ((nOptions % MAX_PAGE_SIZE) > 0)
            {
                ++nPages;
            }

            return nPages;
        }

        public void Load(params string[] menuOptions)
        {
            Load("", "", menuOptions);
        }

        public void Load(string headerOrFooter, bool useAsHeader, params string[] menuOptions)
        {
            if (useAsHeader)
            {
                Load(headerOrFooter, "", menuOptions);
            }
            else
            {
                Load("", headerOrFooter, menuOptions);
            }
        }

        // Loads a new set of menu options into the arrowed menu
        public void Load(string header, string footer, params string[] menuOptions)
        {
            Reset();

            Header = header;
            Footer = footer;

            int nPages = DetermineNumberOfPages(menuOptions.Length);

            int pageBaseX = CursorX;

            // + LINE_SPACING because the current page number is printed before the options
            int pageBaseY = CursorY + LINE_SPACING;

            if (Header != "")
            {
                pageBaseY += LINE_SPACING * 2;
            }

            int currentMenuOptionIndex = 0;

            for (int i = 0; i < nPages; ++i)
            {
                int currentPageSize = menuOptions.Length - currentMenuOptionIndex;

                if (currentPageSize > MAX_PAGE_SIZE)
                {
                    currentPageSize = MAX_PAGE_SIZE;
                }

                Pages.Add(new ArrowedMenuPage(currentPageSize, pageBaseX, pageBaseY));

                // Tries to add this menu option to the page, it will return false if the page is full
                // thus moving on to the next page
                while (currentMenuOptionIndex < menuOptions.Length && 
                       Pages[i].AddNewOption(menuOptions[currentMenuOptionIndex]))
                {
                    ++currentMenuOptionIndex;
                }
            }
        }

        private void SetCursorToXY()
        {
            Console.SetCursorPosition(CursorX, CursorY);
        }

        // Replaces what ever is at line y with the replacement string in the new color
        private void ReplaceLineAtY(int y, string replacement, Color newColor)
        {
            Console.SetCursorPosition(CursorX, y);

            Console.Write(new string(' ', Console.WindowWidth));

            Console.SetCursorPosition(CursorX, y);

            Console.Write(replacement, newColor);
        }

        // Sets the first option in the menu to be the current one under selection
        private void HoverFirstOption()
        {
            CursorX = 0;
            CursorY = Pages[CurrentlyOpenPageIndex].PageOptions[0].YLocation; 

            ReplaceLineAtY(CursorY, Pages[CurrentlyOpenPageIndex].PageOptions[0].OptionString, HOVER_COLOR);

            HoveredOptionIndex = 0;
        }

        // Sets whatever the index of the item is pointing to be hovered over
        private void HoverOverIndex()
        {
            CursorX = 0;
            CursorY = Pages[CurrentlyOpenPageIndex].PageOptions[HoveredOptionIndex].YLocation; 

            ReplaceLineAtY(CursorY, Pages[CurrentlyOpenPageIndex].PageOptions[HoveredOptionIndex].OptionString, 
                HOVER_COLOR);
        }

        // Deselects whatever option is currently selected
        private void UnhoverIndex()
        {
            CursorX = 0;
            CursorY = Pages[CurrentlyOpenPageIndex].PageOptions[HoveredOptionIndex].YLocation; 

            ReplaceLineAtY(CursorY, Pages[CurrentlyOpenPageIndex].PageOptions[HoveredOptionIndex].OptionString, 
                Color.White);
        }

        // Prints out the given page 
        private void DisplayAtPageNumber(int pageNumber)
        {
            Console.Clear();
            CursorX = 0;
            CursorY = 0;
            SetCursorToXY();

            if (Header != "")
            {
                Console.Write(Header, Color.White);
                CursorY += LINE_SPACING;
                CursorY += LINE_SPACING;
                SetCursorToXY();
            }

            Console.Write("Page {0}/{1}", Color.White, pageNumber + 1, Pages.Count);

            CursorX = Pages[pageNumber].PageBaseXCoord;
            CursorY = Pages[pageNumber].PageBaseYCoord;
            SetCursorToXY();

            foreach (ArrowedMenuOption option in Pages[pageNumber].PageOptions)
            {
                Console.Write(option.OptionString, Color.White);
                CursorY += LINE_SPACING;
                SetCursorToXY();
            }

            if (Footer != "")
            {
                CursorY += LINE_SPACING;
                SetCursorToXY();
                Console.Write(Footer, Color.White);
            }

            HoverFirstOption();
        }

        // Prints out the first page of options
        public void Display()
        {
            DisplayAtPageNumber(0);
        }

        // Goes to the next (+1) page, wraps to first page 
        private void DisplayNextPage()
        {
            ++CurrentlyOpenPageIndex;

            if (CurrentlyOpenPageIndex >= Pages.Count)
            {
                CurrentlyOpenPageIndex = 0;
            }

            DisplayAtPageNumber(CurrentlyOpenPageIndex);
        }

        // Goes to previous page (-1), wraps to last page
        private void DisplayPrevPage()
        {
            --CurrentlyOpenPageIndex;

            if (CurrentlyOpenPageIndex < 0)
            {
                CurrentlyOpenPageIndex = Pages.Count - 1;
            }

            DisplayAtPageNumber(CurrentlyOpenPageIndex);
        }

        private bool IsInputValid(ConsoleKey key)
        {
            return key == ConsoleKey.Enter || key == ConsoleKey.UpArrow || 
                   key == ConsoleKey.DownArrow || key == ConsoleKey.LeftArrow ||
                   key == ConsoleKey.RightArrow || key == ConsoleKey.F || 
                   key == ConsoleKey.U || key == ConsoleKey.Escape ||
                   key == ConsoleKey.Spacebar;
        }

        // Returns the next valid user key press
        private ConsoleKey GetUserPress()
        {
            ConsoleKey key = ConsoleKey.A;

            do
            {
                key = Console.ReadKey().Key;

            } while (!IsInputValid(key));

            return key;
        }

        private void UpdateOnVerticalKey(ConsoleKey pressedKey)
        {
            if (pressedKey == ConsoleKey.UpArrow)
            {
                if (HoveredOptionIndex == 0)
                {
                    HoveredOptionIndex = Pages[CurrentlyOpenPageIndex].PageOptions.Length - 1;
                }
                else
                {
                    --HoveredOptionIndex;
                }
            }
            else
            {
                if (HoveredOptionIndex == Pages[CurrentlyOpenPageIndex].PageOptions.Length - 1)
                {
                    HoveredOptionIndex = 0;
                }
                else
                {
                    ++HoveredOptionIndex;
                }
            }
        }

        private void UpdateOnHorizontalKey(ConsoleKey pressedKey)
        {
            if (Pages.Count > 1)
            {
                if (pressedKey == ConsoleKey.RightArrow)
                {
                    DisplayNextPage();
                }
                else
                {
                    DisplayPrevPage();
                }
            }
        }

        private void UpdateFavorites(ConsoleKey pressedKey)
        {
            string option = Pages[CurrentlyOpenPageIndex].PageOptions[HoveredOptionIndex].OptionString;

            if (pressedKey == ConsoleKey.F)
            {
                FavoritesMenu.GetInstance().AddFavorite(option);
            }
            else
            {
                FavoritesMenu.GetInstance().RemoveFavorite(option);
            }
        }

        private void UpdateOnKey(ConsoleKey pressedKey)
        {
            if (pressedKey == ConsoleKey.UpArrow || pressedKey == ConsoleKey.DownArrow)
            {
                UpdateOnVerticalKey(pressedKey);
            }
            else if (pressedKey == ConsoleKey.RightArrow || pressedKey == ConsoleKey.LeftArrow)
            {
                UpdateOnHorizontalKey(pressedKey);
            }
            else
            {
                UpdateFavorites(pressedKey);
            }
        }

        // Runs the selection for the next menu, interacts with user
        // discards the out string parameter
        public IMenu GetNextMenu()
        {
            string temp = "";

            return GetNextMenu(out temp);
        }

        // Runs the selection for the next menu, interacts with user
        // out parameter becomes the raw string of the option chosen
        public IMenu GetNextMenu(out string chosenString)
        {
            ConsoleKey pressedKey = ConsoleKey.A;

            while ((pressedKey = GetUserPress()) != ConsoleKey.Enter && 
                pressedKey != ConsoleKey.Escape &&
                pressedKey != ConsoleKey.Spacebar)
            {
                UnhoverIndex();

                UpdateOnKey(pressedKey);

                HoverOverIndex();
            }

            string selectedItem = "Escape";

            if (pressedKey != ConsoleKey.Escape)
            {
                selectedItem = Pages[CurrentlyOpenPageIndex].PageOptions[HoveredOptionIndex].OptionString;
            }

            chosenString = selectedItem;

            return MenuFactory.GetMenuFromString(selectedItem);
        }
    }
}
