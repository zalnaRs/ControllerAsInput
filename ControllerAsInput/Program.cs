using SharpDX.XInput;
using System;
using System.Threading;
namespace ControllerAsInput
{
    class Program
    {
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
           
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Change Cursor Speed");
            Console.WriteLine("2) Change Scroll Speed");
            Console.WriteLine("3) Show License");
            Console.WriteLine("4) Exit");
            Console.Write("\r\nSelect an option: ");
            //  BatteryLevel();
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
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Thread.Sleep(3000);
            }
            
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
                Thread.Sleep(3000);
            }

        }
        private static void ShowLicense()
        {
            Console.WriteLine("Scroll speed: (Default 20000 | Lower is faster)");
            Console.ReadLine();
        }
    }
}
