using System;
using Match3.GameParams;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Match3.GameComponents.UIComponents.Touchable
{
    public class Button : TouchableComponent
    {
        #region Fields

        private readonly SpriteFont _spriteFont;
        private readonly string _text;
        private readonly Color penColor = GameSettings._colors.penColor;
        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont spriteFont, Vector2 position, string text) : base (texture, position)
        {
            _texture = texture;
            _spriteFont = spriteFont;
            Position = position;
            _text = text;
        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            base.Draw(gameTime, _spriteBatch);
            // _spriteBatch.Draw(_texture, Rectangle, componentColor);
            if (string.IsNullOrEmpty(_text)) return;
            var x = (Rectangle.X + (Rectangle.Width / 2)) - (_spriteFont.MeasureString(_text).X / 2);
            var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_spriteFont.MeasureString(_text).Y / 2);
            _spriteBatch.DrawString(_spriteFont, _text, new Vector2(x, y), penColor);
        }
        #endregion
    }
}