using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Snowfall.Graphics
{
    public class BasicViewer
    {
        public BasicViewer(int width, int height)
        {
            float asp = (float)width / height;
            ProjectionMatrix = Matrix4.CreateOrthographicOffCenter(-asp, asp, -1, 1, 0.3f, 2f);
            Position = new Vector2(0, 0);
            Scale = 1;
            Direction = 0;
        }

        public void UpdateProjection(int width, int height)
        {
            float asp = (float)width / height;
            ProjectionMatrix = Matrix4.CreateOrthographicOffCenter(-asp, asp, -1, 1, 0.3f, 2f);
        }

        public Vector2 Position { get; set; }
        public float Scale { get; set; }
        public float Direction { get; set; }

        public Matrix4 ProjectionMatrix { get; private set; }
        public Matrix4 ViewMatrix
        {
            get
            {
                return Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Direction))
                    * Matrix4.CreateTranslation(Position.X, Position.Y, 0)
                    * Matrix4.CreateScale(Scale, Scale, 1);
            }
        }
    }
}
