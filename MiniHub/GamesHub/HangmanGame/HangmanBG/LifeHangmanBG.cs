namespace GamesHub.HangmanGame
{
    internal class LifeHangmanBG
    {
        public void Life(int life, Queue<char> noLetters, int time, string playerName, int playerScore)
        {
            if (time == 0 || life == 1)
            {
                switch (life)
                {
                    case 8:
                        Console.SetCursorPosition(0, 7);
                        for (int i = 0; i < 9; i++)
                        {
                            Thread.Sleep(50);
                            Console.Write("=");
                        }
                        DrawBox();
                        Console.SetCursorPosition(17, 3);
                        foreach (var ch in "Сгрешени букви:")
                        {
                            Thread.Sleep(50);
                            Console.Write(ch);
                        }

                        ShowPlayerScore(playerName, playerScore);
                        break;

                    case 7:
                        Console.SetCursorPosition(0, 6); Console.WriteLine("       |"); Thread.Sleep(50);
                        Console.SetCursorPosition(0, 5); Console.WriteLine("       |"); Thread.Sleep(50);
                        Console.SetCursorPosition(0, 4); Console.WriteLine("       |"); Thread.Sleep(50);
                        Console.SetCursorPosition(0, 3); Console.WriteLine("       |"); Thread.Sleep(50);
                        Console.SetCursorPosition(0, 2); Console.WriteLine("  +----+"); break;

                    case 6: Console.SetCursorPosition(2, 3); Console.WriteLine("@"); break;
                    case 5: Console.SetCursorPosition(2, 4); Console.WriteLine("|"); break;
                    case 4: Console.SetCursorPosition(1, 4); Console.WriteLine("/"); break;
                    case 3: Console.SetCursorPosition(3, 4); Console.WriteLine("\\"); break;
                    case 2: Console.SetCursorPosition(1, 5); Console.WriteLine("/"); break;
                    case 1: Console.SetCursorPosition(3, 5); Console.WriteLine("\\"); break;
                }
            }

            Console.SetCursorPosition(33, 3);
            Console.WriteLine(string.Join(", ", noLetters));
            Console.SetCursorPosition(0, 9);
        }

        private void DrawBox()
        {
            Console.SetCursorPosition(15, 2);
            Console.WriteLine("----------------------------------------");
            for (int i = 3; i < 7; i++)
            {
                Console.SetCursorPosition(15, i); Console.Write("|");
                Console.SetCursorPosition(54, i); Console.Write("|");
                Thread.Sleep(50);
            }
            Console.SetCursorPosition(15, 7);
            Console.WriteLine("----------------------------------------");
        }

        private void ShowPlayerScore(string playerName, int playerScore)
        {
            Console.SetCursorPosition(17, 5);
            string label = $"Точки на {playerName}:";
            foreach (var ch in label)
            {
                Thread.Sleep(50);
                Console.Write(ch);
            }

            Console.SetCursorPosition(28 + playerName.Length, 5);
            Console.WriteLine(playerScore);
        }
    }
}
