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

            string createTableQuery = @"
        CREATE TABLE IF NOT EXISTS WordsBG (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Word TEXT NOT NULL UNIQUE
        );
    ";

            var command = new SQLiteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
        }
        public HangmanRepository()
        {
            EnsureDatabaseCreated();
        }

    }
}
