using SharpDX.XInput;
using System;
using System.Threading;
namespace ControllerAsInput
{


    class Program
    {
        static string LocalVersion = "1.1";
        static string Version = "Unkonow";
        static bool showMenu = false;
        static void Main(string[] args)
        {
            try {
                var inputMonitor = new ControllerAsInput();
                inputMonitor.Start();
            } catch {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.ReadLine();
            }
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ControllerAsInput has started!");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            showMenu = true;
            System.Net.WebClient wc = new System.Net.WebClient();
            Version = wc.DownloadString("https://pastebin.com/raw/Ygbw2tdu");
            while (showMenu)
            {
                showMenu = MainMenu();
            }

        }

       /* private static System.Timers.Timer BatteryLevel()
        {
            System.Timers.Timer tmr = new System.Timers.Timer();
            tmr.Elapsed += (sender, args) => BatteryLevel();
            tmr.AutoReset = true;
            tmr.Interval = 999;
            tmr.Start();
            if (SharpDX.XInput.BatteryLevel == SharpDX.XInput.BatteryLevel.Full)
            {

            }
            Console.WriteLine("");
            Console.WriteLine();

            return tmr;
        }*/

        private static bool MainMenu()
        {
            var IfNewVersion = false;
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Change Cursor Speed");
            Console.WriteLine("2) Change Scroll Speed");
            Console.WriteLine("3) Show License");
            Console.WriteLine("4) Exit");
            if (!Version.Equals(LocalVersion))
            {
                IfNewVersion = true;
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("5) Update to the new version!");
                Console.WriteLine("Version: " + LocalVersion + "New version: "+ Version);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            } else
            {
                Console.WriteLine("Version: " + LocalVersion);
            }

            
            Console.Write("\r\nSelect an option: ");
            //  BatteryLevel();
            if (IfNewVersion)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        ChangeCursorSpeed();
                        return true;
                    case "2":
                        ChangeScrollSpeed();
                        return true;
                    case "3":
                        ShowLicense();
                        return true;
                    case "4":
                        System.Diagnostics.Process[] runingProcess = System.Diagnostics.Process.GetProcesses();
                        int i;
                        for (i = 0; i < runingProcess.Length; i++)
                        {
                            if (runingProcess[i].ProcessName == "osk")
                            {
                                runingProcess[i].Kill();
                            }

                        }
                        Environment.Exit(0);
                        return false;
                    case "5":
                        System.Diagnostics.Process.Start("https://github.com/zalnaRs/ControllerAsInput/releases");
                        Thread.Sleep(25000);
                        Environment.Exit(0);
                        return false;
                    default:
                        return true;
                }
            } else
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        ChangeCursorSpeed();
                        return true;
                    case "2":
                        ChangeScrollSpeed();
                        return true;
                    case "3":
                        ShowLicense();
                        return true;
                    case "4":
                        Environment.Exit(0);
                        return false;

                    default:
                        return true;
                }
            }
            

        }

        private static void ChangeCursorSpeed()
        {
            Console.WriteLine("Mouse speed: (Default 1000 | Lower is faster)");
            try
            {
                int MouseSpeed = int.Parse(Console.ReadLine());
                    ControllerAsInput.MovementDivider = MouseSpeed;
            } catch
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Invalid value!");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("\n" + "Press any key to go back...");
            Console.ReadLine();

        }
        private static void ChangeScrollSpeed()
        {
            Console.WriteLine("Scroll speed: (Default 20000 | Lower is faster)");
            try
            {
                int ScrollSpeed = int.Parse(Console.ReadLine());
                ControllerAsInput.ScrollDivider = ScrollSpeed;
            }
            catch
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Invalid value!");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("\n" + "Press any key to go back...");
            Console.ReadLine();
        }
        private static void ShowLicense(){
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"LICENSE");
                foreach (string line in lines)
                {
                    Console.WriteLine("\t" + line);
                }
            } catch
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Someting went wrong!");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        
            Console.WriteLine("\n" + "Press any key to go back...");
            Console.ReadLine();
         }
    }
}
