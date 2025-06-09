using System.Data.SQLite;

namespace GamesHub.HangmanGame
{
    internal class HangmanRepository
    {
        private readonly string connectionString = "Data Source=hangman.db;Version=3;";

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

        public void AddWord(string word)
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            var command = new SQLiteCommand("INSERT INTO WordsBG (Word) VALUES (@word)", connection);
            command.Parameters.AddWithValue("@word", word);
            command.ExecuteNonQuery();
        }

        public string GetRandomWord()
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            var command = new SQLiteCommand("SELECT Word FROM WordsBG ORDER BY RANDOM() LIMIT 1", connection);
            return command.ExecuteScalar()?.ToString();
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
        }

    }
}