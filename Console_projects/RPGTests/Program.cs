using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RPGTests
{
    internal class Program
    {
        static int mapLen = 20;
        static void Main()
        {
            StringBuilder sbMap = new StringBuilder("  █████████████████\n" +
                                                    "  █ O ██          █\n" +
                                                    "  █   ██      █   █\n" +
                                                    "  ██ ███      █   █\n" +
                                                    "  █           █   █\n" +
                                                    "  █    █      █   █\n" +
                                                    "  ██████████████ ██\n");
            int index;

            while (true)
            {
                Console.Write("\n"+sbMap);
                ConsoleKey keyPressed = Console.ReadKey(true).Key;
                //Console.WriteLine($"Key pressed: {Console.ReadKey(true).Key}");

                index = sbMap.ToString().IndexOf('O');
                if (!IsWall(keyPressed, index, sbMap))
                {
                    MovePlayer(keyPressed, index, ref sbMap);
                }

                Console.Clear();
            }
        }

        static bool IsWall(ConsoleKey direction, int playerPos, StringBuilder map)
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

        static void MovePlayer(ConsoleKey direction, int playerPos, ref StringBuilder map)
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
    }
}

//░▒▓█Ø
