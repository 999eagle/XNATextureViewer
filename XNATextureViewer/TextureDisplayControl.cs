using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XNATextureViewer
{
    class TextureDisplayControl : GraphicsDeviceControl
    {
        Texture2D texture;

        protected override void Initialize()
        {
        }

        public void LoadNewTexture(string filePath)
        {
            texture = Content.Load<Texture2D>(filePath);
            Invalidate();
        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (texture != null)
            {
                SpriteBatch.Begin();
                SpriteBatch.Draw(texture, new Rectangle(0, 0, Math.Min(texture.Width, Width), Math.Min(texture.Height, Height)), Color.White);
                SpriteBatch.End();
            }
        }
    }
}
