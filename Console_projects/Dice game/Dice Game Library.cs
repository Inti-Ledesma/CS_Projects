using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice_game
{
    internal class Dice_Game_Library
    {
        static readonly Random roll = new Random();
        public static string menuText = "\n Choose what would you like to do:\n" +
                                        " 1. Play the game\n" +
                                        " 2. Instructions\n" +
                                        " 3. Exit\n" +
                                        " Option selected: ";
        public static void DiceGame()
        {
            while (true)
            {
                const int ROUNDS = 9;
                int playerNum;
                int CPUNum;
                int playerScore = 0;
                int cpuScore = 0;
                int winner;
                int exit;

                for (int i = 0; i < ROUNDS; i++)
                {
                    Console.Write($"\n          [Round {i+1}]\n" +
                                   "\n Press any key to roll the dice.");
                    Console.ReadKey();
                    System.Threading.Thread.Sleep(500);
                    playerNum = RollDice();
                    Console.Write($"\n You rolled a {playerNum}\n ...");
                    System.Threading.Thread.Sleep(500);
                    CPUNum = RollDice();
                    Console.WriteLine($"\n CPU rolled a {CPUNum}");

                    winner = DetermineWinner(playerNum, CPUNum);
                    InformOutcome(winner);
                    IncreaseScores(winner, ref playerScore, ref cpuScore);

                    Console.Write($"\n Current score:\n Player: {playerScore} - CPU: {cpuScore}");
                    Console.ReadKey();
                    Console.Clear();
                }

                Console.WriteLine($"\n Final score:\n Player: {playerScore} - CPU: {cpuScore}");
                winner = DetermineWinner(playerScore, cpuScore);
                InformOutcome(winner);

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
                          " The game it's fairly simple.\n\n" +
                          " You have to roll a dice and the CPU will do the same. There will be three possible outcomes:\n" +
                          " 1. You roll a bigger number than the CPU and gain 1 point.\n" +
                          " 2. CPU rolls a bigger number than you and gains 1 point.\n" +
                          " 3. Both roll the same number ending in a draw and gaining 0 points.\n\n" +
                          " There's a total of 9 rounds and each of you will have a roll per round.\n" +
                          " The one that gained more points wins the game.\n" +
                          " Good luck! (because you'll literally need it)\n\n" +
                          " Press any key to go back to the menu...");
            Console.ReadKey();
            Console.Clear();
        }
        static int RollDice()
        {
            return roll.Next(1, 7);
        }
        static int DetermineWinner(int playerNum, int cpuNum)
        {
            if (playerNum > cpuNum) return 1;
            if (playerNum < cpuNum) return -1;
            return 0;
        }
        static void InformOutcome(int winner)
        {
            switch (winner)
            {
                case -1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n CPU wins!");
                    break;
                case 0:
                    Console.WriteLine("\n It's a draw!");
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n You win!");
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        static void IncreaseScores(int winner, ref int playerScore, ref int cpuScore)
        {
            switch (winner)
            {
                case -1:
                    cpuScore++;
                    break;
                case 0:
                    break;
                case 1:
                    playerScore++;
                    break;
            }
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
