using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameComponents
{
    public abstract class Component
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch _spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}