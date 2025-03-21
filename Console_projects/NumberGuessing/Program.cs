using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NumberGuessing.NumberGuessingLib;

namespace NumberGuessing
{
    internal class Program
    {
        static void Main()
        {
            int option;
            while (true)
            {
                Console.Write(titleText + menuText);
                while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 3)
                {
                    Console.Clear();
                    Console.Write("\n Please select a valid option!\n");
                    Console.Write(menuText);
                }
                Console.Clear();

                if (option == 1) { NumberGuessingGame(); }
                else if (option == 2) { DisplayInstructions(); }
                else { break; }
            }
            Console.Write("\n Thank you for using the app, have a great day!");
            Console.ReadKey();
        }
    }
}
