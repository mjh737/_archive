using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphite
{
    public class SkyBox : DrawableGameComponent
    {
        Quad[] quads;
        BasicEffect effect;

        public SkyBox(Game game) : base(game)
        {
            quads = new Quad[6];
        }

        public override void Initialize()
        {
            quads[0] = new Quad(@"Lobby\Up", new Vector3[] {
                new Vector3(-1, 1, 1),
                new Vector3(1,1,1),
                new Vector3(1,1,-1),
                new Vector3(-1,1,-1),
            });

            quads[1] = new Quad(@"Lobby\Down", new Vector3[] {
                new Vector3(-1, -1, 1),
                new Vector3(1,-1,1),
                new Vector3(1,-1,-1),
                new Vector3(-1,-1,-1),
            });

            quads[2] = new Quad(@"Lobby\Front", new Vector3[] {
                new Vector3(-1, 1, 1),
                new Vector3(1,1,1),
                new Vector3(1,-1,1),
                new Vector3(-1,-1,1)
            });

            quads[3] = new Quad(@"Lobby\Back", new Vector3[] {
                new Vector3(-1, 1, -1),
                new Vector3(1,1,-1),
                new Vector3(1,-1,-1),
                new Vector3(-1,-1,-1)
            });

            quads[4] = new Quad(@"Lobby\Left", new Vector3[] {
                new Vector3(-1, 1, -1),
                new Vector3(-1,1,1),
                new Vector3(-1,-1,1),
                new Vector3(-1,-1,-1)
            });

            quads[5] = new Quad(@"Lobby\Right", new Vector3[] {
                new Vector3(1, 1, -1),
                new Vector3(1,1,1),
                new Vector3(1,-1,1),
                new Vector3(1,-1,-1)
            });

            base.Initialize();
            
            effect = new BasicEffect(GraphicsDevice, null);
        }

        protected override void LoadContent()
        {
            foreach (Quad quad in quads)
            {
                quad.LoadContent(Game.Content);
            }

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            effect.Begin();

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();
                {
                    foreach (Quad quad in quads)
                    {
                        quad.Draw(GraphicsDevice);
                    }
                    pass.End();
                }
            }

            effect.End();

            base.Draw(gameTime);
        }
    }
}
