// Tyler Percario
// ConsoleNonsense 7/12/19
// Interface for all menu objects 

using System;

namespace ConsoleMenuLib
{
    public interface IMenu
    {
        void Display();

        IMenu GetNextMenu();
    }
}
