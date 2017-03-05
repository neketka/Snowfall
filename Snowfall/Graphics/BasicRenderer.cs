using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Snowfall.Graphics
{
    public class BasicRenderer : IDisposable
    {
        public BasicRenderer(Renderer renderer) 
        {
            Program = new ShaderProgram(Properties.Resources.basicvs, Properties.Resources.basicfs);
            Program.SetAttribLocation("fragment", 0);
            this.renderer = renderer;
        }

        public ShaderPass RenderObject(VBO<Vector3> vertices, VBO<Vector2> texcoords, VBO<int> indices, Texture2D tex, Matrix4 modelview)
        {
            BasicViewer viewer = renderer.Viewer;
            ShaderPass pass = new ShaderPass(Program, PrimitiveType.Triangles);
            pass.SetUniform("modelmatrix", modelview);
            pass.SetUniform("viewmatrix", viewer.ViewMatrix);
            pass.SetUniform("projmatrix", viewer.ProjectionMatrix);
            pass.SetUniform("tex", tex);
            pass.SetAttributeVBO("vert", vertices);
            pass.SetAttributeVBO("texcoord", texcoords);
            pass.SetIndexBuffer(indices);
            return pass;
        }

        public void Dispose()
        {
            Program.Dispose();
        }

        private Renderer renderer;
        public ShaderProgram Program { get; private set; }
    }
}
