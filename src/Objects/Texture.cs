using OpenTK.Graphics.OpenGL;
using StbImageSharp;

namespace RocnikovaPraca0.Objects;

public class Texture
{
    public int textureID;

    public int Width, Height;
    
    public Texture(string filePath)
    {
        textureID = GL.GenTexture();
        
        //GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, textureID);
        
        StbImage.stbi_set_flip_vertically_on_load(1);

        using (Stream stream = File.OpenRead(filePath))
        {
            ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
            
            Width = image.Width;
            Height = image.Height;
            
            GL.TexImage2D(
                TextureTarget.Texture2D, 
                0, 
                PixelInternalFormat.Rgba, 
                image.Width, 
                image.Height, 
                0, 
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                image.Data
                );
        }
        
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
    }

    public void Use(TextureUnit unit = TextureUnit.Texture0)
    {
        GL.ActiveTexture(unit);
        GL.BindTexture(TextureTarget.Texture2D, textureID);
    }
    
    
    
    
    /*
    public static Texture LoadFromFile(string filePath)
    {
        int handle = GL.GenTexture();
        
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, handle);

        StbImage.stbi_set_flip_vertically_on_load(1);

        using (Stream stream = File.OpenRead(filePath))
        {
            ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
            
            GL.TexImage2D(
                TextureTarget.Texture2D, 
                0, 
                PixelInternalFormat.Rgba, 
                image.Width, 
                image.Height, 
                0, 
                PixelFormat.Rgba, 
                PixelType.UnsignedByte, 
                image.Data
            );
        }
        
        GL.TexParameter(
            TextureTarget.Texture2D, 
            TextureParameterName.TextureMinFilter, 
            (int)TextureMinFilter.Linear
        );
        GL.TexParameter(
            TextureTarget.Texture2D, 
            TextureParameterName.TextureMagFilter, 
            (int)TextureMagFilter.Linear
        );
        
        
        GL.TexParameter(
            TextureTarget.Texture2D, 
            TextureParameterName.TextureWrapS, 
            (int)TextureWrapMode.Repeat
        );
        GL.TexParameter(
            TextureTarget.Texture2D, 
            TextureParameterName.TextureWrapT, 
            (int)TextureWrapMode.Repeat
        );
        
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        return new Texture(handle);
    }
    */
}