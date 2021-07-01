using Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UIComponents.GameComponents.TileGrid
{
    public class Tile : TouchableComponent
    {
        public TileType _tileType;
        public Tile(Texture2D texture, Vector2 position, TileType tileType) : base(texture, position)
        {
            _texture = texture;
            Position = position;
            _tileType = tileType;
        }
        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            base.Draw(gameTime, _spriteBatch);
        }
    }
}