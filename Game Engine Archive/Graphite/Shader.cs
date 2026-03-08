using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Graphite
{
    public class Shader
    {
        string content;
        Effect effect; public Effect Effect { get { return effect; } }

        public Shader(string content, ContentManager contentManager)
        {
            this.content = content;
            contentManager.RootDirectory = "C:\\Users\\Matt\\Desktop\\Shuttle\\Visual Studio 2008\\Projects\\Xapphire\\X3\\Graphite\\bin\\x86\\Debug\\Content";
            
            effect =  contentManager.Load<Effect>(content);
        }

        public void SetParameters()
        {
            effect.Parameters["World"].SetValue(Matrix.Identity);
            if (effect.Parameters["View"] != null) effect.Parameters["View"].SetValue(Executive.Camera.View);
            if (effect.Parameters["Projection"] != null) effect.Parameters["Projection"].SetValue(Executive.Camera.Projection);

            SetLight();
        }

        public void SetLight()
        {
            if (effect.Parameters["AmbientLightColor"] != null) effect.Parameters["AmbientLightColor"].SetValue(new Vector4(0.25f, 0.25f, 0.25f, 1.0f));
            if (effect.Parameters["LightDiffuseColor"] != null) effect.Parameters["LightDiffuseColor"].SetValue(new Vector4(0.50f, 0.50f, 0.50f, 1.0f));
            if (effect.Parameters["MaterialDiffuseColor"] != null) effect.Parameters["MaterialDiffuseColor"].SetValue(new Vector4(0.50f, 0.50f, 0.50f, 1.0f));
            if (effect.Parameters["LightPosition"] != null) effect.Parameters["LightPosition"].SetValue(new Vector3(1000,1000,1000));
            if (effect.Parameters["EyePosition"] != null) effect.Parameters["EyePosition"].SetValue(Executive.Camera.Position);
            if (effect.Parameters["SpecularPower"] != null) effect.Parameters["SpecularPower"].SetValue(32);
            if (effect.Parameters["SpecularColor"] != null) effect.Parameters["SpecularColor"].SetValue(new Vector4(0.50f, 0.50f, 0.50f, 1.0f));
            
        }
    }
}
