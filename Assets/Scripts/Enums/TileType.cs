using System;
using GameParams;
using Microsoft.Xna.Framework;

namespace Enums
{
    public enum TileType
    {
        Apple,
        Cherry,
        Lemon,
        Pear
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
                _ => throw new ArgumentOutOfRangeException(nameof(tileType), tileType, null)
            };
        }
    }
}