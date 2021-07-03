using System;
using System.Collections.Generic;
using Match3.Enums;
using Match3.GameParams;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace Match3.GameComponents.TileGrid
{
    public class Grid
    {
        #region Fields

        public static Grid instance;
        private readonly GridController _gridController;
        #endregion
        
        #region Methods

        public Grid()
        {
            instance = this;
            _gridController = new GridController();
        }

        public void LoadContent(ContentManager Content)
        {
            _gridController.LoadContent(Content);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (var i = 0; i < _gridController._tiles.GetLength(0); i++)
            {
                for (var j = 0; j < _gridController._tiles.GetLength(1); j++)
                {
                    _gridController._tiles[i, j]?.Draw(gameTime, spriteBatch);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            _gridController.Update(gameTime);
        }
        #endregion
    }
}