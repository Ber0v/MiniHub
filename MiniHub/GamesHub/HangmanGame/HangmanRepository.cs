using System.Data.SQLite;

namespace GamesHub.HangmanGame
{
    internal class HangmanRepository
    {
        private readonly string connectionString = "Data Source=hangman.db;Version=3;";
        private static readonly string[] defaultWords =
        {
    "ябълка",
    "компютър",
    "игра",
    "залез",
    "природа",
    "дърво",
    "вятър",
    "река",
    "планина",
    "море",
    "слънце",
    "звезда",
    "луна",
    "цвете",
    "гора",
    "лист",
    "небе",
    "птица",
    "риба",
    "животно",
    "град",
    "село",
    "улица",
    "къща",
    "стена",
    "прозорец",
    "врата",
    "стол",
    "маса",
    "книга",
    "тетрадка",
    "писалка",
    "молив",
    "училище",
    "ученик",
    "учител",
    "часовник",
    "чанта",
    "хляб",
    "вода",
    "мляко",
    "кафе",
    "чай",
    "захар",
    "сол",
    "чиния",
    "вилица",
    "нож",
    "лъжица",
    "печка",
    "хладилник",
    "телевизор",
    "радио",
    "телефон",
    "картина",
    "огледало",
    "дреха",
    "обувка",
    "ръка",
    "крак",
    "глава",
    "очи",
    "нос",
    "уста",
    "коса",
    "зъб",
    "ухо",
    "сърце",
    "мозък",
    "кръв",
    "стомах",
    "ръкавица",
    "шапка",
    "панталон",
    "риза",
    "яке",
    "поле",
    "гора",
    "песен",
    "филм",
    "музика",
    "звук",
    "език",
    "дума",
    "изречение",
    "мисъл",
    "чувство",
    "мечта",
    "въпрос",
    "отговор",
    "истина",
    "лъжа",
    "приятел",
    "враг",
    "играчка",
    "подарък",
    "радост",
    "тъга",
    "страх",
    "смях",
    "плач",
    "усмивка"
};

        public List<string> GetAllWords()
        {
            var words = new List<string>();

            using var connection = new SQLiteConnection(connectionString);
            connection.Open();
            var command = new SQLiteCommand("SELECT Word FROM WordsBG", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                words.Add(reader.GetString(0));
            }

            return words;
        }

        public bool AddWord(string word)
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            var command = new SQLiteCommand("INSERT OR IGNORE INTO WordsBG (Word) VALUES (@word)", connection);
            command.Parameters.AddWithValue("@word", word.ToLower());
            return command.ExecuteNonQuery() > 0;
        }

        public string GetRandomWord()
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            var command = new SQLiteCommand("SELECT Word FROM WordsBG ORDER BY RANDOM() LIMIT 1", connection);
            return command.ExecuteScalar()?.ToString();
        }
        private void SeedDefaultWords()
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            var countCommand = new SQLiteCommand("SELECT COUNT(*) FROM WordsBG", connection);
            long count = (long)countCommand.ExecuteScalar();

            if (count == 0)
            {
                foreach (var word in defaultWords)
                {
                    var insert = new SQLiteCommand("INSERT OR IGNORE INTO WordsBG (Word) VALUES (@word)", connection);
                    insert.Parameters.AddWithValue("@word", word.ToLower());
                    insert.ExecuteNonQuery();
                }
            }
        }
        public void EnsureDatabaseCreated()
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            string createWords = @"
        CREATE TABLE IF NOT EXISTS WordsBG (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Word TEXT NOT NULL UNIQUE
        );";

            string createPlayers = @"
        CREATE TABLE IF NOT EXISTS Players (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL UNIQUE,
            Score INTEGER NOT NULL
        );";

            var command = new SQLiteCommand(createWords, connection);
            command.ExecuteNonQuery();
            command.CommandText = createPlayers;
            command.ExecuteNonQuery();
        }

        public List<Player> GetAllPlayers()
        {
            var players = new List<Player>();

            using var connection = new SQLiteConnection(connectionString);
            connection.Open();
            var command = new SQLiteCommand("SELECT Name, Score FROM Players", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                players.Add(new Player
                {
                    Name = reader.GetString(0),
                    Score = reader.GetInt32(1)
                });
            }

            return players;
        }

        public Player? GetPlayer(string name)
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            var command = new SQLiteCommand("SELECT Name, Score FROM Players WHERE Name = @name", connection);
            command.Parameters.AddWithValue("@name", name);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Player
                {
                    Name = reader.GetString(0),
                    Score = reader.GetInt32(1)
                };
            }

            return null;
        }

        public void AddPlayer(string name)
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            var command = new SQLiteCommand("INSERT INTO Players (Name, Score) VALUES (@name, 0)", connection);
            command.Parameters.AddWithValue("@name", name);
            command.ExecuteNonQuery();
        }

        public void UpdatePlayerScore(string name, int score)
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            var command = new SQLiteCommand("UPDATE Players SET Score = @score WHERE Name = @name", connection);
            command.Parameters.AddWithValue("@score", score);
            command.Parameters.AddWithValue("@name", name);
            command.ExecuteNonQuery();
        }
        public HangmanRepository()
        {
            EnsureDatabaseCreated();
            SeedDefaultWords();
        }

    }
}