using System;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Project
{
    class Wordle
    {
        public void Game()
        {

            Console.WriteLine("Benvingut al wordle!");
            Console.WriteLine("Has d'encertar una paraula catalana de 5 lletres diferents.");
            Console.WriteLine("Tens 6 intents. Bona sort!");
            string solution = LoadWord();
            for (int i = 0; i < 6; i++)
            {
                char[] correct = { 'R', 'R', 'R', 'R', 'R' };
                string attempt = Console.ReadLine();
                if (attempt.Length >= 5)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (attempt[j] == solution[j]) correct[j] = 'G';
                        else
                        {
                            for (int k = 0; k < 5; k++)
                                if (attempt[j] == solution[k]) correct[j] = 'Y';
                        }
                    }
                    bool solved = true;
                    for (int j = 0; j < 5; j++)
                    {
                        switch (correct[j])
                        {
                            case 'R':
                                solved = false;
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.Write(attempt[j]);
                                break;
                            case 'Y':
                                solved = false;
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.Write(attempt[j]);
                                break;
                            case 'G':
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.Write(attempt[j]);
                                break;
                        }
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine();
                    if (solved)
                    {
                        Console.WriteLine("Has encertat!");
                        return;
                    }
                    else if (i < 5)
                    {
                        Console.WriteLine("Torna a provar!");
                    }
                    else
                    {
                        Console.WriteLine("Has perdut...");
                    }

                }
                else if (i < 5)
                {
                    Console.WriteLine("Has escrit una paraula massa curta. Has perdut un intent");
                    Console.WriteLine("Torna a provar!");
                }
                else
                {
                    Console.WriteLine("Has escrit una paraula massa curta. Has perdut un intent");
                    Console.WriteLine("Has perdut...");
                }
            }

        }
        static void Main()
        {
            var menu = new Wordle();

            menu.Game();
        }
        static string LoadWord()
        {
            string[] words = {"pedra","costa","trens","pluja","paret","focus",
                            "metro","estoc","sabre","mural","firma",
                            "ronya","pinya","pomer","mores","basto",
                            "rival","garbi","xaloc","estol","boira",
                            "pobre","poder","venir","petar","polir",
                            "xicot","pixar","cantir","poble","segar"};
            Random rnd = new Random();
            int num = rnd.Next(0, 30);
            string word = words[num];
            return word;
        }
    }
}