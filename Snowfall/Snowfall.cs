using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using Snowfall.Graphics;

namespace Snowfall
{
    public class Snowfall
    {
        static Snowfall()
        {
            string logFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Snowfall_Logs";
            logFile = logFolder + DateTime.Now.ToFileTime().ToString() + "_snowfall.txt";
            File.CreateText(logFolder);
        }

        public Snowfall()
        {
            Window = new GameWindow(800, 600, GraphicsMode.Default, "Snowfall");
            Window.UpdateFrame += Window_UpdateFrame;
            Window.Resize += Window_Resize;
            Window.RenderFrame += Window_RenderFrame;
        }

        private void Window_Resize(object sender, EventArgs e)
        {
            Renderer.SetViewport(0, 0, Window.Width, Window.Height);
        }

        private void Window_UpdateFrame(object sender, FrameEventArgs e)
        {
        }

        private void Window_RenderFrame(object sender, FrameEventArgs e)
        {
        }

        public static void Log(string log)
        {
            File.WriteAllText(logFile, log);
        }

        public void Start()
        {
            Window.Run(60);
        }

        public GameWindow Window { get; private set; }
        public Renderer Renderer { get; private set; }
        private static string logFile;
    }
}
