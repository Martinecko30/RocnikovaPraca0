using OpenTK.Graphics.OpenGL;

namespace RocnikovaPraca0.Shaders;

public class Shader : IDisposable
{
    public int Handle;
    private int shaderHandle;

    public Shader(string shaderPath, ShaderType type)
    {
        string shaderText = File.ReadAllText(shaderPath);
        
        shaderHandle = GL.CreateShader(type);
        GL.ShaderSource(shaderHandle, shaderText);
        
        CompileShader(shaderHandle);

        CreateProgram();
        
        GL.DetachShader(Handle, shaderHandle);
        GL.DeleteShader(shaderHandle);
    }

    private void CompileShader(int shader)
    {
        GL.CompileShader(shader);
        
        GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);
        if (success == 0)
        {
            var infoLog = GL.GetShaderInfoLog(shader);
            throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
        }
    }

    private void CreateProgram()
    {
        Handle = GL.CreateProgram();
        
        GL.AttachShader(Handle, shaderHandle);
        GL.LinkProgram(Handle);
        
        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success);
        if (success == 0)
        {
            string infoLog = GL.GetProgramInfoLog(Handle);
            throw new Exception($"Error occurred whilst creating Program {Handle}.\n\n{infoLog}");
        }
    }
    
    public void Use()
    {
        GL.UseProgram(Handle);
    }
    
    public int GetAttribLocation(string attribName)
    {
        return GL.GetAttribLocation(Handle, attribName);
    }
    
    
    
    private bool disposedValue = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            GL.DeleteProgram(Handle);

            disposedValue = true;
        }
    }

    ~Shader()
    {
        if (disposedValue == false)
        {
            Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
        }
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}