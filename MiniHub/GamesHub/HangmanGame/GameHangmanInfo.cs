namespace GamesHub.HangmanGame
{
    using Core;
    internal class GameHangmanInfo
    {
        public void Game()
        {
            ConsoleConfigurator.Configure("GameHub/Hangman");
            string prompt = @"

  /\  /\__ _ _ __   __ _ _ __ ___   __ _ _ __  
 / /_/ / _` | '_ \ / _` | '_ ` _ \ / _` | '_ \ 
/ __  / (_| | | | | (_| | | | | | | (_| | | | |
\/ /_/ \__,_|_| |_|\__, |_| |_| |_|\__,_|_| |_|
                   |___/                       
Welcome to the Hangman Game!";
            string[] options = { "Play", "players Score", "Settings", "How to play", "Go to menu" };

            Menu mineMenu = new Menu(prompt, options);
            int slectedIndex = mineMenu.Run();

            switch (slectedIndex)
            {
                case 0:
                    HangmanBG bG = new HangmanBG();
                    bG.StartBG();
                    break;
                case 1:
                    PlayersScore();
                    break;
                case 2:
                    Settings();
                    break;
                case 3:
                    Info();
                    break;
                case 4:
                    GotoMenu();
                    break;
            }

        }

        public void PlayersScore()
        {
            ConsoleConfigurator.Configure("GameHub/Hangman/Score");
            var repo = new HangmanRepository();
            var players = repo.GetAllPlayers();

            Console.Clear();
            Console.WriteLine("Press any key to go back.\n");

            foreach (var player in players)
            {
                Console.WriteLine($"<< {player.Name} - {player.Score} >>");
            }

            Console.ReadKey(true);
            Game();
        }

        public void Settings()
        {
            ConsoleConfigurator.Configure("GameHub/Hangman/Settings");
            string prompt = @"

  /\  /\__ _ _ __   __ _ _ __ ___   __ _ _ __  
 / /_/ / _` | '_ \ / _` | '_ ` _ \ / _` | '_ \ 
/ __  / (_| | | | | (_| | | | | | | (_| | | | |
\/ /_/ \__,_|_| |_|\__, |_| |_| |_|\__,_|_| |_|
                   |___/                       
choose a language:";
            string[] options = { "More Words", "Go Back" };
            Menu mineMenu = new Menu(prompt, options);
            int slectedIndex = mineMenu.Run();

            switch (slectedIndex)
            {
                case 0:
                    BGWords();
                    break;
                case 1:
                    Game();
                    break;
            }
        }

        public void BGWords()
        {
            string word = null;
            var repo = new HangmanRepository();

            while (word != "край")
            {
                Console.Write("Въведи дума: ");
                word = Console.ReadLine().ToLower();

                if (word != "край")
                {
                    if (repo.AddWord(word))
                    {
                        Console.WriteLine("Думата е добавена в базата.");
                    }
                    else
                    {
                        Console.WriteLine("Думата вече съществува.");
                    }
                }
            }

            Console.WriteLine("Натисни произволен клавиш за връщане в менюто.");
            Console.ReadKey(true);
            Game();
        }


        public void Info()
        {
            ConsoleConfigurator.Configure("GameHub/Hangman/Info");

            Console.Clear();
            Console.WriteLine(@"

  /\  /\__ _ _ __   __ _ _ __ ___   __ _ _ __  
 / /_/ / _` | '_ \ / _` | '_ ` _ \ / _` | '_ \ 
/ __  / (_| | | | | (_| | | | | | | (_| | | | |
\/ /_/ \__,_|_| |_|\__, |_| |_| |_|\__,_|_| |_|
                   |___/                       ");

            Console.WriteLine("In this game you need to figure out the missing letters of the word...");
            Console.WriteLine("but you only have a limitted guesses ");
            Console.WriteLine("so be caresufull!🤗");

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey(true);
            Game();
        }

        private void GotoMenu()
        {
            GamesHub lobby = new GamesHub();
            lobby.RunMainMenu();
        }
    }
}
