using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HangmanGame
{
    internal class HangmanLib
    {
        static string titleText = "     +--------------------+\n" +
                                         "     |  The hangman game  |\n" +
                                         "     +--------------------+\n";
        static string menuText = "\n Choose what would you like to do:\n" +
                                        " 1. Play the game\n" +
                                        " 2. Instructions\n" +
                                        " 3. Exit\n" +
                                        " Option selected: ";
        static StringBuilder sbHangman = new StringBuilder(@"
 ╔══════╗
 ║       
 ║       
 ║        
 ║        
 ║
 ╠════════╗
 ╚════════╝   ");
        static string exitMessage = "\n Do you want to try again?\n" +
                                    "   1. Yes! " +
                                    "   0. Nah..." +
                                    "\n Your choice: ";
        static Random random = new Random();

        public static int DisplayMenu()
        {
            int option;

            Console.Write(titleText + menuText);
            while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 3)
            {
                Console.Clear();
                Console.Write("\n Please select a valid option!\n");
                Console.Write(menuText);
            }

            return option;
        }
        public static void ExecuteGame()
        {
            char[] parts = { '|', 'O', '|', '/', '\\', '/', '\\' };
            int[] partsPos = { 21, 32, 43, 42, 44, 54, 56 };
            string[] words = { "plane", "guy", "thrombosis", "wall" };
            char letter;

            while (true)
            {
                List<char> chosenLettersList = new List<char>();
                string selectedWord = words[random.Next(0, words.Length)];
                string convertedWord = ConvertWordToHangmanStyle(selectedWord);
                int errors = 0;
                int tries = 0;

                while (true)
                {
                    Console.Write(sbHangman);
                    Console.WriteLine(convertedWord);
                    Console.Write("\n Letters: ");
                    foreach (char i in chosenLettersList)
                    {
                        if (!(chosenLettersList.IndexOf(i) == 0)) { Console.Write($" - {i}"); }
                        else { Console.Write($"{i}"); }    
                    }

                    letter = char.ToLower(GetValidLetter("\n\n Press a letter to guess the word: "));
                    while (chosenLettersList.Contains(letter))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n You already choose that letter.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        letter = char.ToLower(GetValidLetter("\n Press a letter to guess the word: "));
                    }

                    chosenLettersList.Add(letter);
                    if (selectedWord.Contains(letter))
                    {
                        convertedWord = CompleteWord(selectedWord, convertedWord, letter);
                        if (!convertedWord.Contains('_'))
                        {
                            Console.Clear();
                            Console.WriteLine($"\n You solved the word \"{selectedWord}\"!");
                            break;
                        }
                    }
                    else
                    {
                        sbHangman[partsPos[errors]] = parts[errors];
                        errors++;
                        if (errors == 7)
                        {
                            Console.Clear();
                            Console.WriteLine("\n You hanged the man! D: " +
                                             $"The word was \"{selectedWord}\"");
                            break;
                        }
                    }
                    tries++;

                    Console.Clear();
                }
                Console.WriteLine(sbHangman);
                if (SelectOption(exitMessage, 0, 1) == 0) { break; }
                Console.Clear();
            }
        }

        public static void DisplayInstructions()
        {
            Console.Clear();
            Console.WriteLine("\n How to play:");
            Console.ReadKey();
        }

        static char GetValidLetter(string prompt)
        {
            char letter;

            Console.Write(prompt);
            letter = Console.ReadKey().KeyChar;
            while (!char.IsLetter(letter))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n That's not a letter, try again.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(prompt);
                letter = Console.ReadKey().KeyChar;
            }

            return letter;
        }

        static string ConvertWordToHangmanStyle(string word)
        {
            string convertedWord = "";

            for (int i = 0; i < word.Length; i++)
            {
                if (char.IsLetter(word[i]))
                {
                    convertedWord += '_';
                }
            }

            return convertedWord;
        }

        static string CompleteWord(string word, string incompleteWord, char letter)
        {
            StringBuilder wordCopy = new StringBuilder(incompleteWord);

            for (int i = 0; i < word.Length; i++)
            {
                i = word.IndexOf(letter, i);
                if (i != -1) { wordCopy[i] = letter; }
                else { break; }
            }

            return wordCopy.ToString();
        }

        static int SelectOption(string prompt, int min, int max)
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
