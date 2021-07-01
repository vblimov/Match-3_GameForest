using Microsoft.Xna.Framework;

namespace GameParams
{
    public static class GameSettings
    {
        public struct SColors
        {
            public Color backgroundColor;
            public Color defaultColor;
            public Color pressedColor;
            public Color penColor;
        }
        public static SColors _colors = new SColors()
        {
            backgroundColor = Color.CornflowerBlue,
            defaultColor = Color.White,
            pressedColor = Color.Gray,
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
        }

        public static SConstants _constants = new SConstants()
        {
            fieldSize = 8,
            defaultScale = 1f,
            tileScale = 0.1f
        };
    }
}