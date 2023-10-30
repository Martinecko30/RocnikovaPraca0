using System.Diagnostics;
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
    private ModelData modelData = new(
        new [] {    
             0.5f,  0.5f, 0.0f,
             0.5f, -0.5f, 0.0f,
            -0.5f, -0.5f, 0.0f,
            -0.5f,  0.5f, 0.0f
        },
        new [] {
            1.0f, 1.0f,
            1.0f, 0.0f,
            0.0f, 0.0f,
            0.0f, 1.0f
        },
        new [] {    // Colors for this example
            1.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f,
            0.0f, 0.0f, 1.0f
        },
        new [] {
            0, 1, 3,
            1, 2, 3
        }
        );

    // ===

    private Loader loader = new Loader();
    private Stopwatch timer;

    private List<Shader> shaders = new();
    private Shader shader;

    private List<TexturedModel> texturedModels = new();
    
    

    protected override void OnLoad()
    {
        base.OnLoad();
        
        // Configure Shaders
        shader = new Shader("res\\Shaders\\shader.vert", "res\\Shaders\\shader.frag");
        
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

        texturedModels.Add(
            new(
                    loader.LoadToVAO(modelData),
                    new List<Texture>()
                    {
                        new("res\\Textures\\wall.jpg"),
                        new("res\\Textures\\awesomeface.png")
                    },
                    shader.Handle
                )
            );
        
        shader.SetInt("texture1", 0);
        shader.SetInt("texture2", 1);
        
        timer = new Stopwatch();
        timer.Start();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        
        base.OnRenderFrame(args);
        
        if (!IsFocused)
        {
            return;
        }
        
        GL.Clear(ClearBufferMask.ColorBufferBit);

        Dictionary<int, TexturedModel> shaderBatch = new();
        
        foreach (var model in texturedModels)
        {
            if(model.ShaderReference == shader.Handle)
                
            
            GL.BindVertexArray(model.RawModel.VAOID);

            for (int i = 0; i < model.Textures.Count; i++)
                model.Textures[i].Use((TextureUnit)((int)All.Texture0 + i));
        }
        
        shader.Use();
        
        // Code
        GL.DrawElements(
            PrimitiveType.Triangles,
            modelData.Indices.Length, 
            DrawElementsType.UnsignedInt,
            0
        );

        base.SwapBuffers();
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
        shader.Dispose();
        
        loader.CleanUp();
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        base.OnUnload();
    }
}