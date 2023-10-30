namespace RocnikovaPraca0.Objects;

public struct RawModel
{
    public int VAOID;
    public int VerticiesCount;

    public RawModel(int vaoID, int verticiesCount)
    {
        this.VAOID = vaoID;
        this.VerticiesCount = verticiesCount;
    }
}