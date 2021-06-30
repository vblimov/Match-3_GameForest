using System.Collections.Generic;
using System.Xml.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameComponents
{
    public class ScreenManager : DrawableGameComponent
    {
        #region Fields

        private List<Screen> _screens = new List<Screen>();
        private bool isInitialized = false;

        #endregion

        #region Properties

        public SpriteBatch SpriteBatch { get; private set; }

        #endregion

        #region Methods

        public ScreenManager(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            isInitialized = true;
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            _screens.ForEach(screen => screen.Load());
        }

        protected override void UnloadContent()
        {
            _screens.ForEach(screen => screen.Unload());
        }

        public override void Draw(GameTime gameTime)
        {
            _screens.ForEach(screen => screen.Draw(gameTime));
        }

        public override void Update(GameTime gameTime)
        {
            _screens[0].Update(gameTime);
        }

        public void AddScreen(Screen newScreen)
        {
            _screens.Add(newScreen);
            if (isInitialized) newScreen.Load();
        }

        #endregion
    }
}