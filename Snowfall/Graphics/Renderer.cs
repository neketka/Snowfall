using System;
using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using WindowInfo = OpenTK.Platform.IWindowInfo;

namespace Snowfall.Graphics
{
    public class Renderer
    {
        public Renderer(GraphicsContext context, WindowInfo winfo)
        {
            this.m_context = context;
            this.m_windowinfo = winfo;
        }

        private void CheckContextCurrent()
        {
            if (!m_context.IsCurrent) m_context.MakeCurrent(m_windowinfo);
        }
        
        public void SetViewport(int x, int y, int w, int h)
        {
            CheckContextCurrent();
            GL.Viewport(x, y, w, h);
        }

        public void Render()
        {
            CheckContextCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit);
            m_context.SwapBuffers();
        }

        private GraphicsContext m_context;
        private WindowInfo m_windowinfo;
    }
}
