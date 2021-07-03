using System;
using Match3.Enums;
using Microsoft.Xna.Framework;

namespace Match3.Utility
{
    public static class GameStatesHandler
    {
        private static GameState _state = GameState.UserInput;

        public delegate void GameStateHandler(GameState state);
        public static event GameStateHandler Change;
        public static GameState GameState
        {
            get => _state;
            set
            {
                _state = value;
                Change?.Invoke(_state);
            }
        }
        
    }
}