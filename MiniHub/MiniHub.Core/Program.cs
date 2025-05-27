namespace MiniHub
{
    using static System.Console;
    internal class Program
    {
        static void Main()
        {
            ConsoleConfig.ConfigureConsole();
            Program program = new Program();
            program.RunMainMenu();
        }

        public void RunMainMenu()
        {
            string prompt = @"
        _       _   ___                      _                   _     
  /\/\ (_)_ __ (_) / __\___  _ __  ___  ___ | | ___  /\  /\_   _| |__  
 /    \| | '_ \| |/ /  / _ \| '_ \/ __|/ _ \| |/ _ \/ /_/ / | | | '_ \ 
/ /\/\ \ | | | | / /__| (_) | | | \__ \ (_) | |  __/ __  /| |_| | |_) |
\/    \/_|_| |_|_\____/\___/|_| |_|___/\___/|_|\___\/ /_/  \__,_|_.__/ 
                                    
Welcome to the MiniHub. What would you like to do? 
(Use the arrow keys to cycle through options and press enter to select an option.)
(for now the only game we have is Hangman and WAR)";
            string[] options = { "Serials", "Hangman", "Card Game", "About", "Exit" };
            Menu mineMenu = new Menu(prompt, options);
            int slectedIndex = mineMenu.Run();
            switch (slectedIndex)
            {
                case 0:
                    Serial serials = new Serial();
                    serials.CheckSerial();
                    break;
                case 1:

                    break;
                case 2:

                    break;
                case 3:
                    DisplayAboutInfo();
                    break;
                case 4:
                    ExitGame();
                    break;
            }
        }

        private void ExitGame()
        {
            Environment.Exit(0);
        }

        private void DisplayAboutInfo()
        {
            Console.Clear();
            Console.WriteLine(@"
        _       _   ___                      _                   _     
  /\/\ (_)_ __ (_) / __\___  _ __  ___  ___ | | ___  /\  /\_   _| |__  
 /    \| | '_ \| |/ /  / _ \| '_ \/ __|/ _ \| |/ _ \/ /_/ / | | | '_ \ 
/ /\/\ \ | | | | / /__| (_) | | | \__ \ (_) | |  __/ __  /| |_| | |_) |
\/    \/_|_| |_|_\____/\___/|_| |_|___/\___/|_|\___\/ /_/  \__,_|_.__/ 
                                    ");

            WriteLine("This Hub demo was created by Berov and Viktor.");
            WriteLine("This is just a demo. full Hub coming to console near you soon!");
            WriteLine("Press any key to return to the menu.");
            ReadKey(true);
            RunMainMenu();
        }
    }
}
