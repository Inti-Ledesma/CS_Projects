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
        static void Main()
        {
            Console.Title = "The prisoner rpg";
            bool exit = false;
            while (!exit)
            {
                switch (DisplayMenu())
                {
                    case 1:
                        ExecuteGame();
                        break;
                    case 2:
                        DisplayInstructions();
                        break;
                    case 3:
                        exit = true;
                        break;
                }
                Console.Clear();
            }
            Console.Write("\n Thank you for using the app, have a great day!");
            Console.ReadKey();
        }
    }
}
//░▒▓█Ø
