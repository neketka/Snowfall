using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using WindowInfo = OpenTK.Platform.IWindowInfo;

namespace Snowfall.Graphics
{
    /*
     * Renderer class
     */
    public class Renderer : IDisposable
    {
        public Renderer(GameWindow window) //Main renderer constructor
        {
            GL.Enable(EnableCap.Texture2D);
            this.window = window;
            window.Resize += Window_Resize;
            this.context = window.Context; //Set the GL context
            this.windowinfo = window.WindowInfo; //Set the window description
            this.BasicRenderer = new BasicRenderer(this);
            Viewer = new BasicViewer(window.Width, window.Height);
        }

        private void Window_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, window.Width, window.Height);
            Viewer.UpdateProjection(window.Width, window.Height);
        }

        public void UpdateWindow(int x, int y, int w, int h)
        {
            GL.Viewport(x, y, w, h);
            Viewer.UpdateProjection(w, h);
        }

        private void CheckContextCurrent() //Checks if the context is current and makes current if not
        {
            if (!context.IsCurrent) context.MakeCurrent(windowinfo);
        }

        public void ClearColor() //Clears the color buffer
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        public void ClearStencil() //Clears the stencil buffer
        {
            GL.Clear(ClearBufferMask.StencilBufferBit);
        }

        public void Dispose()
        {
            BasicRenderer.Dispose();
        }

        public void OnRender()
        {
            ClearColor();
            Render(this, new EventArgs());
            window.SwapBuffers();
        }

        public event EventHandler Render = (o, s) => { };
        
        public BasicViewer Viewer { get; set; }//Viewer
        public BasicRenderer BasicRenderer { get; private set; }
        private IGraphicsContext context; //OpenGL context
        private WindowInfo windowinfo; //Window descriptor
        private GameWindow window; //Window
    }
}
