using System;
using GameParams;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UIComponents.GameComponents
{
    public class TouchableComponent
    {
        #region Fields

        protected MouseState _currentMouseState;

        protected MouseState _previousMouseState;

        protected Texture2D _texture;

        private bool _isHovering = false;

        protected Color componentColor = GameSettings._colors.defaultColor;

        #endregion

        #region Properties

        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle => new Rectangle((int) Position.X, (int) Position.Y, _texture.Width, _texture.Height);

        #endregion

        #region Methods

        public TouchableComponent(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
        }


        public virtual void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            componentColor = _isHovering ? GameSettings._colors.pressedColor : GameSettings._colors.defaultColor;
            _spriteBatch.Draw(_texture, Rectangle,componentColor);
            Update(gameTime);
        }
        private void Update(GameTime gameTime)
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouseState.X, _currentMouseState.Y, 1, 1);

            _isHovering = false;
            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;
                if (_currentMouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        #endregion
    }
}