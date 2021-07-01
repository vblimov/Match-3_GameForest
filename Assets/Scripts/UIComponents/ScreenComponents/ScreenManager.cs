using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UIComponents.ScreenComponents
{
    public class ScreenManager : DrawableGameComponent
    {
        #region Fields

        private static ScreenManager instance;
        private List<Screen> _screens = new List<Screen>();
        private List<Screen> _screensCopy = new List<Screen>();
        private bool isInitialized;

        #endregion

        #region Properties

        public SpriteBatch SpriteBatch { get; private set; }

        #endregion

        #region Methods
        public static ScreenManager getInstance()
        {
            return instance ??= new ScreenManager(Match3Game.getInstance());
        }
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

            foreach (var screen in _screens)
            {
                screen.Load();
            }
        }

        protected override void UnloadContent()
        {
            foreach (var screen in _screens)
            {
                screen.Unload();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _screensCopy.Clear();
            _screens.ForEach(screen => _screensCopy.Add(screen));
            foreach (var screen in _screensCopy)
            {
                screen.Draw(gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var screen in _screens)
            {
                screen.Update(gameTime);
            }
        }

        public void AddScreen(Screen screen)
        {
            _screens.Add(screen);
            if (isInitialized) screen.Load();
        }

        public void RemoveScreen(Screen screen)
        {
            _screens.Remove(screen);
            screen.Unload();
        }

        #endregion
    }
}