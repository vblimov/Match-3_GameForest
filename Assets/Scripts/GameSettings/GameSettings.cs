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
            public Color pressedColor;
            public Color selectedColor;
            public Color penColor;
        }
        public static SColors _colors = new SColors()
        {
            backgroundColor = Color.CornflowerBlue,
            defaultColor = Color.White,
            hoveredColor = Color.LightGray,
            pressedColor = Color.Gray,
            selectedColor = Color.IndianRed,
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
        };

        public struct SNames
        {
            public string playButtonText;
        }
        public static SNames _names = new SNames()
        {
            playButtonText = "Play"
        };

        public struct SConstants
        {
            public int fieldSize;
            public float defaultScale;
            public float tileScale;
            public int minRectangleSize;
            public int tileSize;
        }
        public static SConstants _constants = new SConstants()
        {
            fieldSize = 8,
            minRectangleSize = 1,
            tileSize = 51
        };

        public struct SPositions
        {
            public Vector2 scorePosition;
            public Vector2 timerPosition;
            public Vector2 gridPosition;
        }
        public static SPositions _positions = new SPositions()
        {
            gridPosition = new Vector2(0,0),
            scorePosition = new Vector2(500, 25),
            timerPosition = new Vector2(500, 55)
        };
    }
}