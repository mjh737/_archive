using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Graphite;
using Graphite.Interfaces;
using Graphite.Shaders;

namespace Graphite.Objects
{
    public class xSkybox : xObject, IChildRenderer, ILoadable
    {
        private xTexturedQuad[] sides;
        private string[] files;
        private Vector3[] offsets;

        public xSkybox(string[] textures) {
            files = textures;
            Scaling = new Vector3(500, 500, 500);
            sides = new xTexturedQuad[6];

            CreateSides();
            CalculateOffsets();
        }

        public void LoadGraphicsContent(GraphicsDevice device, ContentManager loader) {
            foreach (xTexturedQuad quad in sides) {
                quad.LoadGraphicsContent(device, loader);
            }
        }

        private void CreateSides() {
            for (int i = 0; i < 6; i++) {
                sides[i] = new xTexturedQuad(files[i]);
                sides[i].Scaling = Scaling;
            }

            sides[0].Rotation = Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), MathHelper.PiOver2);
            sides[1].Rotation = Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), -MathHelper.PiOver2);
            sides[2].Rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), MathHelper.PiOver2);
            sides[3].Rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), -MathHelper.PiOver2);
            sides[5].Rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), MathHelper.Pi);
        }

        private void CalculateOffsets()
        {
            offsets = new Vector3[6];

            offsets[0] = new Vector3(0, 0.5f, 0) * Scaling;
            offsets[1] = new Vector3(0, -0.5f, 0) * Scaling;
            offsets[2] = new Vector3(-0.5f, 0, 0) * Scaling;
            offsets[3] = new Vector3(0.5f, 0, 0) * Scaling;
            offsets[4] = new Vector3(0, 0, -0.5f) * Scaling;
            offsets[5] = new Vector3(0, 0, 0.5f) * Scaling;
        }

        public void RenderChildren(GraphicsDevice device)
        {
            xShader shader = xShaderManager.GetShader(objShader);

            shader.Effect.Begin();
            foreach (EffectPass pass in shader.Effect.CurrentTechnique.Passes) {
                for (int i = 0; i < 6; i++)
                {
                    sides[i].Position = xCameraManager.ActiveCamera.Position + offsets[i];

                    shader.SetParameters(sides[i]);

                    pass.Begin();
                    sides[i].Render(device);
                    pass.End();
                }
            }
            shader.Effect.End();
        }
    }
}