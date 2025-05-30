using MiniHub.Lobby;
using MiniHub.Movie.Serials;

namespace MiniHub.Movie
{
    using Core;
    class Movie
    {
        public void MovieLobby()
        {
            ConsoleConfigurator.Configure("MiniHub/Movie");
            string prompt = @"
        _       _              _     
  /\/\ (_)_ __ (_) /\  /\_   _| |__  
 /    \| | '_ \| |/ /_/ / | | | '_ \ 
/ /\/\ \ | | | | / __  /| |_| | |_) |
\/    \/_|_| |_|_\/ /_/  \__,_|_.__/ 
                                          
Welcome to the MiniHub. What would you like to do? 
(Use the arrow keys to cycle through options and press enter to select an option.)";
            string[] options = { "Films", "Serials", "Go Back" };
            Menu mineMenu = new Menu(prompt, options);
            int slectedIndex = mineMenu.Run();
            switch (slectedIndex)
            {
                case 0:
                    break;
                case 1:
                    Serial serials = new Serial();
                    serials.CheckSerial();
                    break;
                case 2:
                    Program program = new Program();
                    program.RunLobby();
                    break;
            }
        }
    }
}
