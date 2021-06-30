﻿
    using GameComponents;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Match3Game : Game
    {
        #region Fields
        private static Match3Game instance;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private ScreenManager _screenManager;
        #endregion
        
        #region Methods
        public Match3Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = GameSettings.Paths.contentPath;
            
            IsMouseVisible = true;
            Window.AllowUserResizing = false;
        }

        public static Match3Game getInstance()
        {
            return instance ??= new Match3Game();
        }

        protected override void Initialize()
        {
            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _screenManager.AddScreen(new MainMenu(_screenManager));
            base.LoadContent();
        }
        
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(GameSettings.Colors.backgroundColor);
            base.Draw(gameTime);
        }
        #endregion
    }
