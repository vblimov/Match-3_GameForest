using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Match3.Enums;
using Match3.GameParams;
using Match3.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Linq.Expressions;
using Match3.GameComponents.UIComponents.Auxiliary;

namespace Match3.GameComponents.TileGrid
{
    public class GridController
    {
        #region Fields

        private MouseState _currentMouseState;
        private MouseState _previousMouseState;
        private Grid _grid = Grid.instance;
        public Tile[,] _tiles;
        private readonly Rectangle _gridRectangle;
        private readonly Random _random = new Random();
        private Tile _currentSelectedTile = null;
        private Tile _previousSelectedTile = null;

        #endregion

        #region Properties

        private TileType RandomTile =>
            (TileType) _random.Next(
                Enum.GetValues(typeof(TileType)).GetLowerBound(0),
                Enum.GetValues(typeof(TileType)).GetUpperBound(0));

        #endregion

        #region Methods

        public GridController()
        {
            var _gridSize = GameSettings._constants.fieldSize * GameSettings._constants.tileSize;
            _gridRectangle = Utility.MathHelper.GetRectangle(
                GameSettings._positions.gridPosition,
                _gridSize,
                _gridSize);
        }

        public void LoadContent(ContentManager Content)
        {
            _tiles = new Tile[GameSettings._constants.fieldSize, GameSettings._constants.fieldSize];
            GameStatesHandler.Change += UpdateGameState;
            FillTiles();
        }

        public void UnloadContent(ContentManager Content)
        {
            GameStatesHandler.Change -= UpdateGameState;
        }

        public void Update(GameTime gameTime)
        {
            if (GameStatesHandler.GameState != GameState.UserInput) return;
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            var mouseRectangle = new Rectangle(
                _currentMouseState.X,
                _currentMouseState.Y,
                GameSettings._constants.minRectangleSize,
                GameSettings._constants.minRectangleSize);

            if (mouseRectangle.Intersects(_gridRectangle) && _currentMouseState.LeftButton == ButtonState.Released &&
                _previousMouseState.LeftButton == ButtonState.Pressed)
            {
                SelectTouchedTile(_currentMouseState.X, _currentMouseState.Y, gameTime);
            }
        }

