using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ConsoleApp4
{
    class Program
    {
        private static Random random = new Random();
        static void Main(string[] args)
        {
        start:
            Console.ForegroundColor = ConsoleColor.White;
            int selection = 0;
            Console.WriteLine($"1- Write random names \n" +
                $"2- Read names from file and write to object\n" +
                $"3- Clear file");
            Console.WriteLine("Please select option:");

            try
            {
                selection = int.Parse(Console.ReadLine());
            }
            catch
            {
                goto start;
            }

            switch (selection)
            {
                case 1:
                    WriteRandomNames();
                    goto start;
                case 2:
                    ReadNamesFromTextFile();
                    goto start;
                case 3:
                    ClearFile();
                    goto start;
                default:
                    Console.WriteLine("Invalid option");
                    goto start;
            }
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        static void ClearFile()
        {
            using (StreamWriter sw = new StreamWriter("persons.txt", false))
            {
                sw.Write("");
            }
        }

        static void ReadNamesFromTextFile()
        {
            var list = new List<Person>();
            if (File.Exists("persons.txt"))
                using (StreamReader sr = new StreamReader("persons.txt", true))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        list.Add(JsonSerializer.Deserialize<Person>(line));
                    }
                }

            foreach (Person item in list)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{item.Name} {item.Surname}");
            }
        }

        static void WriteRandomNames()
        {
            for (int i = 0; i < 25; i++)
            {
                var person = new Person
                {
                    Name = RandomString(5),
                    Surname = RandomString(6)
                };

                using StreamWriter file = new StreamWriter("persons.txt", append: true);
                file.WriteLine(JsonSerializer.Serialize<Person>(person));
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
