using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameComponents
{
    public class Button : Component
    {
        //Fields
        private MouseState _currentMouseState;
        private MouseState _previousMouseState;
        private SpriteFont _spriteFont;
        private bool _isHovering;
        private Texture2D _texture;
        //Properties
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Color PenColor { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle => new Rectangle((int) Position.X, (int) Position.Y, _texture.Width, _texture.Height);
        public string Text { get; set; }
        //Methods
        public Button(Texture2D texture, SpriteFont spriteFont)
        {
            _texture = texture;
            _spriteFont = spriteFont;
            PenColor = Color.Black;
        }
        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            var color = Color.White;
            if (_isHovering)
            {
                color = Color.Gray;
            }
            _spriteBatch.Draw(_texture, Rectangle, color);
            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_spriteFont.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_spriteFont.MeasureString(Text).Y / 2);
                _spriteBatch.DrawString(_spriteFont, Text, new Vector2(x, y), PenColor);

            }
        }
        public override void Update(GameTime gameTime)
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
    }
}