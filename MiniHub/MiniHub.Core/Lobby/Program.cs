using static System.Console;

namespace MiniHub.Lobby
{
    using Core;
    using Movie;
    internal class Program
    {
        static void Main()
        {
            Program program = new Program();
            program.RunLobby();
        }

        public void RunLobby()
        {
            ConsoleConfigurator.Configure("MiniHub");
            string prompt = @"
        _       _              _     
  /\/\ (_)_ __ (_) /\  /\_   _| |__  
 /    \| | '_ \| |/ /_/ / | | | '_ \ 
/ /\/\ \ | | | | / __  /| |_| | |_) |
\/    \/_|_| |_|_\/ /_/  \__,_|_.__/ 
                                          
Welcome to the MiniHub. What would you like to do? 
(Use the arrow keys to cycle through options and press enter to select an option.)";
            string[] options = { "Movies", "Games", "About", "Exit" };
            Menu mineMenu = new Menu(prompt, options);
            int slectedIndex = mineMenu.Run();
            switch (slectedIndex)
            {
                case 0:
                    Movie movie = new Movie();
                    movie.MovieLobby();
                    break;
                case 1:

                    break;
                case 2:
                    DisplayAboutInfo();
                    break;
                case 3:
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
            Clear();
            WriteLine(@"
        _       _              _     
  /\/\ (_)_ __ (_) /\  /\_   _| |__  
 /    \| | '_ \| |/ /_/ / | | | '_ \ 
/ /\/\ \ | | | | / __  /| |_| | |_) |
\/    \/_|_| |_|_\/ /_/  \__,_|_.__/ 
");

            WriteLine("This Hub demo was created by Berov.");
            WriteLine("This is just a demo. full Hub coming to console near you soon!");
            WriteLine("Press any key to return to the menu.");
            ReadKey(true);
            RunLobby();
        }

        internal void RunMainMenu()
        {
            throw new NotImplementedException();
        }
    }
}
