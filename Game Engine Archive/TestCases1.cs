using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Demo;
using Graphite.Managers;
using Graphite.Effects;
using Graphite.GameObjectComponentSystem;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Graphite;

namespace UnitTests
{
    [TestFixture]
    public class TestCases
    {
        private Executive game = null;

        [TestFixtureSetUp]
        [ExpectedException(typeof(ContentLoadException))]
        public void SetupTestFixture()
        {
            try
            {
                game = new Executive();
                Executive.Game = game;
                game.Exit();
                game.Run();
            }
            catch (ContentLoadException)
            {
                game.Exit();
            }
        }

        [Test]
        public void GameStartUp()
        {
            Assert.IsNotNull(Executive.Game,            "EngineManager.Game is null");
            Assert.IsNotNull(Executive.Device,          "EngineManager.Device is null");
            Assert.IsNotNull(Executive.ContentManager,  "EngineManager.ContentManager is null");
        }

        [Test]
        public void FPSCounter()
        {
            Assert.IsNotNull(Executive.FPSManager);
            Assert.AreEqual("0", Executive.FPSManager.FPS.ToString());
        }

        [Test, Category("ShaderManager")]
        public void ShaderManagerCreated()
        {
            Assert.IsTrue(ShaderManager.Initialized);
            Assert.IsNotNull(ShaderManager.GetShader("BasicEffect"));
            Assert.AreEqual(typeof(fxBasic), ShaderManager.GetShader("BasicEffect").GetType());
        }

        [Test, Category("ShaderManager")]
        public void ShaderManagerGetFailed()
        {
            //Test for failure
            Assert.IsNull(ShaderManager.GetShader("testShader"));
        }

        [Test, Category("TextureManager")]
        public void TextureManagerCreated()
        {
            Assert.IsTrue(TextureManager.Initialized);
        }

        [Test, Category("TextureManager")]
        public void TextureManagerGetFailed()
        {
            Assert.IsNull(TextureManager.GetTexture("testTexture"));
        }

        [Test, Category("InputManager")]
        public void InputManagerCreated()
        {
            Assert.IsNotNull(Executive.InputManager);
        }

        [Test, Category("ScreenManager")]
        public void ScreenManagerCreated()
        {
            //Assert.IsNotNull(ScreenManager.SpriteFont);
            Assert.IsNotNull(DisplayManager.SpriteBatch);
        }

        [Test, Category("CameraManager")]
        public void CameraManagerCreated()
        {
            Assert.IsTrue(CameraManager.Initialized);
        }

        [Test, Category("SceneGraphManager")]
        public void SceneGraphManagerCreated()
        {
            Assert.IsNotNull(SceneGraphManager.SceneRoot);
        }

        [Test, Category("SceneGraphManager")]
        public void SceneObjectsLoaded()
        {
            GameObject gameObject = new GameObject();
            gameObject.Position = new Vector3(100, 100, 100);
            SceneGraphManager.AddNode(gameObject);
            Assert.AreEqual(1, SceneGraphManager.SceneRoot.ChildNodes.Count);
        }

        [TestFixtureTearDown]
        public void TestFixtureTesrDown()
        {
            game = null;
        }
    }
}
