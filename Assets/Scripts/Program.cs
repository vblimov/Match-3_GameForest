using System;

namespace Match_3_GameForest
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using var mainMenu = new Match3Game();
            mainMenu.Run();
        }
    }
}
