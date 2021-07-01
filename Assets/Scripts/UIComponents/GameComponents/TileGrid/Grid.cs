using System;
using System.Collections.Generic;
using Enums;
using GameParams;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace UIComponents.GameComponents.TileGrid
{
    public class Grid
    {
        #region Fields

        public static Grid instance;

        public Tile[,] tiles = new Tile[GameSettings._constants.fieldSize, GameSettings._constants.fieldSize];

        private Random _random = new Random();

        #endregion

        #region Properties

        private TileType RandomTile =>
            (TileType) _random.Next(
                Enum.GetValues(typeof(TileType)).GetLowerBound(0),
                Enum.GetValues(typeof(TileType)).GetUpperBound(0)+1);

        #endregion

        #region Methods

        public Grid()
        {
            instance = this;
        }

        public void LoadContent(ContentManager Content)
        {
            for (var i = 0; i < tiles.GetLength(0); i++)
            {
                for (var j = 0; j < tiles.GetLength(1); j++)
                {
                    tiles[i, j] = new Tile(new Vector2(i, j), RandomTile);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (var i = 0; i < tiles.GetLength(0); i++)
            {
                for (var j = 0; j < tiles.GetLength(1); j++)
                {
                    tiles[i, j].Draw(gameTime, spriteBatch);
                }
            }
        }

        #endregion
    }
}