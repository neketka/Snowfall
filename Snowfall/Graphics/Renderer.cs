using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using WindowInfo = OpenTK.Platform.IWindowInfo;

namespace Snowfall.Graphics
{
    /*
     * Renderer
     */
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
            GL.Viewport(x, y, w, h);
        }

        public void RenderTexturedObject(VBO<Vector2> vertices, VBO<Vector2> texcoords, VBO<int> indices, Texture t)
        {
            GL.EnableVertexAttribArray(1);
            vertices.Bind();
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);

            GL.EnableVertexAttribArray(2);
            texcoords.Bind();
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 0, 0);

            indices.Bind();
            t.Bind();
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedByte, IntPtr.Zero);
        }

        public void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            m_context.SwapBuffers();
        }

        private GraphicsContext m_context;
        private WindowInfo m_windowinfo;
    }
}
