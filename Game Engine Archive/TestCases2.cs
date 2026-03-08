using System;
using System.Collections.Generic;
using System.Text;
using Graphite.Managers;
using NUnit.Framework;
using Graphite.GameObjectComponents;
using Microsoft.Xna.Framework;
using Graphite.Cameras;

namespace UnitTests
{
    [TestFixture]
    public class TestCases
    {
        Executive host;

        [TestFixtureSetUp]
        public void SetupTestFixture()
        {
            host = new Executive();
            host.Exit();
            host.Run();
        }

        [Test, Category("StartUp")]
        public void GameStartUp()
        {
            Assert.IsNotNull(host);
            Assert.IsNotNull(host.GraphicsDevice);
            Assert.IsNotNull(host.Content);
        }

        [Test, Category("ShaderManager")]
        public void ShaderManagerUsage()
        {
            Shader test = new Shader("");
            ShaderManager.AddShader("test", test);
            Assert.AreEqual(test, ShaderManager.GetShader("test"));
        }

        [Test, Category("ShaderManager")]
        public void ShaderManagerGetFailed()
        {
            //Test for failure
            Assert.IsNull(ShaderManager.GetShader("testShader"));
        }

        [Test, Category("ScreenGraphManager")]
        public void ScreenManagerCreated()
        {
            //Assert.IsNotNull(host.SceneManager);
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

        [Test, Category("CameraManager")]
        public void CameraManagerLoaded()
        {
            //Assert.IsNotNull(host.CameraManager);
        }

        [Test, Category("CameraManager")]
        public void CamerasAdded()
        {
            Camera camera = new Camera(host.GraphicsDevice.Viewport);
            CameraManager.AddCamera("test", camera);
        }

        [TestFixtureTearDown]
        public void TestFixtureTesrDown()
        {
            host = null;
        }
    }
}
