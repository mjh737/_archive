using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using System.Drawing;

namespace Sapphire
{
    public class sSprite : sObject
    {
        private Sprite sprite;
        private Texture texture;
        private string fileName;

        public sSprite(string path, Vector3 pos, int width, int height, Device device)
        {
            this.position = pos;
            this.sprite = new Sprite(device);
            this.scaling = new Vector3(width, height, 1);
            this.fileName = path;
            ReloadResources(device);
        }

        public override void ReloadResources(Device device)
        {
            this.texture = TextureLoader.FromFile(device, fileName);
        }

        public override void Render(Device device)
        {
            Vector3 textureCenter = new Vector3
                (
                    texture.GetSurfaceLevel(0).Description.Width / 2,
                    texture.GetSurfaceLevel(0).Description.Height / 2,
                    0
                );
            sprite.Transform = Matrix.Transformation
            (
                new Vector3(),
                new Quaternion(),
                new Vector3(scaling.X/(textureCenter.X*2), scaling.Y/(textureCenter.Y*2),0),
                textureCenter,
                new Quaternion(),
                new Vector3()
            );

            sprite.Begin(SpriteFlags.AlphaBlend);
            sprite.Draw(texture, new Vector3(), position, Color.White.ToArgb());
            sprite.End();
        }
    }
}
