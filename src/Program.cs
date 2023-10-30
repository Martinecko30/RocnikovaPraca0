using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using RocnikovaPraca0.Core;

namespace RocnikovaPraca0
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
                Title = "Rocnikova Praca - Martin Valent",

                // This is needed to run on macos
                // Flags = ContextFlags.ForwardCompatible,
            };

            try
            {
                using var window = new EngineCore(GameWindowSettings.Default, nativeWindowSettings);
                window.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}