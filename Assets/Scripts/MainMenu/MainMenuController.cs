using System;

namespace MainMenu
{
    public class MainMenuController
    {
        private static MainMenuController _instance;
        public static MainMenuController getInstance()
        {
            return _instance ??= new MainMenuController();
        }
        public MainMenuController()
        {
            
        }

        public void PlayGame(object sender, EventArgs eventArgs)
        {
            
        }
    }
}