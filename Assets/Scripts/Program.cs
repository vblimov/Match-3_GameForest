using System;
using MainMenu;

namespace Match_3_GameForest
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using var mainMenu = new MainMenuViewer();
            mainMenu.Run();
        }
    }
}
