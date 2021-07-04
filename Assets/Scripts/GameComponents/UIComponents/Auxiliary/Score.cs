namespace Match3.GameComponents.UIComponents.Auxiliary
{
    public static class Score
    {
        #region Fields

        private static int _score = 0;

        #endregion

        #region Properties

        public static string ScoreFormatted => $"Score: {_score}";

        #endregion

        #region Methods

        public static void IncreaseScore(int amount)
        {
            _score += amount;
        }
        public static void Reset()
        {
            _score = 0;
        }
        #endregion
    }
}