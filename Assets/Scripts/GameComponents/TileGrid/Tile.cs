using System;
using System.Security;
using System.Threading.Tasks;
using Match3.Enums;
using Match3.GameComponents.UIComponents.Auxiliary;
using Match3.GameParams;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Match3.Resources;
using Match3.GameComponents.UIComponents.Touchable;
using Match3.GameComponents.UIComponents.ScreenComponents;
using Match3.Utility;
using Microsoft.Xna.Framework.Input;
using MathHelper = Microsoft.Xna.Framework.MathHelper;

namespace Match3.GameComponents.TileGrid
{
    public class Tile
    {
        #region Fields

        private MouseState _currentMouseState;
        private MouseState _previousMouseState;
        private Texture2D _texture;
        private Color componentColor = GameSettings._colors.defaultColor;
        private bool _isHovering;
        private Vector2 _previousPosition;
        private Vector2 _nextPosition;
        private TileState _state = TileState.Stay;
        private const float lerpSpeed = 0.1f;
        private float alphaValue = 5;

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

        public Tile((int column, int row) gridPosition, TileType tileType)
        {
            Position = new Vector2(
                gridPosition.column * GameSettings._constants.tileSize,
                gridPosition.row * GameSettings._constants.tileSize
            );
            ResourcesLoader.Tiles.TryGetValue(tileType, out _texture);
            TileType = tileType;
        }

        public Tile(Tile tile)
        {
            Position = new Vector2(
                tile.GridPosition.Column * GameSettings._constants.tileSize,
                tile.GridPosition.Row * GameSettings._constants.tileSize
            );
            ResourcesLoader.Tiles.TryGetValue(tile.TileType, out _texture);
            TileType = tile.TileType;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            componentColor =
                _isHovering
                    ? GameSettings._colors.hoveredColor
                    : IsSelected
                        ? GameSettings._colors.selectedColor
                        : CanMatch
                            ? GameSettings._colors.canMatchedColor
                            : GameSettings._colors.defaultColor;
            alphaValue += alphaValue;
            spriteBatch.Draw(
                _texture, 
                Rectangle, 
                new Color(
                    componentColor.R, 
                    componentColor.G, 
                    componentColor.B, 
                    (byte)MathHelper.Clamp(alphaValue, 0, 255)));
            Update(gameTime);
        }

        private void Update(GameTime gameTime)
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouseState.X, _currentMouseState.Y, 1, 1);
            Move(gameTime, _state);
            _isHovering = false;
            if (!mouseRectangle.Intersects(Rectangle)) return;
            if (IsSelected) return;
            _isHovering = true;
        }

        public void MoveTile(Vector2 nextPosition, bool isRollBack)
        {
            _nextPosition = nextPosition;
            if (isRollBack)
            {
                _previousPosition = Position;
            }

            _state = isRollBack ? TileState.MoveRollBackForward : TileState.Move;
        }

        private void Move(GameTime gameTime, TileState tileState)
        {
            if (tileState == TileState.Stay) return;
            var newPosition = new Vector2(
                MathHelper.Lerp(Position.X, _nextPosition.X, lerpSpeed),
                MathHelper.Lerp(Position.Y, _nextPosition.Y, lerpSpeed)
            );
            var distance = Math.Abs(Vector2.Distance(newPosition, Position));
            Position = newPosition;

            if (distance < Utility.MathHelper.FLOAT_TOLERANCE)
            {
                Position = _nextPosition;
                if (tileState == TileState.MoveRollBackForward)
                {
                    _nextPosition = _previousPosition;
                    _state = TileState.MoveRollBackBackward;
                }
                else
                {
                    _state = TileState.Stay;
                    GameStatesHandler.GameState = GameState.UserInput;
                }
            }
        }

        public void Destroy()
        {
            _texture = ResourcesLoader.Tile;
            TileType = TileType.None;
        }

        #endregion
    }
}