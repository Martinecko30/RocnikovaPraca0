using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RocnikovaPraca0.Objects;
using RocnikovaPraca0.Shaders;

namespace RocnikovaPraca0.Core;

public class EngineCore : GameWindow
{
    public EngineCore(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings) {}
    
    // === Temporary variables ===
    private ModelData triangle = new(
        new [] {
            0.5f,  0.5f, 0.0f,  // top right
            0.5f, -0.5f, 0.0f,  // bottom right
            -0.5f, -0.5f, 0.0f,  // bottom left
            -0.5f,  0.5f, 0.0f   // top left
        },
        new [] {
        0f
        },
        new [] {
            0f
        },
        new [] {
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        },
        0
        );
    // ===

    private Shader vertexShader, fragShader;

    public int VertexBufferObject;
    public int VertexArrayObject;
    public int ElementBufferObject;
    
    

    protected override void OnLoad()
    {
        base.OnLoad();

        // Configure VBO + VAO
        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);
        
        GL.BufferData(
            BufferTarget.ArrayBuffer, 
            triangle.verticies.Length * sizeof(float), 
            triangle.verticies, 
            BufferUsageHint.StaticDraw
        );
        GL.VertexAttribPointer(
            0, 
            3, 
            VertexAttribPointerType.Float, 
            false, 
            3 * sizeof(float), 
            0);
        GL.EnableVertexAttribArray(0);

        // Configure EBO
        ElementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
        GL.BufferData(
            BufferTarget.ElementArrayBuffer,
            triangle.indices.Length * sizeof(uint),
            triangle.indices,
            BufferUsageHint.StaticDraw
            );
        
        // Configure Shaders
        vertexShader = new Shader("res\\Shaders\\shader.vert", ShaderType.VertexShader);
        fragShader = new Shader("res\\Shaders\\shader.frag", ShaderType.FragmentShader);
        
        GL.ClearColor(0.1f, 0.1f, 0.7f, 1.0f);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        
        base.OnRenderFrame(args);
        
        if (!IsFocused)
        {
            return;
        }
        
        GL.Clear(ClearBufferMask.ColorBufferBit);
        
        // Code
        UseShaders();
        GL.BindVertexArray(VertexArrayObject);
        GL.DrawElements(
            PrimitiveType.Triangles, 
            triangle.indices.Length, 
            DrawElementsType.UnsignedInt, 
            0
            );

        SwapBuffers();
    }

    // TODO: Array of shaders
    private void UseShaders()
    {
        vertexShader.Use();
        fragShader.Use();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        
        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }
    }
    
    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
    }
    
    protected override void OnUnload()
    {
        vertexShader.Dispose();
        fragShader.Dispose();
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        //GL.DeleteBuffer(_vertexBufferObject);

        base.OnUnload();
    }
}