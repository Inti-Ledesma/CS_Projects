using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Calculator.CalculatorOperations;

namespace Calculator
{
    internal class Program
    {
        static void Main()
        {
            string menuPrompt = "\n What would you like to do?\n 1. Sum\n 2. Substract\n"+
                                " 3. Multiply\n 4. Divide\n 5. Exit app\n\n Option selected: ";
            int option;
            double num1;
            double num2;
            double result;

            Console.WriteLine(" ---------------------------------------\n"+
                              "  Welcome to my Calculator program! :D\n"+
                              " ---------------------------------------");
            while (true) {
                Console.Write(menuPrompt);
                while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 5)
                {
                    Console.Clear();
                    Console.Write($"\n That's not a valid option!\n{menuPrompt}");
                }
                if (option == 5) break;

                num1 = GetValidNumber("\n Please, enter the first number: ", false);
                num2 = GetValidNumber("\n Please, enter the second number: ", option == 4);
                result = PerformOperation(num1,num2,option);

                Console.Write($"\n The final result is {result:F2}\n\n"+
                               " Press any key to go back to the menu...");
                Console.ReadLine();
                Console.Clear();
            }
            Console.Write("\n Thank for using the app, have a great day!");
            Console.ReadKey();
        }
    }
}
