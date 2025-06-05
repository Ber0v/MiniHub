using static System.Console;


namespace GamesHub
{
    using Core;
    using HangmanGame;

    public class GamesHub
    {
        public void RunMainMenu()
        {
            ConsoleConfigurator.Configure("MiniHub/GameHub");
            string prompt = @"   ___                                  _     
  / _ \__ _ _ __ ___   ___  /\  /\_   _| |__  
 / /_\/ _` | '_ ` _ \ / _ \/ /_/ / | | | '_ \ 
/ /_\\ (_| | | | | | |  __/ __  /| |_| | |_) |
\____/\__,_|_| |_| |_|\___\/ /_/  \__,_|_.__/ 
                                              
Welcome to the GameHub. What are we going to play? 
(for now the only game we have is Hangman)";
            string[] options = { "Hangman", "About", "Exit" };
            Menu mineMenu = new Menu(prompt, options);
            int slectedIndex = mineMenu.Run();
            switch (slectedIndex)
            {
                case 0:
                    GameHangmanInfo Hangman = new GameHangmanInfo();
                    Hangman.Game();
                    break;
                case 1:
                    DisplayAboutInfo();
                    break;
                case 2:
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
            Console.WriteLine(@"   ___                                  _     
  / _ \__ _ _ __ ___   ___  /\  /\_   _| |__  
 / /_\/ _` | '_ ` _ \ / _ \/ /_/ / | | | '_ \ 
/ /_\\ (_| | | | | | |  __/ __  /| |_| | |_) |
\____/\__,_|_| |_| |_|\___\/ /_/  \__,_|_.__/ 
         ");

            WriteLine("This Hub demo was created by Berov");
            WriteLine("This is just a demo. full Hub coming to console near you soon!");
            WriteLine("Press any key to return to the Game menu.");
            ReadKey(true);
            RunMainMenu();
        }
    }
}
