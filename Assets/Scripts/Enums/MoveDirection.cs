using System;

namespace Match3.Enums
{
    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right,
        None
    }

    public static class MoveDirectionExtension
    {
        public static int GetDirection(this MoveDirection direction)
        {
            return direction switch
            {
                MoveDirection.Up => 1,
                MoveDirection.Down => -1,
                MoveDirection.Right => 1,
                MoveDirection.Left => -1,
                MoveDirection.None => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}