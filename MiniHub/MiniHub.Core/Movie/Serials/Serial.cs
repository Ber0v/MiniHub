using System.Text.RegularExpressions;

namespace MiniHub.Movie.Serials
{
    using Core;
    class Serial
    {
        public void CheckSerial()
        {
            ConsoleConfigurator.Configure("MiniHub/Movie/Serials");
            while (true)
            {
                string prompt = @"Choose which series you are watching:";
                string path = "D:\\1Berov\\Movies\\Serials\\";
                List<string> names = Directory.GetDirectories(path)
                    .Select(d => string.Join(" ", new DirectoryInfo(d).Name.Split(' ', '.', ',').Take(3)))
                    .ToList();
                names.Add("Go Back");

                Menu mineMenu = new Menu(prompt, names.ToArray());
                int selectedIndex = mineMenu.Run();
                if (names[selectedIndex] == "Go Back")
                {
                    Movie movie = new Movie();
                    movie.MovieLobby();
                }
                CheckEpisode(path, selectedIndex);
            }
        }

        private static void CheckEpisode(string path, int selectedIndex)
        {
            while (true)
            {
                string prompt = "Choose which episode you want to watch:";
                string selectedSeriesPath = Directory.GetDirectories(path)[selectedIndex];
                string[] episodes = Directory.GetFiles(selectedSeriesPath, "*.mkv");

                List<string> episodeNames = new List<string>();
                Regex regex = new Regex(@"S\d+E\d+", RegexOptions.IgnoreCase);
                foreach (string episodePath in episodes)
                {
                    Match match = regex.Match(episodePath);
                    if (match.Success)
                    {
                        string episodeName = match.Value;
                        episodeNames.Add(episodeName);
                    }
                }
                episodeNames.Add("Go Back");

                string[] uniqueEpisodeNames = episodeNames.Distinct().ToArray();
                Menu episodeMenu = new Menu(prompt, uniqueEpisodeNames);
                int selectedEpisodeIndex = episodeMenu.Run();

                if (uniqueEpisodeNames[selectedEpisodeIndex] == "Go Back")
                {
                    break;
                }
                Episode episode = new Episode();
                episode.MyEpisode(episodes, selectedEpisodeIndex, regex);
            }
        }
    }
}