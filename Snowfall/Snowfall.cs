using System;
using System.IO;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using Snowfall.Graphics;
using Snowfall.Input;

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
            logFile = logFolder + "\\" + DateTime.Now.ToFileTime().ToString() + "_snowfall.txt"; //Create path for log
            if (!Directory.Exists(logFolder)) Directory.CreateDirectory(logFolder);
            File.Create(logFile).Close(); //Create log file
        }

        public Snowfall() //Game constructor
        {
            Window = new GameWindow(800, 600, GraphicsMode.Default, "Snowfall"); //Create game window
            Window.UpdateFrame += Window_UpdateFrame; //Attach tick event
            Window.RenderFrame += Window_RenderFrame; //Attach render event
            Window.Disposed += Window_Disposed;
            Renderer = new Renderer(Window);
            Keyboard = new Keyboard(Window);
            Mouse = new Mouse(Window);
            BasicRenderObject obj = new BasicRenderObject(Renderer.BasicRenderer, 
                new Vector3[] { new Vector3(-0.5f, 0.5f, -1f), new Vector3(0.5f, 0.5f, -1f), new Vector3(-0.5f, -0.5f, -1f), new Vector3(0.5f, -0.5f, -1f) }, 
                new Vector2[] { new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0) }, 
                new int[] { 0, 1, 2, 1, 2, 3 }, 
                new Texture2D(new Bitmap(Bitmap.FromFile(@"D:\Nikita\Pictures\Arya Lol.png"))));
            float dir = 0;
            Tick += (o, e) => { obj.ModelMatrix = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(dir)); dir++; };
            Renderer.Render += (o, e) => {
                obj.Render(FBO.DeviceFramebuffer);
            };
            Window.Disposed += (o, e) => { obj.Dispose(); };
        }

        private void Window_Disposed(object sender, EventArgs e)
        {
            Renderer.Dispose();
        }

        private void Window_UpdateFrame(object sender, FrameEventArgs e) //Tick event
        {
            Tick(this, (float)e.Time); //Tick all events
        }

        private void Window_RenderFrame(object sender, FrameEventArgs e) //Render event
        {
            Renderer.OnRender();
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
        public event EventHandler<float> Tick = (s, t) => { };

        public Keyboard Keyboard { get; private set; } //Keyboard device state
        public Mouse Mouse { get; private set; } //Mouse device state
        public GameWindow Window { get; private set; } //The variable for the window
        public Renderer Renderer { get; private set; } //The renderer
        private static string logFile; //Path for log file
    }
}
