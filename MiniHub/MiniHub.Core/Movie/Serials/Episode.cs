using System.Diagnostics;
using System.Text.RegularExpressions;

namespace MiniHub.Movie.Serials
{
    using Core;
    internal class Episode
    {
        public void MyEpisode(string[] episodes, int selectedEpisodeIndex, Regex regex)
        {
            Match match = regex.Match(episodes[selectedEpisodeIndex]);
            string episodeName = match.Value;
            ConsoleConfigurator.Configure(episodeName);
            while (true)
            {
                string prompt = @$"   >>{episodeName}<<
what would it be:";
                string[] options = { "Start Episode", "Delete Episode", "Go Back" };
                Menu mineMenu = new Menu(prompt, options);
                int slectedIndex = mineMenu.Run();
                switch (slectedIndex)
                {
                    case 0:
                        StartEpisode(episodes, selectedEpisodeIndex);
                        break;
                    case 1:
                        DeleteEpisode(episodes[selectedEpisodeIndex]);
                        return;
                    case 2:
                        Movie movie = new Movie();
                        movie.MovieLobby();
                        return;
                }
            }
        }

        private static void StartEpisode(string[] episodes, int selectedEpisodeIndex)
        {
            string selectedEpisodePath = episodes[selectedEpisodeIndex];
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process { StartInfo = startInfo };
            process.Start();

            using (StreamWriter writer = process.StandardInput)
            {
                if (writer.BaseStream.CanWrite)
                {
                    writer.WriteLine($"start {selectedEpisodePath}");
                }
            }
        }

        private static void DeleteEpisode(string episodePath)
        {
            try
            {
                File.Delete(episodePath);
                Console.WriteLine("File deleted successfully.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file: {ex.Message}");
                Console.ReadKey();
            }
            return;
        }
    }
}
