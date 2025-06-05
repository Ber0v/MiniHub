using System.Text;

namespace GamesHub
{
    public static class ConsoleConfigurator
    {
        public static void Configure(string title)
        {
            Console.Title = title;
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
        }
    }
}
