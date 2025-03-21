using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dice_game.Dice_Game_Library;

namespace Dice_game
{
    internal class Program
    {
        static void Main()
        {
            while (true)
            {
                int option;

                Console.Write("       +-----------------+\n" +
                              "       |  The Dice Game  |\n" +
                              "       +-----------------+\n" +
                                    $"{menuText}");
                while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 3)
                {
                    Console.Clear();
                    Console.Write("\n Please select a valid option!\n");
                    Console.Write(menuText);
                }
                Console.Clear();

                if (option == 1) { DiceGame(); }
                else if (option == 2) { DisplayInstructions(); }
                else { break; }
            }
            Console.Write("\n Thank for using the app, have a great day!");
            Console.ReadKey();
        }
    }
}
