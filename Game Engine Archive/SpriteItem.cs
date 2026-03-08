using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using Engine;
public class HMSprite : Item
{
    private Sprite mySprite;
    private Texture myTexture;
    private string myPath;

    public HMSprite(string imagePath, Vector3 location, int width, int height, Device myDevice)
    {
        myPosition = location;
        mySprite = new Sprite(myDevice);
        myScaling = new Vector3(width, height, 1);
        myPath = imagePath;
        myTexture = TextureLoader.FromFile(myDevice, myPath);
    }

    public override void ReloadResources(Device myDevice)
    {
        myTexture = TextureLoader.FromFile(myDevice, myPath);
    }

    public override void Render(Camera myCamera, Device myDevice)
    {
        Vector3 texCenter = new Vector3(
            myTexture.GetSurfaceLevel(0).Description.Width / 2,
            myTexture.GetSurfaceLevel(0).Description.Height / 2,
            0
        );

        mySprite.Transform = Matrix.Transformation(
            new Vector3(), new Quaternion(),
            new Vector3(myScaling.X / (texCenter.X * 2), myScaling.Y / (texCenter.Y * 2), 0),
            texCenter, new Quaternion(), new Vector3()
        );

        mySprite.Begin(SpriteFlags.AlphaBlend);
        mySprite.Draw(myTexture, new Vector3(), myPosition, 16777215);
        mySprite.End();
    }
}