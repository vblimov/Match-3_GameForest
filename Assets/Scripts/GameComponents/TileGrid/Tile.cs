using System;
using System.Threading.Tasks;
using Match3.Enums;
using Match3.GameComponents.UIComponents.Auxiliary;
using Match3.GameParams;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Match3.Resources;
using Match3.GameComponents.UIComponents.Touchable;
using Match3.GameComponents.UIComponents.ScreenComponents;
using Microsoft.Xna.Framework.Input;
using MathHelper = Microsoft.Xna.Framework.MathHelper;

namespace Match3.GameComponents.TileGrid
{
    enum TileState
    {
        Move,
        Stay
    }
    public class Tile
    {
        #region Fields

        private MouseState _currentMouseState;
        private MouseState _previousMouseState;
        private Texture2D _texture;
        private Color componentColor = GameSettings._colors.defaultColor;
        private bool _isHovering;
        private Vector2 _nextPosition = new Vector2();
        private TileState _state = TileState.Stay;
        private const float lerpSpeed = 6f;

        #endregion

        #region Properties
        public bool CanMatch { get; set; }
        public TileType TileType { get; private set; }
        public bool IsSelected { get; set; }
        public Vector2 Position { get; private set; }

        public (int Column, int Row) GridPosition =>
        (
            (int) Position.X / GameSettings._constants.tileSize,
            (int) Position.Y / GameSettings._constants.tileSize
        );

        private Rectangle Rectangle => new Rectangle(
            (int) Position.X,
            (int) Position.Y,
            _texture.Width,
            _texture.Height);

        #endregion

        #region Methods

        public Tile(Vector2 position, TileType tileType)
        {
            Position = new Vector2(
                position.X * GameSettings._constants.tileSize,
                position.Y * GameSettings._constants.tileSize
            );
            ResourcesLoader.Tiles.TryGetValue(tileType, out _texture);
            TileType = tileType;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            componentColor =
                _isHovering
                    ? GameSettings._colors.hoveredColor
                    : IsSelected
                        ? GameSettings._colors.selectedColor
                        : GameSettings._colors.defaultColor;
            spriteBatch.Draw(_texture, Rectangle, componentColor);
            Update(gameTime);
        }

        private void Update(GameTime gameTime)
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouseState.X, _currentMouseState.Y, 1, 1);
            if (_state == TileState.Move)
            {
                Move(gameTime);
            }
            _isHovering = false;
            if (!mouseRectangle.Intersects(Rectangle)) return;
            if (IsSelected) return;
            _isHovering = true;
        }

        public void MoveTile(Vector2 nextPosition)
        {
            _nextPosition = nextPosition;
            _state = TileState.Move;
        }

        private void Move(GameTime gameTime)
        {
            var newPosition = new Vector2(
                MathHelper.Lerp(Position.X, _nextPosition.X, lerpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds),
                MathHelper.Lerp(Position.Y, _nextPosition.Y, lerpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds)
            );
            var distance = Math.Abs(Vector2.Distance(newPosition, Position));
            Position = newPosition;
            
            if (!(distance < Utility.MathHelper.FLOAT_TOLERANCE)) return;
            _state = TileState.Stay;
            Position = _nextPosition;
            _nextPosition = new Vector2();
        }

        public void Destroy()
        {
            _texture = ResourcesLoader.Tile;
        }
        #endregion
    }
}