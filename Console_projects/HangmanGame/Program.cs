using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static HangmanGame.HangmanLib;

namespace HangmanGame
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "The hangman game";
            int option;
            while (true)
            {
                option = DisplayMenu();

                if (option == 1) { ExecuteGame(); }
                else if (option == 2) { DisplayInstructions(); }
                else { break; }
                Console.Clear();
            }
            Console.Write("\n Thank you for using the app, have a great day!");
            Console.ReadKey(true);
        }
    }
}
