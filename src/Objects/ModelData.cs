namespace RocnikovaPraca0.Objects;

public class ModelData
{
    public float[] verticies;
    public float[] textureCoord;
    public float[] normals;
    public float[] indices;
    public float furthestPoint;

    public ModelData(float[] verticies, float[] textureCoord, float[] normals, float[] indices, float furthestPoint)
    {
        this.verticies = verticies ?? throw new ArgumentNullException(nameof(verticies));
        this.textureCoord = textureCoord ?? throw new ArgumentNullException(nameof(textureCoord));
        this.normals = normals ?? throw new ArgumentNullException(nameof(normals));
        this.indices = indices ?? throw new ArgumentNullException(nameof(indices));
        this.furthestPoint = furthestPoint;
    }
}