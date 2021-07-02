using System;

namespace Match3
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
