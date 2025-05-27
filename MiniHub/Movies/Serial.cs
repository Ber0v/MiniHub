using System.Text.RegularExpressions;

namespace Movies
{
    internal class Serial
    {
        public void CheckSerial()
        {
            while (true)
            {
                string prompt = @"Choose which series you are watching:";
                string path = "D:\\Movies\\Serials\\";
                List<string> names = Directory.GetDirectories(path)
                    .Select(d => string.Join(" ", new DirectoryInfo(d).Name.Split(' ', '.', ',').Take(3)))
                    .ToList();
                names.Add("Exit");

                Menu mineMenu = new Menu(prompt, names.ToArray());
                int selectedIndex = mineMenu.Run();
                if (names[selectedIndex] == "Exit")
                {
                    Program program = new Program();
                    program.RunMainMenu();
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
                episodeNames.Add("Exit");

                string[] uniqueEpisodeNames = episodeNames.Distinct().ToArray();
                Menu episodeMenu = new Menu(prompt, uniqueEpisodeNames);
                int selectedEpisodeIndex = episodeMenu.Run();

                if (uniqueEpisodeNames[selectedEpisodeIndex] == "Exit")
                {
                    break;
                }
                Episode episode = new Episode();
                episode.MyEpisode(episodes, selectedEpisodeIndex, regex);
            }
        }
    }
}