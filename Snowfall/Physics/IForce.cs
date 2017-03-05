using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Snowfall.Physics
{
    public abstract class IForce<TProperty> where TProperty : class
    {
        public abstract Vector2 CalculateAcceleration(TProperty p);
        public void Exert(ISolidBody body)
        {
            TProperty property = body as TProperty;
            if (property == null) return;
            body.Acceleration += CalculateAcceleration(property);
        }
    }
}
