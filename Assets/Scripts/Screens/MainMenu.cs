using System;
using System.Collections.Generic;
using GameParams;
using Match_3_GameForest.Screens;
using UIComponents.ScreenComponents;
using UIComponents.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

    public class MainMenu : Screen
    {
        #region Fields
        
        private SpriteBatch _spriteBatch;
        private List<TouchableComponent> buttons;

        #endregion
        
        #region Methods

        public MainMenu(ScreenManager screenManager)
        {
            ScreenManager = screenManager;
        }

        public override void Load()
        {
            var exitBut = new Button(
                ScreenManager.Game.Content.Load<Texture2D>(GameSettings._paths.buttonPath),
                ScreenManager.Game.Content.Load<SpriteFont>(GameSettings._paths.fontPath),
                new Vector2(250, 250),
                GameSettings._names.playButtonText);
            buttons = new List<TouchableComponent>()
            {
                exitBut
            };
            exitBut.Click += PlayGame;
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
            buttons.ForEach(button => button.Draw(gameTime, _spriteBatch));
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion
    }
