using System;
using Match3.GameParams;
using Microsoft.Xna.Framework;

namespace Match3.Enums
{
    public enum TileType
    {
        Apple,
        Cherry,
        Lemon,
        Pear,
        Coconut,
        None
    }

    public static class TileTypeExtension
    {
        public static string TexturePath(this TileType tileType)
        {
            return tileType switch
            {
                TileType.Apple => GameSettings._paths.appleTilePath,
                TileType.Cherry => GameSettings._paths.cherryTilePath,
                TileType.Lemon => GameSettings._paths.lemonTilePath,
                TileType.Pear => GameSettings._paths.pearTilePath,
                TileType.Coconut => GameSettings._paths.coconutTilePath,
                TileType.None => GameSettings._paths.tilePath,
                _ => throw new ArgumentOutOfRangeException(nameof(tileType), tileType, null)
            };
        }
    }
}