using Microsoft.Xna.Framework;

public struct GameSettings
{
    public struct Colors
    {
        public static Color backgroundColor = Color.CornflowerBlue;
        public static Color defaultColor    = Color.White;
        public static Color pressedColor    = Color.Gray;
        public static Color penColor        = Color.Black;
    }
    public struct Paths
    {
        public const string contentPath     = "Content";
        public const string UIPath          = "UI";
        public const string FontPath        = "Fonts";
        public const string buttonPath      = UIPath + "/" + "button";
        public const string fontPath        = FontPath + "/" + "font";
    }
    public struct Names
    {
        public const string playButtonText  = "Play";
    }
    
}