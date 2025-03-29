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
        private const string titleText = "\n     +--------------------+\n" +
                                           "     |  The hangman game  |\n" +
                                           "     +--------------------+\n";
        private const string menuText = "\n Choose what would you like to do:\n" +
                                          " 1. Play the game\n" +
                                          " 2. Instructions\n" +
                                          " 3. Exit\n" +
                                          " Option selected: ";
        private const string HANGMAN = " ╔══════╗\n"+
                                       " ║       \n"+
                                       " ║       \n"+
                                       " ║        \n"+
                                       " ║        \n"+
                                       " ║\n"+
                                       " ╠════════╗\n"+
                                       " ╚════════╝   ";
        private const string exitMessage = "\n Do you want to try again?\n" +
                                           "   1. Yes! " +
                                           "   0. Nah..." +
                                           "\n Your choice: ";
        static readonly Random random = new Random();

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
            Console.Clear();

            return option;
        }
        public static void ExecuteGame()
        {
            StringBuilder sbHangman = new StringBuilder(HANGMAN);
            char[] parts = { '|', 'O', '|', '/', '\\', '/', '\\' };
            int[] partsPos = { 18, 28, 38, 37, 39, 48, 50 };
            string[] words = { "plane", "guy", "thrombosis", "wall" };
            char letter;

            while (true)
            {
                sbHangman = new StringBuilder(HANGMAN);
                List<char> chosenLettersList = new List<char>();
                string selectedWord = words[random.Next(0, words.Length)];
                string convertedWord = ConvertWordToHangmanStyle(selectedWord);
                int errors = 0;
                int tries = 0;

                while (true)
                {
                    Console.Write(sbHangman);
                    Console.WriteLine(convertedWord);
                    DisplayListOfLetter(chosenLettersList);

                    letter = char.ToLower(GetValidLetter("\n Press a letter to guess the word: "));
                    while (chosenLettersList.Contains(letter))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n You already choose that letter.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        letter = char.ToLower(GetValidLetter("\n Press a letter to guess the word: "));
                    }

                    chosenLettersList.Add(letter);
                    tries++;
                    if (selectedWord.Contains(letter))
                    {
                        convertedWord = CompleteWord(selectedWord, convertedWord, letter);
                        if (!convertedWord.Contains('_'))
                        {
                            Console.Clear();
                            Console.WriteLine($"\n You solved the word \"{selectedWord}\"!\n");
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
                            Console.WriteLine("\n You hanged the man! D:\n" +
                                             $" The word was \"{selectedWord}\"\n");
                            break;
                        }
                    }

                    Console.Clear();
                }

                Console.WriteLine($" Letters guessed: {tries}   Errors: {errors}");
                Console.Write(sbHangman);
                Console.WriteLine(convertedWord);
                DisplayListOfLetter(chosenLettersList);
                if (SelectOption(exitMessage, 0, 1) == 0) { break; }
                Console.Clear();
            }
        }

        public static void DisplayInstructions()
        {
            Console.Clear();
            Console.Write("\n How to play:\n\n" +
                          " In this game you'll have to guess a word by" +
                          " entering the letters that make it up.\n\n" +
                          " - If the letter is part of the word" +
                          " it will be placed on its respective spot.\n" +
                          " - If the letter is not part of the word" +
                          " it will add a part to the hangman draw as an error\n\n" +
                          " You have a maximum of 7 errors per word.\n\n" +
                          " If you guess the word correctly you win!\n" +
                          " But if you make too many mistakes the man will be hanged.\n" +
                          " Try not to kill him please.\n\n" +
                          " Press any key to go back to the menu...");
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

        static void DisplayListOfLetter(List<char> list)
        {
            Console.Write("\n Letters: ");
            foreach (char i in list)
            {
                if (!(list.IndexOf(i) == 0)) { Console.Write($" - {i}"); }
                else { Console.Write($"{i}"); }
            }
            Console.WriteLine();
        }
    }
}