        private async void UpdateGameState(GameState state)
        {
            
            switch (state)
            {
                case GameState.UserInput:
                    break;
                case GameState.Swiping:
                    await GameStatesHandler.WaitForSeconds(0.25f);
                    SwapTiles();
                    break;
                case GameState.Matching:
                    await GameStatesHandler.WaitForSeconds(0.35f);
                    ClearMatches();
                    break;
                case GameState.Falling:
                    await GameStatesHandler.WaitForSeconds(0.25f);
                    FallTiles();
                    break;
                case GameState.Filling:
                    await GameStatesHandler.WaitForSeconds(0.35f);
                    FillTiles();
                    break;
                case GameState.GeneratingTiles:
                    await GameStatesHandler.WaitForSeconds(0.35f);
                    GameStatesHandler.GameState = CheckMatches() ? GameState.Matching : GameState.UserInput;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void SelectTouchedTile(int xPos, int yPos, GameTime gameTime)
        {
            if (GameStatesHandler.GameState != GameState.UserInput)
            {
                return;
            }
            //get row and column of touched tile
            var (row, column) = (
                (int) ((yPos - GameSettings._positions.gridPosition.Y) / GameSettings._constants.tileSize),
                (int) ((xPos - GameSettings._positions.gridPosition.X) / GameSettings._constants.tileSize));

            _previousSelectedTile = _currentSelectedTile;
            _currentSelectedTile = _tiles[column, row];
            if (_currentSelectedTile.IsSelected)
            {
                _currentSelectedTile.IsSelected = false;
                _previousSelectedTile = null;
                _currentSelectedTile = null;
            }
            else
            {
                _currentSelectedTile.IsSelected = true;
                if (_previousSelectedTile != null &&
                    Utility.MathHelper.IsStandingNear(_previousSelectedTile, _currentSelectedTile))
                {
                    GameStatesHandler.GameState = GameState.Swiping;
                }
                else
                {
                    SelectNewTile(ref _previousSelectedTile, ref _currentSelectedTile);
                }
            }
        }

        private void SwapTiles()
        {
            _currentSelectedTile.IsSelected = false;
            _previousSelectedTile.IsSelected = false;

            Swap(_previousSelectedTile, _currentSelectedTile, false);

            if (CheckMatches())
            {
                SwapTilesPositions(_previousSelectedTile, _currentSelectedTile, false);
                GameStatesHandler.GameState = GameState.Matching;
            }
            else
            {
                SwapTilesPositions(_previousSelectedTile, _currentSelectedTile, true);
                Swap(_previousSelectedTile, _currentSelectedTile, true);
            }
            _previousSelectedTile = null;
            _currentSelectedTile = null;
        }

        private void FillTiles()
        {
            GameStatesHandler.GameState = GameState.GeneratingTiles;
            for (var x = 0; x < _tiles.GetLength(0); x++)
            {
                for (var y = 0; y < _tiles.GetLength(1); y++)
                {
                    _tiles[x, y] ??= new Tile((x, y), RandomTile);
                }
            }
        }

        private bool CheckMatches()
        {
            var hasMatches = false;
            //vertical
            for (var x = 0; x < _tiles.GetLength(0); x++)
            {
                var matchTileCount = 0;
                var currentTileGroup = TileType.None;
                for (var y = 0; y < _tiles.GetLength(1); y++)
                {
                    if (y == 0)
                    {
                        currentTileGroup = _tiles[x, y].TileType;
                    }

                    if (_tiles[x, y].TileType == currentTileGroup)
                    {
                        matchTileCount++;
                    }
                    else
                    {
                        if (matchTileCount >= 3)
                        {
                            hasMatches = true;
                            MarkMatches(x, y, matchTileCount, true, false);
                        }

                        currentTileGroup = _tiles[x, y].TileType;
                        matchTileCount = 1;
                    }

                    if (y == _tiles.GetLength(0) - 1 && matchTileCount >= 3)
                    {
                        hasMatches = true;
                        MarkMatches(x, y, matchTileCount, true, true);
                    }
                }
            }
            //horizontal
            for (var y = 0; y < _tiles.GetLength(0); y++)
            {
                var matchTileCount = 0;
                var currentTileGroup = TileType.None;
                for (var x = 0; x < _tiles.GetLength(1); x++)
                {
                    if (x == 0)
                    {
                        currentTileGroup = _tiles[x, y].TileType;
                    }

                    if (_tiles[x, y].TileType == currentTileGroup)
                    {
                        matchTileCount++;
                    }
                    else
                    {
                        if (matchTileCount >= 3)
                        {
                            hasMatches = true;
                            MarkMatches(x, y, matchTileCount, false, false);
                        }

                        currentTileGroup = _tiles[x, y].TileType;
                        matchTileCount = 1;
                    }

                    if (x == _tiles.GetLength(0) - 1 && matchTileCount >= 3)
                    {
                        hasMatches = true;
                        MarkMatches(x, y, matchTileCount, false, true);
                    }
                }
            }
            return hasMatches;
        }

        private void MarkMatches(int x, int y, int count, bool isVertical, bool isLastInLine)
        {
            var lineOffset = isLastInLine ? 1 : 0;
            var lineOffsetForLastLine = isLastInLine ? 0 : 1;
            for (var k = lineOffset; k <= count - lineOffsetForLastLine; k++)
            {
                var column = isVertical ? x : (x - count + k);
                var row = isVertical ? (y - count + k) : y;
                _tiles[column, row].CanMatch = true;
            }
        }

        private void ClearMatches()
        {
            for (var x = 0; x < _tiles.GetLength(0); x++)
            {
                for (var y = 0; y < _tiles.GetLength(1); y++)
                {
                    if (!_tiles[x, y].CanMatch) continue;
                    
                    Score.IncreaseScore(1);
                    _tiles[x, y] = null;
                }
            }

            GameStatesHandler.GameState = GameState.Falling;
        }

        private void FallTiles()
        {
            for (var x = 0; x < _tiles.GetLength(0); x++)
            {
                var verticalHolesCount = 0;
                for (var y = _tiles.GetLength(1) - 1; y >= 0; y--)
                {
                    if (_tiles[x, y] == null)
                    {
                        verticalHolesCount++;
                    }
                    else if (verticalHolesCount != 0)
                    {
                        _tiles[x, y + verticalHolesCount] = _tiles[x, y];
                        _tiles[x, y + verticalHolesCount].MoveTile(
                            new Vector2(
                                _tiles[x, y].Position.X,
                                _tiles[x, y].Position.Y + verticalHolesCount * GameSettings._constants.tileSize),
                            false);
                        _tiles[x, y] = null;
                    }
                }
            }
            GameStatesHandler.GameState = GameState.Filling;
        }

        private void SelectNewTile(ref Tile previousSelectedTile, ref Tile currentSelectedTile)
        {
            currentSelectedTile.IsSelected = true;
            if (previousSelectedTile != null)
            {
                previousSelectedTile.IsSelected = false;
            }

            previousSelectedTile = null;
        }

        private void Swap(Tile tile1, Tile tile2, bool isRollBack)
        {
            _tiles[tile1.GridPosition.Column, tile1.GridPosition.Row] = isRollBack? tile1 : tile2;
            _tiles[tile2.GridPosition.Column, tile2.GridPosition.Row] = isRollBack? tile2 : tile1;
        }

        private void SwapTilesPositions(Tile previousSelectedTile, Tile currentSelectedTile, bool isRollBack)
        {
            previousSelectedTile.MoveTile(currentSelectedTile.Position, isRollBack);
            currentSelectedTile.MoveTile(previousSelectedTile.Position, isRollBack);
        }

        #endregion
    }
}