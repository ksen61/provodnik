using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provodnik
{
    public class Menu
    {
        public static string Show(string[] directoryElements, string currentPath)
        {
            Console.WriteLine("Проводик");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(120, 3);
            Console.WriteLine("F1 - Создать файл");
            Console.SetCursorPosition(120, 4);
            Console.WriteLine("F2 - Создать папку");
            Console.SetCursorPosition(120, 5);
            Console.WriteLine("F3 - Удалить");
            int pos = 2;

            foreach (string element in directoryElements)
            {
                Console.SetCursorPosition(0, pos);
                Console.WriteLine($"  {element}");
                Console.SetCursorPosition(50, pos);

                if (File.Exists(element))
                {
                    FileInfo fileInfo = new FileInfo(element);
                    Console.WriteLine($"|   {fileInfo.Extension} | Размер: {fileInfo.Length} байт | Создан: {fileInfo.CreationTime}");
                }
                else if (Directory.Exists(element))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(element);
                    Console.WriteLine($"|   Каталог | Создан: {directoryInfo.CreationTime}");
                }

                pos++;
            }

            var position = 2;

            while (true)
            {
                ConsoleKeyInfo userButton;
                Console.SetCursorPosition(0, position);
                Console.WriteLine("->");
                userButton = Console.ReadKey();
                Console.SetCursorPosition(0, position);
                Console.WriteLine("  ");

                if (userButton.Key == ConsoleKey.DownArrow && position != directoryElements.Length + 1)
                {
                    position++;
                }
                else if (userButton.Key == ConsoleKey.UpArrow && position != 2)
                {
                    position--;
                }
                else if (userButton.Key == ConsoleKey.Enter)
                {
                    if (Global.Count == 0)
                    {
                        Console.SetCursorPosition(2, position);
                        string newPath = directoryElements[position - 2];
                        Console.Clear();
                        Global.Count++;
                        Explorer.ShowFolder(newPath);
                        return "Назад";
                    }
                    else
                    {
                        Console.SetCursorPosition(2, position);
                        string newPath = directoryElements[position - 2];
                        Console.Clear();
                        Global.Count++;
                        return newPath;
                    }
                }
                else if (userButton.Key == ConsoleKey.Escape)
                {
                    if (Global.Count != 0)
                    {
                        Global.Count--;
                        Console.Clear();
                        return "Назад";
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
                else if (userButton.Key == ConsoleKey.F1)
                {
                    SaveDelete.SaveFile(currentPath);
                }
                else if (userButton.Key == ConsoleKey.F2)
                {
                    SaveDelete.SaveDirectory(currentPath);
                }
                else if (userButton.Key == ConsoleKey.F3)
                {
                    string newPath = directoryElements[position - 2];
                    SaveDelete.DeleteFileOrDirectory(newPath);
                }
            }
        }
    }

    public static class SaveDelete
    {
        public static void SaveFile(string path)
        {
            if (Global.Count != 0)
            {
                Console.SetCursorPosition(120, 6);
                string fileName = Console.ReadLine();
                try
                {
                    File.Create(Path.Combine(path, fileName));
                }
                catch { }
            }
        }

        public static void SaveDirectory(string path)
        {
            if (Global.Count != 0)
            {
                Console.SetCursorPosition(120, 6);
                string directoryName = Console.ReadLine();
                try
                {
                    Directory.CreateDirectory(Path.Combine(path, directoryName));
                }
                catch { }
            }
        }

        public static void DeleteFileOrDirectory(string pathToDelete)
        {
            try
            {
                File.Delete(pathToDelete);
            }
            catch { }
            try
            {
                Directory.Delete(pathToDelete, true);
            }
            catch { }
        }
    }
}