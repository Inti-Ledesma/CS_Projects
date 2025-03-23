using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NumberGuessing
{
    internal class NumberGuessingLib
    {
        static readonly Random roll = new Random();
        public static string titleText = "     +----------------------+\n" +
                                         "     | Number guessing game |\n" +
                                         "     +----------------------+\n";
        public static string menuText = "\n Choose what would you like to do:\n" +
                                        " 1. Play the game\n" +
                                        " 2. Instructions\n" +
                                        " 3. Exit\n" +
                                        " Option selected: ";
        public static void ExecuteGame()
        {
            int exit=1;
            while (exit!=0)
            {
                int randomNum = roll.Next(1, 11);
                int result = 1;
                int tries = 0;
                while (result != 0)
                {
                    int guess = GetValidNumber("\n Please enter your guess: ");
                    tries++;
                    result = GetComparisonResult(randomNum, guess);
                    InformResult(result, tries);
                }

                WriteExitMessage();
                while (!int.TryParse(Console.ReadLine(), out exit) || exit < 0 || exit > 1)
                {
                    Console.Clear();
                    Console.WriteLine("\n Please select a valid option!\n");
                    WriteExitMessage();
                }
                Console.Clear();
                if (exit == 0) break;
                else Console.WriteLine("\n Welcome back!");
            }
        }
        public static void DisplayInstructions()
        {
            Console.Write("\n How to play:\n\n" +
                          " A number between 1 and 10 will be generated.\n" +
                          " If you guess the correct number you win!\n" +
                          " Try getting it in the least ammount of tries possible!\n\n" +
                          " Press any key to go back to the menu...");
            Console.ReadKey();
            Console.Clear();
        }
        static int GetValidNumber(string prompt)
        {
            int number;

            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.Write($"\n That's not a valid number!\n{prompt}");
            }
            
            return number;
        }
        static int GetComparisonResult(int num1, int num2)
        {
            if (num1 > num2) { return -1; }
            else if (num1 < num2) { return 1; }
            else { return 0; }
        }
        static void InformResult(int result, int tries)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            switch (result)
            {
                case -1:
                    Console.WriteLine("\n Your guess is too low.");
                    break;
                case 0:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\n Your guess is correct! ");
                    if (tries > 1) { Console.WriteLine($"({tries} tries)"); }
                    else { Console.WriteLine($"({tries} try!)"); }
                    break;
                case 1:
                    Console.WriteLine("\n Your guess is too high.");
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        static void WriteExitMessage()
        {
            Console.WriteLine("\n Do you want to try again?");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("   1. Yes! ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   0. Nah...\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\n Your choice: ");
        }
    }
}
