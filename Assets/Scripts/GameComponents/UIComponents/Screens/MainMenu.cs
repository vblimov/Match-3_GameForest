using System;
using System.Collections.Generic;
using Match3.Enums;
using Match3.GameParams;
using Match3.GameComponents.UIComponents.Screens;
using Match3.GameComponents.UIComponents.ScreenComponents;
using Match3.GameComponents.UIComponents.Touchable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Match3.Resources;
using Match3.Utility;

namespace Match3.GameComponents.UIComponents.Screens
{
    public class MainMenu : Screen
    {
        #region Fields
        
        private SpriteBatch _spriteBatch;
        private List<TouchableComponent> _buttons;

        #endregion
        
        #region Methods

        public MainMenu(ScreenManager screenManager)
        {
            ScreenManager = screenManager;
        }

        public override void Load()
        {
            var playButton = new Button(
                ResourcesLoader.Button,
                ResourcesLoader.Font,
                GameSettings._positions.defaultButtonPosition,
                GameSettings._names.playButtonText);
            _buttons = new List<TouchableComponent>()
            {
                playButton
            };
            playButton.Click += PlayGame;
            base.Load();
        }

        private void PlayGame(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new GamePlay(ScreenManager));
            ExitScreen();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch = ScreenManager.SpriteBatch;
            Console.Write(_spriteBatch);
            _spriteBatch.Begin();
            _buttons.ForEach(button => button.Draw(gameTime, _spriteBatch));
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion
    }
}