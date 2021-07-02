using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Match3.GameComponents.UIComponents.ScreenComponents
{
    public class Screen
    {
        #region Fields

        protected ContentManager content;
        protected readonly Color _backgroundColor = Color.CornflowerBlue;

        public ScreenManager ScreenManager { get; internal set; }

        #endregion

        #region Methods

        public virtual void Load()
        {
            
        }

        public virtual void Unload()
        {
            
        }

        public virtual void Draw(GameTime gameTime)
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public void ExitScreen()
        {
            ScreenManager.RemoveScreen(this);
        }
        #endregion
    }
}