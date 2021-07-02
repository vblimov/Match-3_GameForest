using System;
using Match3.GameParams;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Match3.GameComponents.UIComponents.Touchable
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

        protected Vector2 Position { get; set; }

        protected Rectangle Rectangle => new Rectangle(
            (int) Position.X,
            (int) Position.Y,
            _texture.Width,
            _texture.Height);

        #endregion

        #region Methods

        protected TouchableComponent(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
        }

        protected TouchableComponent(Vector2 position)
        {
            Position = position;
        }


        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            componentColor = _isHovering ? GameSettings._colors.hoveredColor : GameSettings._colors.defaultColor;
            spriteBatch.Draw(_texture, Rectangle, componentColor);
            Update(gameTime);
        }

        protected virtual void Update(GameTime gameTime)
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouseState.X, _currentMouseState.Y, 1, 1);

            _isHovering = false;
            if (!mouseRectangle.Intersects(Rectangle)) return;
            _isHovering = true;
            if (_currentMouseState.LeftButton == ButtonState.Released &&
                _previousMouseState.LeftButton == ButtonState.Pressed)
            {
                Click?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}