using System;
using System.Collections.Generic;
using Match3.GameComponents.UIComponents.Auxiliary;
using Match3.GameComponents.UIComponents.ScreenComponents;
using Match3.GameComponents.UIComponents.Touchable;
using Match3.GameParams;
using Match3.Resources;
using Match3.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3.GameComponents.UIComponents.Screens
{
    public class GameOver : Screen
    {
        #region Fields
        
        private SpriteBatch _spriteBatch;
        private List<TouchableComponent> _buttons;
        
        #endregion

        #region Methods

        public GameOver(ScreenManager screenManager)
        {
            ScreenManager = screenManager;
        }

        public override void Load()
        {
            var exitButton = new Button(
                ResourcesLoader.Button,
                ResourcesLoader.Font,
                GameSettings._positions.defaultButtonPosition,
                GameSettings._names.exitButtonText);
            _buttons = new List<TouchableComponent>()
            {
                exitButton
            };
            exitButton.Click += ExitGame;
            base.Load();
        }

        private void ExitGame(object sender, EventArgs e)
        {
            ScreenManager.AddScreen(new MainMenu(ScreenManager));
            ExitScreen();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch = ScreenManager.SpriteBatch;
            Console.Write(_spriteBatch);
            _spriteBatch.Begin();
            _buttons.ForEach(button => button.Draw(gameTime, _spriteBatch));
            _spriteBatch.DrawString(ResourcesLoader.Font, Score.ScoreFormatted, 
                GameSettings._positions.finalScorePosition, GameSettings._colors.penColor);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion
    }
}