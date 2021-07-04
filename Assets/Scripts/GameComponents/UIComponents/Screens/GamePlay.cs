using Match3.Enums;
using Match3.GameParams;
using Match3.Resources;
using Match3.GameComponents.UIComponents.Auxiliary;
using Match3.GameComponents.TileGrid;
using Match3.GameComponents.UIComponents.ScreenComponents;
using Match3.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3.GameComponents.UIComponents.Screens
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
            Timer.AddListener(EndGame);
            Score.Reset();
            base.Load();
        }

        private void EndGame()
        {
            GameStatesHandler.GameState = GameState.UserInput;
            ScreenManager.AddScreen(new GameOver(ScreenManager));
            ExitScreen();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _grid.Draw(gameTime, _spriteBatch);
            _spriteBatch.DrawString(ResourcesLoader.Font, Timer.TimeRemainingFormatted,
                GameSettings._positions.timerPosition, GameSettings._colors.penColor);
            _spriteBatch.DrawString(ResourcesLoader.Font, Score.ScoreFormatted, 
                GameSettings._positions.scorePosition, GameSettings._colors.penColor);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            Timer.Update(gameTime);
            _grid.Update(gameTime);
        }

        public override void ExitScreen()
        {
            base.ExitScreen();
            _grid.UnloadContent(content);
        }
        #endregion
    }
}