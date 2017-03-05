using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Snowfall.Physics
{
    public interface ISolidBody
    {
        bool BroadCollision(ISolidBody body);
        bool SimpleCollision(ISolidBody body);
        ICollisionData NarrowCollision(ISolidBody body);
        Vector2 Acceleration { get; set; }
        Vector2 Velocity { get; }
        Vector2 Translation { get; }
    }
}
