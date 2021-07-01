﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace UIComponents
{
    public static class Timer
    {
        #region Fields

        private static float _timeRemaining;

        private static bool _isExpired = false;

        private static Action _callback;

        #endregion

        #region Properties

        public static string _timeRemainingFormatted => 
            $"Time Remaining: {Math.Round(_timeRemaining, MidpointRounding.ToZero)}";

        #endregion

        #region Methods

        public static void Reset(float newTime = 60)
        {
            _timeRemaining = newTime;
            _isExpired = false;
        }

        public static void AddListener(Action listener)
        {
            _callback += listener;
        }
        
        public static void Update(GameTime gameTime)
        {
            if (_isExpired)
            {
                return;
            }

            _timeRemaining -= (float) gameTime.ElapsedGameTime.TotalSeconds;

            _timeRemaining = MathF.Max(0F, _timeRemaining);

            if (_timeRemaining != 0F) return;
            _isExpired = true;
            _callback?.Invoke();
        }

        #endregion
    }
}