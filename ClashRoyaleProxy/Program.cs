using System;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.IO;

namespace ClashRoyaleProxy
{
    internal class Program
    {
        /// <summary>
        /// Initializes the main console.
        /// I wanted this garbage extracted from Main().
        /// </summary>
        static void InitializeConsole()
        {
            // Console handle if needed for next versions
            IntPtr hConsole = Process.GetCurrentProcess().Handle;

            // Check whether the proxy runs more than once
            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Length > 1)
            {
                Logger.Log("You seem to run this proxy more than once.", LogType.WARNING);
                Logger.Log("Aborting..", LogType.WARNING);
                Thread.Sleep(5500);
                Environment.Exit(-1);
            }

            // UI stuff
            Console.Title = "Clash Royale Proxy | " + Helper.AssemblyVersion + " | © " + DateTime.UtcNow.Year;
            Console.SetCursorPosition(0, 0);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Gray;

            /* Point CenteredConsole = new Point((Screen.PrimaryScreen.WorkingArea.Width - Console.WindowWidth) / 2, 
                                                 (Screen.PrimaryScreen.WorkingArea.Height - Console.WindowHeight) / 2);
               Console.SetWindowPosition(CenteredConsole.X, CenteredConsole.Y) */
        }

        static void Main(string[] args)
        {
            InitializeConsole();
            Proxy.Start();
        }
    }
}