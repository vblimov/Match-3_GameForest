using System;
using System.Collections.Generic;
using GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

    public class MainMenu : Screen
    {
        #region Fields

        private ScreenManager _screenManager;
        private Button _playButton;
        private SpriteBatch _spriteBatch;

        #endregion
        
        #region Methods

        public MainMenu(ScreenManager screenManager)
        {
            _screenManager = screenManager;
        }

        public override void Load()
        {
            _playButton = new Button(
                _screenManager.Game.Content.Load<Texture2D>(GameSettings.Paths.buttonPath),
                _screenManager.Game.Content.Load<SpriteFont>(GameSettings.Paths.fontPath),
                new Vector2(250, 250),
                GameSettings.Names.playButtonText);
            
            base.Load();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch = _screenManager.SpriteBatch;
            Console.Write(_spriteBatch);
            _spriteBatch.Begin();
            _playButton.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        #endregion
    }
