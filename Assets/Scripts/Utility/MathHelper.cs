using System;
using Match3.Enums;
using Match3.GameComponents.TileGrid;
using Microsoft.Xna.Framework;

namespace Match3.Utility
{
    public static class MathHelper
    {
        public const float FLOAT_TOLERANCE = 1f;
        public const int GRID_TOLERANCE = 5;
        public static Rectangle GetRectangle(Vector2 position, int width, int height)
        {
            var (x, y) = position;
            return new Rectangle((int)x, (int)y, width, height);
        }
        public static bool IsStandingNear(Tile firstTile, Tile secondTile)
        {
            if (firstTile == null || secondTile == null)
            {
                throw new ArgumentException("Tile cannot be null");
            }
            var standingNearHorizontal = Math.Abs(firstTile.GridPosition.Column - secondTile.GridPosition.Column);
            var standingNearVertical = Math.Abs(firstTile.GridPosition.Row - secondTile.GridPosition.Row);
            return standingNearHorizontal + standingNearVertical == 1;
        }

    }
}