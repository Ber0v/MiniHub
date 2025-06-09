
namespace GamesHub.HangmanGame
{
    using Core;
    internal class HangmanBG
    {
        class WordGenerator
        {
            private HangmanRepository repo = new HangmanRepository();

            public string GetRandomWord()
            {
                return repo.GetRandomWord();
            }
        }


        public void StartBG()
        {
            Console.Clear();
            Console.WriteLine(@"

  /\  /\__ _ _ __   __ _ _ __ ___   __ _ _ __  
 / /_/ / _` | '_ \ / _` | '_ ` _ \ / _` | '_ \ 
/ __  / (_| | | | | (_| | | | | | | (_| | | | |
\/ /_/ \__,_|_| |_|\__, |_| |_| |_|\__,_|_| |_|
                   |___/                       
Write your name below:");
            var repo = new HangmanRepository();
            var players = repo.GetAllPlayers();
            var options = players.Select(p => p.Name).ToList();
            options.Add("Нов играч");
            Menu playerMenu = new Menu("Избери име", options.ToArray());
            int choice = playerMenu.Run();

            string playerName;
            int playerScore;

            if (choice == players.Count)
            {
                Console.Write("Въведете име: ");
                playerName = Console.ReadLine();
                var existing = repo.GetPlayer(playerName);
                if (existing == null)
                {
                    repo.AddPlayer(playerName);
                    playerScore = 0;
                }
                else
                {
                    playerScore = existing.Score;
                }
            }
            else
            {
                var player = players[choice];
                playerName = player.Name;
                playerScore = player.Score;
            }
            bool playAgain = true;

            while (playAgain)
            {
                Console.Clear();
                int health = 8;
                Queue<char> noLetters = new Queue<char>();
                List<char> guessedLetters = new List<char>();
                bool letterGuessed = true;
                int displayRefresh = 0;

                WordGenerator generator = new WordGenerator();
                string word = generator.GetRandomWord();

                while (health > 1 && letterGuessed)
                {
                    LifeHangmanBG life = new LifeHangmanBG();
                    life.Life(health, noLetters, displayRefresh, playerName, playerScore);

                    if (displayRefresh == 0)
                    {
                        for (int i = 0; i < word.Length; i++)
                        {
                            Console.SetCursorPosition(i * 2 + 1, 9);
                            Console.Write(i == 0 || i == word.Length - 1 || guessedLetters.Contains(word[i]) ? word[i] : '_');
                        }
                        displayRefresh++;
                    }

                    Console.SetCursorPosition(0, 11);
                    Console.Write("Въведи буква: ");
                    char input;
                    do
                    {
                        Console.SetCursorPosition(14, 11);
                        var keyInfo = Console.ReadKey();
                        if (keyInfo.Key == ConsoleKey.Escape)
                        {
                            Console.Clear();
                            GameHangmanInfo Hangman = new GameHangmanInfo();
                            Hangman.Game();
                        }
                        input = keyInfo.KeyChar;
                    } while (!char.IsLetter(input));

                    Loading();

                    if (word.Contains(input))
                    {
                        guessedLetters.Add(input);
                        displayRefresh--;
                    }
                    else if (!noLetters.Contains(input))
                    {
                        health--;
                        noLetters.Enqueue(input);
                        if (displayRefresh > 0) displayRefresh--;
                    }

                    if (word.Skip(1).Take(word.Length - 2).All(letter => guessedLetters.Contains(letter)))
                    {
                        playerScore++;
                        repo.UpdatePlayerScore(playerName, playerScore);
                        letterGuessed = false;
                        Console.Clear();
                        Console.WriteLine("\nБраво, познахте думата: " + word);
                        break;
                    }
                }

                if (letterGuessed)
                {
                    LifeHangmanBG life = new LifeHangmanBG();
                    life.Life(health, noLetters, displayRefresh, playerName, playerScore);
                    Console.WriteLine("\nТи не позна думата: " + word);
                    playerScore = 0;
                    repo.UpdatePlayerScore(playerName, playerScore);
                }

                playAgain = AskToPlayAgain();
            }
        }

        private static bool AskToPlayAgain()
        {
            Console.WriteLine("Натиснете произволен клавиш, за да продължите.");
            Console.ReadKey(true);

            string prompt = @"

  /\  /\__ _ _ __   __ _ _ __ ___   __ _ _ __  
 / /_/ / _` | '_ \ / _` | '_ ` _ \ / _` | '_ \ 
/ __  / (_| | | | | (_| | | | | | | (_| | | | |
\/ /_/ \__,_|_| |_|\__, |_| |_| |_|\__,_|_| |_|
                   |___/                       
Искате ли да играете отново?";
            string[] options = { "Да", "Не" };
            Menu mineMenu = new Menu(prompt, options);
            int selectedIndex = mineMenu.Run();

            return selectedIndex == 0;
        }

        private static void Loading()
        {
            Console.WriteLine();
            Console.Write("Loading");
            Thread.Sleep(100);
            for (int i = 0; i < 2; i++)
            {
                Console.Write(".");
                Thread.Sleep(100);
                Console.Write(".");
                Thread.Sleep(100);
                Console.Write(".");
                if (i != 1)
                {
                    Thread.Sleep(200);
                    Console.Write("\b \b\b \b\b \b");
                }
            }
            Thread.Sleep(200);
            Console.Write("\b \b\b \b\b \b\b \b\b \b\b \b\b \b\b \b\b \b\b \b");
            Console.SetCursorPosition(15, 11);
            Console.Write("\b \b");
        }
    }
}
