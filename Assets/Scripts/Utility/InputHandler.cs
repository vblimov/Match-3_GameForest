using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using Microsoft.Xna.Framework.Input;

namespace Match3.Utility
{
    public static class InputHandler
    {
        private static int _inputDeny = 0;
        public static bool CanInput => _inputDeny == 0;
        // public static MouseState TryGetState()
        // {
        //     if (_inputDeny == 0)
        //     {
        //         return Mouse.GetState();
        //     }
        // }

        public static void AllowInput()
        {
            _inputDeny += 1;
        }

        public static void DenyInput()
        {
            _inputDeny -= 1;
        }
    }
}