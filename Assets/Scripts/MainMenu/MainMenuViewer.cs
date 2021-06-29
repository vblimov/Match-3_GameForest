using System;
using System.Collections.Generic;
using GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MainMenu
{
    public class MainMenuViewer : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly Color _backgroundColor = Color.CornflowerBlue;

        private List<Component> _menuComponents;
        private MainMenuController MMController;

        //Methods
        public MainMenuViewer()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            MMController = MainMenuController.getInstance();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var playButton = new Button(Content.Load<Texture2D>("UI/button"), Content.Load<SpriteFont>("Fonts/font"))
            {
                Position = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2),
                Text = "Play",
            };
            playButton.Click += MMController.PlayGame;
            var quitButton = new Button(Content.Load<Texture2D>("UI/button"), Content.Load<SpriteFont>("Fonts/font"))
            {
                Position = new Vector2(0, 0),
                Text = "Quit",
            };
            quitButton.Click += QuitGame;
            _menuComponents = new List<Component>
            {
                playButton, quitButton
            };
            base.LoadContent();
        }

        private void QuitGame(object sender, EventArgs e)
        {
            Exit();
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (var component in _menuComponents)
            {
                component.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backgroundColor);
            _spriteBatch.Begin();
            foreach (var component in _menuComponents)
            {
                component.Draw(gameTime, _spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
        
    }
}