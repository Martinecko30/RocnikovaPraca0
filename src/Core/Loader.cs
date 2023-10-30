using OpenTK.Graphics.OpenGL;
using RocnikovaPraca0.Objects;

namespace RocnikovaPraca0.Core;

public class Loader
{
    private List<int> VAOs = new();
    private List<int> EBOs = new();
    private List<int> VBOs = new();
    private List<int> textures = new();

    public RawModel LoadToVAO(float[] positions, float[] normals, float[] textureCoords, int[] indices)
    {
        int vaoID = CreateVAO();
        
        BindIndicesBuffer(indices);
        
        /*
        |             Vertex 1            |
        | X | Y | Z || R | G | B || S | T |
        */
        
        StoreDataInAttributeList(0, 3, positions);
        StoreDataInAttributeList(1, 3, normals);
        StoreDataInAttributeList(2, 2, textureCoords);
        
        return new RawModel(vaoID, indices.Length);
    }

    public RawModel LoadToVAO(ModelData data)
    {
        return LoadToVAO(data.Verticies, data.Normals, data.TextureCoord, data.Indices);
    }

    private int CreateVAO()
    {
        int vaoID = GL.GenVertexArray();
        VAOs.Add(vaoID);
        GL.BindVertexArray(vaoID);
        return vaoID;
    }

    private void StoreDataInAttributeList(int attributeNumber, int coordinateSize, float[] data)
    {
        int vboID = GL.GenBuffer();
        VBOs.Add(vboID);
        GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
        
        GL.BufferData(
            BufferTarget.ArrayBuffer, 
            data.Length * sizeof(float),
            data,
            BufferUsageHint.StaticDraw
            );
        
        GL.VertexAttribPointer(
            attributeNumber,
            coordinateSize,
            VertexAttribPointerType.Float,
            false,
            0, 
            0
            );
        
        
        GL.EnableVertexAttribArray(attributeNumber);
    }

    private void BindIndicesBuffer(int[] indices)
    {
        int eboID = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboID);
        EBOs.Add(eboID);
        GL.BufferData(
            BufferTarget.ElementArrayBuffer, 
            indices.Length * sizeof(uint), 
            indices,
            BufferUsageHint.StaticDraw
            );
    }
    
    public void CleanUp() {
        foreach(int vao in VAOs) {
            GL.DeleteVertexArray(vao);
        }
        foreach(int vbo in VBOs) {
            GL.DeleteBuffer(vbo);
        }
        foreach(int ebo in EBOs) {
            GL.DeleteBuffer(ebo);
        }
        foreach(int texture in textures) {
            GL.DeleteTexture(texture);
        }
    }
}