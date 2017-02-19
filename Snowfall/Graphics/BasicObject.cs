using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Snowfall.Graphics
{
    public class BasicObject : IDisposable
    {
        public BasicObject(BasicRenderer renderer, Vector3[] vertices, Vector2[] texcoords, int[] indices, Texture2D texture)
        {
            this.renderer = renderer;
            this.vbuffer = new VBO<Vector3>(vertices, false);
            this.tbuffer = new VBO<Vector2>(texcoords, false);
            this.ibuffer = new VBO<int>(indices, true);
            this.texture = texture;
            this.ModelMatrix = Matrix4.Identity;
        }

        public void Render()
        {
            renderer.RenderObject(vbuffer, tbuffer, ibuffer, texture, ModelMatrix);
        }

        public void Dispose()
        {
            vbuffer.Dispose();
            tbuffer.Dispose();
            ibuffer.Dispose();
            texture.Dispose();
        }

        public Matrix4 ModelMatrix { get; set; }
        private VBO<Vector3> vbuffer;
        private VBO<Vector2> tbuffer;
        private VBO<int> ibuffer;
        private Texture2D texture;
        private BasicRenderer renderer;
    }
}
