namespace RocnikovaPraca0.Objects;

public struct ModelData
{
    public float[] Verticies;
    public float[] TextureCoord;
    public float[] Normals;
    public int[] Indices;

    public ModelData(float[] verticies, float[] textureCoord, float[] normals, int[] indices)
    {
        this.Verticies = verticies ?? throw new ArgumentNullException(nameof(verticies));
        this.TextureCoord = textureCoord ?? throw new ArgumentNullException(nameof(textureCoord));
        this.Normals = normals ?? throw new ArgumentNullException(nameof(normals));
        this.Indices = indices ?? throw new ArgumentNullException(nameof(indices));
    }
}