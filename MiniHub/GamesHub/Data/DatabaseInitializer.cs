namespace GameHub.Data
{
    using System;
    using System.Data.SQLite;
    using System.IO;
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase()
        {
            string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            string dbPath = Path.Combine(dataFolder, "gamehub.db");

            // Създаване на папката ако я няма
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);

            // Създаване на базата, ако я няма
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);

                using var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;");
                connection.Open();

                var cmd = connection.CreateCommand();

                // Таблица за играчи
                cmd.CommandText = @"
                    CREATE TABLE Players (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        GameName TEXT NOT NULL,
                        Score INTEGER NOT NULL
                    );";
                cmd.ExecuteNonQuery();

                // Таблица за думи
                cmd.CommandText = @"
                    CREATE TABLE Words (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Word TEXT NOT NULL UNIQUE
                    );";
                cmd.ExecuteNonQuery();

                // Примерни начални думи за "Бесеница"
                cmd.CommandText = @"
                    INSERT INTO Words (Word) VALUES 
                    ('ябълка'),
                    ('компютър'),
                    ('прозорец');";
                cmd.ExecuteNonQuery();
            }
        }
    }
}
