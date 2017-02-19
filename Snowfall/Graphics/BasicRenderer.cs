using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Snowfall.Graphics
{
    public class BasicRenderer : IDisposable
    {
        public BasicRenderer(Renderer renderer) 
        {
            program = new ShaderProgram(Properties.Resources.basicvs, Properties.Resources.basicfs);
            this.renderer = renderer;
        }

        public void RenderObject(VBO<Vector3> vertices, VBO<Vector2> texcoords, VBO<int> indices, Texture2D tex, Matrix4 modelview)
        {
            BasicViewer viewer = renderer.Viewer;
            ShaderPass pass = new ShaderPass(program, PrimitiveType.Triangles);
            pass.SetUniform("modelmatrix", modelview);
            pass.SetUniform("viewmatrix", viewer.ViewMatrix);
            pass.SetUniform("projmatrix", viewer.ProjectionMatrix);
            pass.SetUniform("tex", tex);
            pass.SetAttributeVBO("vert", vertices);
            pass.SetAttributeVBO("texcoord", texcoords);
            pass.SetIndexBuffer(indices);
            pass.RunPass();
        }

        public void Dispose()
        {
            program.Dispose();
        }

        private Renderer renderer;
        private ShaderProgram program;
    }
}
