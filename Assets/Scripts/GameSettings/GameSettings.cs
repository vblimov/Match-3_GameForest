using Microsoft.Xna.Framework;

namespace Match3.GameParams
{
    public static class GameSettings
    {
        public struct SColors
        {
            public Color backgroundColor;
            public Color defaultColor;
            public Color hoveredColor;
            public Color canMatchedColor;
            public Color selectedColor;
            public Color penColor;
        }
        public static SColors _colors = new SColors()
        {
            backgroundColor = Color.CornflowerBlue,
            defaultColor = Color.White,
            hoveredColor = Color.LightGray,
            canMatchedColor = Color.IndianRed,
            selectedColor = Color.Gray,
            penColor = Color.Black
        };

        public struct SPaths
        {
            public string contentPath;
            public string buttonPath;
            public string tilePath;
            public string fontPath;
            public string appleTilePath;
            public string lemonTilePath;
            public string pearTilePath;
            public string cherryTilePath;
            public string coconutTilePath;
            public string gridPath;
        }
        public static SPaths _paths = new SPaths()
        {
            contentPath = "Content",
            buttonPath = "UI" + "/" + "button",
            tilePath = "Tiles" + "/" + "tile",
            fontPath = "Fonts" + "/" + "font",
            appleTilePath = "Tiles" + "/" + "Apple",
            lemonTilePath = "Tiles" + "/" + "Lemon",
            pearTilePath = "Tiles" + "/" + "Pear",
            cherryTilePath = "Tiles" + "/" + "Cherry",
            coconutTilePath = "Tiles" + "/" + "coconut",
            gridPath = "Tiles" + "/" + "grid",
        };

        public struct SNames
        {
            public string playButtonText;
            public string exitButtonText;
        }
        public static SNames _names = new SNames()
        {
            playButtonText = "Play",
            exitButtonText = "OK"
        };

        public struct SConstants
        {
            public int fieldSize;
            public int minRectangleSize;
            public int tileSize;
        }
        public static SConstants _constants = new SConstants()
        {
            fieldSize = 8,
            minRectangleSize = 1,
            tileSize = 50
        };

        public struct SPositions
        {
            public Vector2 scorePosition;
            public Vector2 finalScorePosition;
            public Vector2 timerPosition;
            public Vector2 gridPosition;
            public Vector2 defaultButtonPosition;
        }
        public static SPositions _positions = new SPositions()
        {
            gridPosition = new Vector2(0,0),
            scorePosition = new Vector2(500, 25),
            finalScorePosition = new Vector2(350, 150),
            timerPosition = new Vector2(500, 55),
            defaultButtonPosition = new Vector2(350, 250)
        };
    }
}