using Microsoft.DirectX.Direct3D;
using Engine;
using Microsoft.DirectX;
public class HMSkyBox : Item
{
    private string[] textures;
    private HMVertexGroup[] faces = new HMVertexGroup[6];

    // The tex array is a list of files to load for each
    // face of the skybox and should be sent in this order:
    // Front, Right, Back, Left, Top, Bottom
    public HMSkyBox(string[] tex, Device myDevice)
    {
        textures = tex;

        CustomVertex.PositionTextured[] verts;
        int[] inds = { 0, 1, 2, 1, 3, 2 };

        // Front Face
        verts = new CustomVertex.PositionTextured[4];
        verts[0] = new CustomVertex.PositionTextured(new Vector3(-1, 1, 1), 0, 0);
        verts[1] = new CustomVertex.PositionTextured(new Vector3(1, 1, 1), 1, 0);
        verts[2] = new CustomVertex.PositionTextured(new Vector3(-1, -1, 1), 0, 1);
        verts[3] = new CustomVertex.PositionTextured(new Vector3(1, -1, 1), 1, 1);
        faces[0] = new HMVertexGroup(verts, inds, textures[0], myDevice);

        // Right Face
        verts = new CustomVertex.PositionTextured[4];
        verts[0] = new CustomVertex.PositionTextured(new Vector3(1, 1, 1), 0, 0);
        verts[1] = new CustomVertex.PositionTextured(new Vector3(1, 1, -1), 1, 0);
        verts[2] = new CustomVertex.PositionTextured(new Vector3(1, -1, 1), 0, 1);
        verts[3] = new CustomVertex.PositionTextured(new Vector3(1, -1, -1), 1, 1);
        faces[1] = new HMVertexGroup(verts, inds, textures[1], myDevice);

        // Back Face
        verts = new CustomVertex.PositionTextured[4];
        verts[0] = new CustomVertex.PositionTextured(new Vector3(1, 1, -1), 0, 0);
        verts[1] = new CustomVertex.PositionTextured(new Vector3(-1, 1, -1), 1, 0);
        verts[2] = new CustomVertex.PositionTextured(new Vector3(1, -1, -1), 0, 1);
        verts[3] = new CustomVertex.PositionTextured(new Vector3(-1, -1, -1), 1, 1);
        faces[2] = new HMVertexGroup(verts, inds, textures[2], myDevice);

        // Left Face
        verts = new CustomVertex.PositionTextured[4];
        verts[0] = new CustomVertex.PositionTextured(new Vector3(-1, 1, -1), 0, 0);
        verts[1] = new CustomVertex.PositionTextured(new Vector3(-1, 1, 1), 1, 0);
        verts[2] = new CustomVertex.PositionTextured(new Vector3(-1, -1, -1), 0, 1);
        verts[3] = new CustomVertex.PositionTextured(new Vector3(-1, -1, 1), 1, 1);
        faces[3] = new HMVertexGroup(verts, inds, textures[3], myDevice);

        // Top Face
        verts = new CustomVertex.PositionTextured[4];
        verts[0] = new CustomVertex.PositionTextured(new Vector3(-1, 1, -1), 0, 0);
        verts[1] = new CustomVertex.PositionTextured(new Vector3(1, 1, -1), 1, 0);
        verts[2] = new CustomVertex.PositionTextured(new Vector3(-1, 1, 1), 0, 1);
        verts[3] = new CustomVertex.PositionTextured(new Vector3(1, 1, 1), 1, 1);
        faces[4] = new HMVertexGroup(verts, inds, textures[4], myDevice);

        // Bottom Face
        verts = new CustomVertex.PositionTextured[4];
        verts[0] = new CustomVertex.PositionTextured(new Vector3(-1, -1, 1), 0, 0);
        verts[1] = new CustomVertex.PositionTextured(new Vector3(1, -1, 1), 1, 0);
        verts[2] = new CustomVertex.PositionTextured(new Vector3(-1, -1, -1), 0, 1);
        verts[3] = new CustomVertex.PositionTextured(new Vector3(1, -1, -1), 1, 1);
        faces[5] = new HMVertexGroup(verts, inds, textures[5], myDevice);
    }

    public override void SetScaling(Vector3 newScaling)
    {
        foreach (HMVertexGroup vg in faces)
        {
            vg.SetScaling(newScaling);
        }
    }

    public override void ReloadResources(Device myDevice)
    {
        foreach (HMVertexGroup vg in faces)
        {
            vg.ReloadResources(myDevice);
        }
    }

    public override void Render(Camera myCamera, Device myDevice)
    {
        foreach (HMVertexGroup vg in faces)
        {
            vg.SetPosition(myCamera.Position);
            vg.Render(myCamera, myDevice);
        }
    }
}