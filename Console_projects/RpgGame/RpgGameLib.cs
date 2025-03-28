using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame
{
    internal class RpgGameLib
    {
        private static readonly int mapLen = 20;
        private static readonly List<int> enemiesPos = new List<int> { 84, 87, 71, 34, 116 };
        private static readonly List<string> enemiesNames = new List<string>() { "Cell Guard 1",
                                                                                 "Cell Guard 2",
                                                                                 "Hallway Guard 1",
                                                                                 "Hallway Guard 2",
                                                                                 "Exit Guard" };

        internal static void MovePlayer(ConsoleKey direction, int playerPos, ref StringBuilder map)
        {
            switch (direction)
            {
                case ConsoleKey.RightArrow:
                    map[playerPos + 1] = 'O';
                    map[playerPos] = ' ';
                    break;
                case ConsoleKey.LeftArrow:
                    map[playerPos - 1] = 'O';
                    map[playerPos] = ' ';
                    break;
                case ConsoleKey.UpArrow:
                    map[playerPos - mapLen] = 'O';
                    map[playerPos] = ' ';
                    break;
                case ConsoleKey.DownArrow:
                    map[playerPos + mapLen] = 'O';
                    map[playerPos] = ' ';
                    break;
            }
        }

        internal static bool IsWall(ConsoleKey direction, int playerPos, StringBuilder map)
        {
            switch (direction)
            {
                case ConsoleKey.RightArrow:
                    if (map[playerPos + 1] == '█') { return true; }
                    break;
                case ConsoleKey.LeftArrow:
                    if (map[playerPos - 1] == '█') { return true; }
                    break;
                case ConsoleKey.UpArrow:
                    if (map[playerPos - mapLen] == '█') { return true; }
                    break;
                case ConsoleKey.DownArrow:
                    if (map[playerPos + mapLen] == '█') { return true; }
                    break;
            }

            return false;
        }

        internal static int IsEnemy(ConsoleKey direction, int playerPos, StringBuilder map)
        {
            switch (direction)
            {
                case ConsoleKey.RightArrow:
                    if (map[playerPos + 1] == 'Ø') { return playerPos+1; }
                    break;
                case ConsoleKey.LeftArrow:
                    if (map[playerPos - 1] == 'Ø') { return playerPos - 1; }
                    break;
                case ConsoleKey.UpArrow:
                    if (map[playerPos - mapLen] == 'Ø') { return playerPos - mapLen; }
                    break;
                case ConsoleKey.DownArrow:
                    if (map[playerPos + mapLen] == 'Ø') { return playerPos + mapLen; }
                    break;
            }

            return -1;
        }

        internal static int FindEnemyOnList(int enemyPos, List<Character> enemiesList)
        {
            string enemyName = enemiesNames[enemiesPos.IndexOf(enemyPos)];
            
            for (int i = 0; i < enemiesList.Count; i++)
            {
                if (enemiesList[i] != null)
                {
                    if (enemiesList[i].Name == enemyName) { return i; }
                }
            }

            return -1;
        }

        static internal int EngageFight(Character player, Character enemy)
        {
            string turnOptions = " What would you like to do?\n" +
                                 " 1. Attack   2. Heal\n" +
                                 " Option: ";

            Console.WriteLine($"\n Engaging fight with '{enemy.Name}'");

            while (true)
            {
                Console.WriteLine($"\n <---- {player.Name}'s turn ---->");
                if (SelectOption(turnOptions, 1, 2) == 1)
                { player.Attack(ref enemy); }
                else { player.Heal(); }
                Console.ReadKey(true);
                if (enemy.IsDead) { return 1; }

                Console.WriteLine($"\n <---- {enemy.Name}'s turn ---->");
                enemy.Attack(ref player);
                Console.ReadKey(true);
                if (player.IsDead) { return -1; }

                Console.Clear();
            }
        }

        internal static int SelectOption(string prompt, int min, int max)
        {
            int option;

            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out option) || option < min || option > max)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n That's not a valid option! Try again...\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(prompt);
            }

            return option;
        }
    }
}
