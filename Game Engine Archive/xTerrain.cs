using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Graphite
{
    public class xTerrain
    {
        xHeightMap heightMap; public xHeightMap HeightMap { get { return heightMap; } set { heightMap = value; } }
        VertexDeclaration vertexDeclaration;
        Effect effect;
        Matrix world;

        Texture2D L1;
        Texture2D L2;
        Texture2D L3;
        Texture2D L4;

        List<xTerrainPatch> patches;
        int totalPatches; public int TotalPatches { get { return totalPatches; } }
        int drawnPatches; public int DrawnPatches { get { return drawnPatches; } }
        FillMode fillMode; public FillMode FillMode { get { return fillMode; } set { fillMode = value; } }

        public xTerrain(xHeightMap heightMap)
        {
            patches = new List<xTerrainPatch>();
            this.heightMap = (xHeightMap)heightMap.Clone();
        }

        public void LoadContent(ContentManager loader)
        {
            effect = loader.Load<Effect>("Content/Effects/TerrainEffect");
            effect.CurrentTechnique = effect.Techniques["DefaultTechnique"];

            L1 = loader.Load<Texture2D>("Content/Textures/grass");
            L2 = loader.Load<Texture2D>("Content/Textures/mud");
            L3 = loader.Load<Texture2D>("Content/Textures/rock");
            L4 = loader.Load<Texture2D>("Content/Textures/snow");

            BuildTerrain();
        }

        public void Draw(GraphicsDevice device)
        {
            int width = heightMap.Width;
            int depth = heightMap.Depth;
            totalPatches = patches.Count;
            drawnPatches = 0;
            device.RenderState.DepthBufferEnable = true;
            device.RenderState.DepthBufferWriteEnable = true;
            device.RenderState.FillMode = fillMode;

            device.VertexDeclaration = vertexDeclaration;
            Matrix worldViewProjection = world * view * projection;
            Vector3 lightDirection = new Vector3(-20.0f * (float)Math.Sin(gameTime.TotalRealTime.TotalMilliseconds * 0.0001f), 0.0f, -20.0f * (float)Math.Cos(gameTime.TotalRealTime.TotalMilliseconds * 0.0001f));

            effect.Parameters["WorldViewProjection"].SetValue(worldViewProjection);
            effect.Parameters["LightDirection"].SetValue(lightDirection);
            effect.Parameters["LayerHeights"].SetValue(new Vector3(16.0f, 21.0f, 27.0f));
            effect.Parameters["L1"].SetValue(L1);
            effect.Parameters["L2"].SetValue(L2);
            effect.Parameters["L3"].SetValue(L3);
            effect.Parameters["L4"].SetValue(L4);

            effect.Begin();

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();

                for (int i = 0; i < patches.Count; ++i)
                {
                    if (frustum.Contains(patches[i].BoundingBox) != ContainmentType.Disjoint)
                    {
                        patches[i].Draw();
                        ++drawnPatches;
                    }
                }
                pass.End();
            }

            effect.End();

            //Make render state solid (so the text displays properly I think)
            fillMode = FillMode.Solid;
        }

        public void BuildTerrain()
        {
            int width = heightMap.Width;
            int depth = heightMap.Depth;

            patches.Clear();

            // Compute the world matrix to place the terrain in the middle of the scene.
            world = Matrix.CreateTranslation((float)width * -0.5f, 0.0f, (float)depth * -0.5f);

            int patchWidth = 16;
            int patchDepth = 16;
            int numPatchesX = width / patchWidth;
            int numPatchesZ = depth / patchDepth;
            int numPatches = numPatchesX * numPatchesZ;

            for (int x = 0; x < numPatchesX; ++x)
            {
                for (int z = 0; z < numPatchesZ; ++z)
                {
                    xTerrainPatch patch = new xTerrainPatch();
                    patch.BuildPatch(heightMap, world, patchWidth, patchDepth, (x + 1) * (patchWidth - 1), (z + 1) * (patchDepth - 1));
                    patches.Add(patch);
                }
            }

            vertexDeclaration = new VertexDeclaration(xGameManager.Instance.Device, VertexPositionNormalTexture.VertexElements);
        }

        public void LoadFromBmp(string filePath)
        {
            heightMap.LoadFromBmpFormat(filePath);
            BuildTerrain();
        }

        public void LoadFromRaw(string filePath)
        {
            heightMap.LoadFromRawFormat(filePath);
            BuildTerrain();
        }

        public void SaveToRaw(string filePath)
        {
            heightMap.SaveToRawFormat(filePath);
            BuildTerrain();
        }
    }
}