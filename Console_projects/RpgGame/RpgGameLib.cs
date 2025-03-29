using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame
{
    internal class RpgGameLib
    {
        private const string MENUTEXT = "\n     +--------------------+\n" +
                                          "     |  The prisoner rpg  |\n" +
                                          "     +--------------------+\n" +
                                        "\n Choose what would you like to do:\n" +
                                          " 1. Play the game\n" +
                                          " 2. Instructions\n" +
                                          " 3. Exit\n" +
                                          " Option selected: ";
        private const int MAPLEN = 20;
        private const string MAP = "  █████████████████\n" +
                                   "  █   O█P     Ø   █\n" +
                                   "  ██ ██████ ████ ██\n" +
                                   "  █ Ø     █Ø█P█   █\n" +
                                   "  ██ ████Ø█ █ █   █\n" +
                                   "  █    ██     █ Ø █\n" +
                                   "  ██████████████▒██\n";
        internal static int DisplayMenu()
        {
            int option;

            option = SelectOption(MENUTEXT, 1, 3);
            Console.Clear();

            return option;
        }

        internal static void DisplayInstructions()
        {
            Console.Clear();
            Console.Write("\n How to play:\n\n" +
                            " In this game you play as a prisoner who has to escape from their cell.\n\n" +
                            " Your goal will be reaching the exit (▒).\n" +
                            " To do so, you'll have to fight the guards (Ø) custoding the prison.\n" +
                            " On your way you'll also find healing potions (P) to use during combat.\n\n" +
                            " Good luck on your way to be free!\n\n" +
                            " Press any button to continue...");
            Console.ReadKey(true);
        }

        internal static void ExecuteGame()
        {
            StringBuilder sbMap = new StringBuilder(MAP);
            Character player = new Character(30, 7, 10, 5, "Player", 26);
            List<Character> enemiesList = new List<Character>()
            {
                new Character(23, 5, 0, 0, "Cell Guard", 64),
                new Character(19, 4, 0, 0, "Cells Room Guard", 89),
                new Character(27, 6, 0, 0, "Short Hallway Guard", 71),
                new Character(24, 5, 0, 0, "Long Hallway Guard", 34),
                new Character(30, 7, 0, 0, "Exit Guard", 116)
            };

            int nextPos;
            Character enemyOnList;
            int fightResult;
            bool endGame = false;

            while (!endGame)
            {
                Console.WriteLine($"\n{sbMap}");
                do
                {
                    ConsoleKey keyPressed = Console.ReadKey(true).Key;
                    nextPos = GetNextPosition(player.MapPos, keyPressed);
                }while (nextPos == -1);

                /*  - Collision checking -
                 * '█' case represents the walls on the map
                 * 'Ø' case represents the enemies on the map
                 * 'P' case represents the heal potions on the map
                 * '▒' case represents the exit on the map
                 * default case is for the movement, updating the pos on the player object and the map
                 */
                switch (sbMap[nextPos])
                {
                    case '█':
                        break;
                    case 'Ø':
                        enemyOnList = FindEnemyOnList(nextPos, enemiesList);
                        fightResult = EngageFight(player, enemyOnList, sbMap);
                        
                        if (fightResult != -1)
                        {
                            Console.WriteLine($"\n You've killed the '{enemyOnList.Name}'!");
                            sbMap[nextPos] = ' ';
                            // Deletes enemy's object reference
                            enemyOnList = null;
                            enemiesList.Remove(enemyOnList);
                            Console.ReadKey(true);
                        }
                        else
                        {
                            endGame = true;
                            Console.WriteLine($"\n You've died to '{enemyOnList.Name}' :(");
                            Console.ReadKey(true);
                            break;
                        }
                        break;
                    case 'P':
                        sbMap[nextPos] = ' ';
                        player.HealPotions += 3;
                        Console.WriteLine($"\n You got 3 extra healing potions! ({player.HealPotions})");
                        Console.ReadKey(true);
                        break;
                    case '▒':
                        endGame = true;
                        Console.WriteLine("\n You defeated all guards and got out of the prison!");
                        Console.ReadKey(true);
                        break;
                    default:
                        sbMap[nextPos] = 'O';
                        sbMap[player.MapPos] = ' ';
                        player.MapPos = nextPos;
                        break;
                }

                Console.Clear();
            }
        }

        private static int GetNextPosition(int currentIndex, ConsoleKey direction)
        {
            switch (direction)
            {
                case ConsoleKey.RightArrow:
                    return currentIndex + 1;
                case ConsoleKey.LeftArrow:
                    return currentIndex - 1;
                case ConsoleKey.UpArrow:
                    return currentIndex - MAPLEN;
                case ConsoleKey.DownArrow:
                    return currentIndex + MAPLEN;
            }
            return -1;
        }

        private static Character FindEnemyOnList(int enemyPos, List<Character> enemiesList)
        {
            for (int i = 0; i < enemiesList.Count; i++)
            {
                if (enemiesList[i].MapPos == enemyPos) { return enemiesList[i]; }
            }

            return null;
        }

        private static int EngageFight(Character player, Character enemy, StringBuilder map)
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
                Console.WriteLine($"\n{map}");
            }
        }

        private static int SelectOption(string prompt, int min, int max)
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
