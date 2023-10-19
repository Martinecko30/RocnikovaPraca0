using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RocnikovaPraca0.Objects;

namespace RocnikovaPraca0.Core;

public class EngineCore : GameWindow
{
    public EngineCore(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings) {}
    
    // === Temporary variables ===
    private ModelData triangle = new(
        new [] {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
            0.5f, -0.5f, 0.0f, //Bottom-right vertex
            0.0f,  0.5f, 0.0f  //Top vertex
        },
        new [] {
        0f
        },
        new [] {
            0f
        },
        new [] {
            0f
        },
        0
        );
    // ===


    public int VertexBufferObject;

    protected override void OnLoad()
    {
        base.OnLoad();

        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

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

        SwapBuffers();
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
        //_shader.Dispose();
        
        //GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        //GL.DeleteBuffer(_vertexBufferObject);

        base.OnUnload();
    }
}