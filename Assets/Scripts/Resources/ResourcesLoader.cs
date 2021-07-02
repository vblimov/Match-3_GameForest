using System;
using System.Collections.Generic;
using System.Linq;
using Match3.Enums;
using Match3.GameParams;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Match3.Resources
{
    public static class ResourcesLoader
    {
        #region Fields

        public static SpriteFont Font { get; private set; }

        public static Texture2D Button { get; private set; }

        public static Texture2D Tile { get; private set; }

        public static Dictionary<TileType, Texture2D> Tiles { get; } =
            new Dictionary<TileType, Texture2D>(Enum.GetValues(typeof(TileType)).Length);

        #endregion

        #region Methods

        public static void Load(ContentManager Content)
        {
            Font = Content.Load<SpriteFont>(GameSettings._paths.fontPath);
            Button = Content.Load<Texture2D>(GameSettings._paths.buttonPath);
            Tile = Content.Load<Texture2D>(GameSettings._paths.tilePath);
            foreach (var tile in Enum.GetValues(typeof(TileType)).Cast<TileType>().ToList())
            {
                Tiles.Add(tile, Content.Load<Texture2D>(tile.TexturePath()));
            }
        }

        #endregion
    }
}