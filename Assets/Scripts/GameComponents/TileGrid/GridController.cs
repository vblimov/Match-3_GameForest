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

namespace Match3.GameComponents.TileGrid
{
    public class GridController
    {
        #region Fields

        private MouseState _currentMouseState;
        private MouseState _previousMouseState;
        private Grid _grid = Grid.instance;
        public readonly Tile[,] _tiles = new Tile[GameSettings._constants.fieldSize, GameSettings._constants.fieldSize];
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
            for (var i = 0; i < _tiles.GetLength(0); i++)
            {
                for (var j = 0; j < _tiles.GetLength(1); j++)
                {
                    _tiles[i, j] = new Tile(new Vector2(i, j), RandomTile);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!InputHandler.CanInput) return;
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouseState.X, _currentMouseState.Y, 1, 1);

            if (mouseRectangle.Intersects(_gridRectangle) && _currentMouseState.LeftButton == ButtonState.Released &&
                _previousMouseState.LeftButton == ButtonState.Pressed)
            {
                SelectTouchedTile(_currentMouseState.X, _currentMouseState.Y, gameTime);
            }
        }

        private void SelectTouchedTile(int xPos, int yPos, GameTime gameTime)
        {
            //get row and column of touched tile
            var (row, column) = (
                yPos / GameSettings._constants.tileSize,
                xPos / GameSettings._constants.tileSize);

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
                    InputHandler.DenyInput();
                    SwapTiles(ref _previousSelectedTile, ref _currentSelectedTile, gameTime);
                    InputHandler.AllowInput();
                }
                else
                {
                    SelectNewTile(ref _previousSelectedTile, ref _currentSelectedTile);
                }
            }
        }

        private void SwapTiles(ref Tile previousSelectedTile, ref Tile currentSelectedTile, GameTime gameTime)
        {
            currentSelectedTile.IsSelected = false;
            previousSelectedTile.IsSelected = false;
            previousSelectedTile.MoveTile(currentSelectedTile.Position);
            currentSelectedTile.MoveTile(previousSelectedTile.Position);
            Swap(previousSelectedTile, currentSelectedTile);
            if (CheckMatches())
            {
                ClearMatches();
            }

            previousSelectedTile = null;
            currentSelectedTile = null;
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

        private void Swap(Tile tile1, Tile tile2)
        {
            _tiles[tile2.GridPosition.Column, tile2.GridPosition.Row] = tile1;
            _tiles[tile1.GridPosition.Column, tile1.GridPosition.Row] = tile2;
        }

        private bool CheckMatches()
        {
            var hasMatches = false;
            //vertical
            for (var i = 0; i < _tiles.GetLength(0); i++)
            {
                var matchTileCount = 0;
                var currentTileGroup = TileType.None;
                for (var j = 0; j < _tiles.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        currentTileGroup = _tiles[i, j].TileType;
                    }
                    if (_tiles[i, j].TileType == currentTileGroup)
                    {
                        matchTileCount++;
                    }
                    else
                    {
                        if (matchTileCount >= 3)
                        {
                            hasMatches = true;
                            MarkMatches(i, j, matchTileCount, true, false);
                        }
                        currentTileGroup = _tiles[i, j].TileType;
                        matchTileCount = 1;
                    }
                    if (j == _tiles.GetLength(0) - 1 && matchTileCount >= 3)
                    {
                        hasMatches = true;
                        MarkMatches(i, j, matchTileCount, true, true);
                    }
                }
            }
            //horizontal
            for (var i = 0; i < _tiles.GetLength(0); i++)
            {
                var matchTileCount = 0;
                var currentTileGroup = TileType.None;
                for (var j = 0; j < _tiles.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        currentTileGroup = _tiles[j, i].TileType;
                    }
                    if (_tiles[j, i].TileType == currentTileGroup)
                    {
                        matchTileCount++;
                    }
                    else
                    {
                        if (matchTileCount >= 3)
                        {
                            hasMatches = true;
                            MarkMatches(j, i, matchTileCount, false, false);
                        }
                        currentTileGroup = _tiles[j, i].TileType;
                        matchTileCount = 1;
                    }
                    if (j == _tiles.GetLength(0) - 1 && matchTileCount >= 3)
                    {
                        hasMatches = true;
                        MarkMatches(j, i, matchTileCount, false, true);
                    }
                }
            }
            return hasMatches;
        }

        private void MarkMatches(int i, int j, int count, bool isVertical, bool isLastInLine)
        {
            var lineOffset = isLastInLine ? 1 : 0;
            var lineOffsetForLastLine = isLastInLine ? 0 : 1;
            for (var k = lineOffset; k <= count - lineOffsetForLastLine; k++)
            {
                var column = isVertical ? i : (i - count + k);
                var row = isVertical ? (j - count + k) : j;
                _tiles[column,row].CanMatch = true;
            }
        }

        private void ClearMatches()
        {
            for (var i = 0; i < _tiles.GetLength(0); i++)
            {
                for (var j = 0; j < _tiles.GetLength(1); j++)
                {
                    if (_tiles[i, j].CanMatch)
                    {
                        _tiles[i, j].Destroy();
                    }
                }
            }
        }

        #endregion
    }
}