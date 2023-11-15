using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Provodnik
{
    public static class Global
    {
        public static int Count = 0;
    }

    public class Explorer
    {
        public static void Main()
        {
            DriveInfo[] allDrivers = DriveInfo.GetDrives();
            string[] Disks = allDrivers.Select(driver => driver.Name).ToArray();
            Menu.Show(Disks, "");
            return;
        }

        public static void ShowFolder(string path)
        {
            while (true)
            {
                if (File.Exists(path))
                {
                    Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
                    Global.Count--;
                    return;
                }
                else
                {
                    string[] folders = Directory.GetDirectories(path);
                    string[] files = Directory.GetFiles(path);
                    string[] elements = folders.Concat(files).ToArray();
                    var newPath = Menu.Show(elements, path);

                    if (newPath == "Назад")
                    {
                        Console.Clear();
                        if (Global.Count == 0)
                        {
                            Main();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            return;
                        }
                    }

                    ShowFolder(newPath);
                }
            }
        }
    }
}