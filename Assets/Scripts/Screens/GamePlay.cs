using System;
using System.Collections.Generic;
using Enums;
using GameParams;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using UIComponents;
using UIComponents.GameComponents;
using UIComponents.GameComponents.TileGrid;
using UIComponents.ScreenComponents;

namespace Match_3_GameForest.Screens
{
    public class GamePlay : Screen
    {
        #region Fields
        
        private SpriteBatch _spriteBatch;
        private Grid _grid = new Grid();

        #endregion

        #region Properties

        #endregion

        #region Methods

        public GamePlay(ScreenManager screenManager)
        {
            ScreenManager = screenManager;
            _spriteBatch = ScreenManager.SpriteBatch;
            content = ScreenManager.Game.Content;
            _grid.LoadContent(content);
        }
        
        public override void Load()
        {
            Timer.Reset();
            Timer.AddListener(() => {ScreenManager.Game.Exit();});
            base.Load();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _grid.Draw(gameTime, _spriteBatch);
            _spriteBatch.DrawString(
                ResourcesLoader.Font, 
                Timer._timeRemainingFormatted, 
                new Vector2(500, 25),
                GameSettings._colors.penColor);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            Timer.Update(gameTime);
        }
        #endregion
    }
}