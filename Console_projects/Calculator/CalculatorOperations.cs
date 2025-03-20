using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class CalculatorOperations
    {
        public static double GetValidNumber(string prompt, bool isDivision)
        {
            double number;

            Console.Write(prompt);
            while (!double.TryParse(Console.ReadLine(), out number) || (isDivision && number == 0))
            {
                Console.Write($"\n That's not a valid number!\n{prompt}");
            }

            return number;
        }
        
        public static double PerformOperation(double num1, double num2, int option)
        {
            switch (option)
            {
                case 1: return num1 + num2;
                case 2: return num1 - num2;
                case 3: return num1 * num2;
                case 4: return num1 / num2;
                default: return 0;
            }
        }
    }
}
