using System;
using System.IO;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Project
{
    class Wordle
    {
        public void Game()
        {

            Console.WriteLine("Benvingut al wordle. Esculli una opció!");
            Console.WriteLine("1 - Comenci un joc en català");
            Console.WriteLine("2 - Start a game in english");
            Console.WriteLine("3 -  Miri l'historial de partides");
            Console.WriteLine("0 - Sortir");

            var ent = Console.ReadLine();
            switch (ent)
            {
                case "1":
                    CatalanGame();
                    break;
                case "2":
                    EnglishGame();
                    break;
                case "3":
                    Historial(DateTime.Now-DateTime.Now,-1);
                    break;
            }
        }
        public void CatalanGame()
        {
            DateTime now = DateTime.Now;
            Console.WriteLine(now);
            Console.WriteLine("Has d'encertar una paraula catalana de 5 lletres diferents.");
            Console.WriteLine("Tens 6 intents. Bona sort!");
            string solution = LoadWord(@"../../../files/catalanWords.txt");
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
                        TimeSpan rest = DateTime.Now - now;
                        Historial(rest,6-i-1);
                        Console.WriteLine(rest);
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

            TimeSpan rests = DateTime.Now - now;
            Historial(rests,0);
        }

        public void EnglishGame()
        {
            DateTime now = DateTime.Now;

            Console.WriteLine("You have to guess a 5-letter english word");
            Console.WriteLine("You have 6 tries, good luck!");
            string solution = LoadWord(@"../../../files/englishWords.txt");
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
                        Console.WriteLine("You got it!");
                        TimeSpan rest = DateTime.Now - now;
                        Historial(rest, 6 - i - 1);
                        Console.WriteLine(rest);
                        return;
                    }
                    else if (i < 5)
                    {
                        Console.WriteLine("Try again!");
                    }
                    else
                    {
                        Console.WriteLine("You lost...");
                    }

                }
                else if (i < 5)
                {
                    Console.WriteLine("You wrote a word too small. You lost an attempt");
                    Console.WriteLine("Try again!");
                }
                else
                {
                    Console.WriteLine("You wrote a word too small. You lost an attempt");
                    Console.WriteLine("You lost...");
                }
            }

            TimeSpan rests = DateTime.Now - now;
            Historial(rests, 0);

        }
        static void Historial(TimeSpan rest, int attempts)
        {
            if (attempts != -1)
            {

                string path = @"../../../files/historial.txt";
                bool existPath = File.Exists(path);
                Console.WriteLine("ha estat jugant " + rest);
                Console.WriteLine("i t'han quedat " + attempts + " intents");
                Console.WriteLine("escriu el teu nom");
                string nam = Console.ReadLine();
                string text = nam + "," + rest + "," + attempts + "\n";
                if (existPath)
                {
                    StreamReader sr = File.OpenText(@"../../../files/historial.txt");
                    text = sr.ReadToEnd();
                    text += nam + "," + rest + "," + attempts + "\n";
                    sr.Close();
                }
                Console.WriteLine(text);
                StreamWriter file;
                if (File.Exists(path))
                {
                    File.Delete(path);

                }
                file = new StreamWriter(path);
                file.Write(text);
            }
            else
            {
                StreamReader sr = File.OpenText(@"../../../files/historial.txt");
                string text = sr.ReadToEnd();
                sr.Close();
                Console.Write(text);
            }
        }
        static void Main()
        {
            var menu = new Wordle();

            menu.Game();
        }
        static string LoadWord(string path)
        {
            bool existPath = File.Exists(path);
            Console.WriteLine(path);
            Console.WriteLine(Directory.GetCurrentDirectory());
            if (existPath)
            {
                StreamReader sr = File.OpenText(path);
                string text = sr.ReadToEnd();
                sr.Close();

                string[] words = text.Split("\n");
                Random rnd = new Random();
                int num = rnd.Next(0, words.Length);
                string word = words[num];
                return word;
            }
            else return "empty";
        }
    }
}