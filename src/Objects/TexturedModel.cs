using RocnikovaPraca0.Shaders;

namespace RocnikovaPraca0.Objects;

public struct TexturedModel
{
    public RawModel RawModel;
    public List<Texture> Textures;
    public int ShaderReference;

    public TexturedModel(RawModel rawModel, List<Texture> textures, int shaderReference)
    {
        this.RawModel = rawModel;
        this.Textures = textures;
        this.ShaderReference = ShaderReference;
    }
}