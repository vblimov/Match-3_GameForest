using Enums;
using GameParams;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using UIComponents.ScreenComponents;

namespace UIComponents.GameComponents.TileGrid
{
    public class Tile : TouchableComponent
    {
        #region Fields

        public TileType _tileType;
        public bool isSelected { get; private set; }

        #endregion

        // #region Properties
        //
        // public int Row { get; set; }
        // public int Column { get; set; }
        //
        // #endregion

        #region Methods

        public Tile(Vector2 position, TileType tileType) : base(position)
        {
            Position = new Vector2(
                position.X * GameSettings._constants.tileSize,
                position.Y * GameSettings._constants.tileSize
            );
            _tileType = tileType;
            ResourcesLoader.Tiles.TryGetValue(_tileType, out _texture);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(_texture, Rectangle, componentColor);
        }

        #endregion
    }
}