using System;
using Enums;
using GameParams;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UIComponents.GameComponents;
using UIComponents.GameComponents.TileGrid;
using UIComponents.ScreenComponents;

namespace Match_3_GameForest.Screens
{
    public class GamePlay : Screen
    {
        #region Fields
        
        private SpriteBatch _spriteBatch;
        private Tile _tile;

        #endregion

        #region Properties

        #endregion

        #region Methods

        public GamePlay(ScreenManager screenManager)
        {
            ScreenManager = screenManager;
        }
        
        public override void Load()
        {
            _tile = new Tile(
                ScreenManager.Game.Content.Load<Texture2D>("Tiles/Apple"),
                new Vector2(0, 0),
                TileType.Apple
            );
            base.Load();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch = ScreenManager.SpriteBatch;
            _spriteBatch.Begin();
            _tile.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion
    }
}