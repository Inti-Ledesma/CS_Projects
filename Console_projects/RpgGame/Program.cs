using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static RpgGame.RpgGameLib;

namespace RpgGame
{
    internal class Program
    {
        const string map = "  █████████████████\n" +
                           "  █ O ██P     Ø   █\n" +
                           "  █   █████ ███   █\n" +
                           "  ██ ███  █Ø█P█   █\n" +
                           "  █ Ø  Ø  █ █ █   █\n" +
                           "  █    █      █ Ø █\n" +
                           "  ██████████████▒██\n";
        static void Main()
        {
            Character player = new Character(30, 7, 10, 100, "Player");
            List<Character> enemiesList = new List<Character>();

            Character firstCellGuard = new Character(23, 5, 0, 0, "Cell Guard 1");
            Character secondCellGuard = new Character(19, 4, 0, 0, "Cell Guard 2");
            Character firstHallwayGuard = new Character(27, 6, 0, 0, "Hallway Guard 1");
            Character secondHallwayGuard = new Character(24, 5, 0, 0, "Hallway Guard 2");
            Character exitGuard = new Character(30, 7, 0, 0, "Exit Guard");

            enemiesList.Add(firstCellGuard);
            enemiesList.Add(secondCellGuard);
            enemiesList.Add(firstHallwayGuard);
            enemiesList.Add(secondHallwayGuard);
            enemiesList.Add(exitGuard);

            int playerIndex;
            int objectPos;
            int enemyPosOnList;
            int fightResult;

            StringBuilder sbMap = new StringBuilder(map);

            while (true)
            {
                Console.Write("\n" + sbMap);
                ConsoleKey keyPressed = Console.ReadKey(true).Key;

                playerIndex = sbMap.ToString().IndexOf('O');

                if (!IsWall(keyPressed, playerIndex, sbMap))
                {
                    objectPos = IsEnemy(keyPressed, playerIndex, sbMap);
                    if (objectPos != -1)
                    {
                        enemyPosOnList = FindEnemyOnList(objectPos, enemiesList);
                        fightResult = EngageFight(player, enemiesList[enemyPosOnList]);

                        if (fightResult != -1)
                        {
                            Console.WriteLine($"\n You've killed the {enemiesList[enemyPosOnList].Name}");
                            sbMap[objectPos] = ' ';
                            enemiesList[enemyPosOnList] = null;
                            enemiesList.Remove(enemiesList[enemyPosOnList]);
                            Console.ReadKey(true);
                        }
                        else
                        {
                            Console.WriteLine($"\n You've died to {enemiesList[enemyPosOnList].Name}");
                            Console.ReadKey(true);
                            break;
                        }
                    }
                    else
                    {
                        MovePlayer(keyPressed, playerIndex, ref sbMap);
                    }
                }

                Console.Clear();
            }
            //Console.ReadKey();
        }
    }
}
//░▒▓█Ø
