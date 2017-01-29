using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using Snowfall.Graphics;

namespace Snowfall
{
    /*
     * The main game class
     */
    public class Snowfall
    {
        static Snowfall() //Static constructor for logs
        {
            string logFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Snowfall_Logs"; //Get the log folder
            logFile = logFolder + DateTime.Now.ToFileTime().ToString() + "_snowfall.txt"; //Create path for log
            File.CreateText(logFolder); //Create log file
        }

        public Snowfall() //Game constructor
        {
            Window = new GameWindow(800, 600, GraphicsMode.Default, "Snowfall"); //Create game window
            Window.UpdateFrame += Window_UpdateFrame; //Attach tick event
            Window.Resize += Window_Resize; //Attach resize event
            Window.RenderFrame += Window_RenderFrame; //Attach render event
        }

        private void Window_Resize(object sender, EventArgs e) //Resize event
        {
            Renderer.SetViewport(0, 0, Window.Width, Window.Height); //Change viewport
        }

        private void Window_UpdateFrame(object sender, FrameEventArgs e) //Tick event
        {
        }

        private void Window_RenderFrame(object sender, FrameEventArgs e) //Render event
        {
        }

        public static void Log(string log) //Error log
        {
            File.WriteAllText(logFile, log); //Write to text file
            if (System.Diagnostics.Debugger.IsAttached) //Check if debugger is attached
                System.Diagnostics.Debug.WriteLine(log); //Write to debugger
        }

        public void Start() //The start method
        {
            Window.Run(60); //Run the event loop
        }

        public GameWindow Window { get; private set; } //The variable for the window
        public Renderer Renderer { get; private set; } //The renderer
        private static string logFile; //Path for log file
    }
}
