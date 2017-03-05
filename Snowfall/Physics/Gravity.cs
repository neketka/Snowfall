using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Snowfall.Physics
{
    public class Gravity : IForce<IGravityData>
    {
        private float gravConstant;
        public Gravity(float constant)
        {
            gravConstant = constant;
        }

        public override Vector2 CalculateAcceleration(IGravityData p)
        {
            return new Vector2(0, gravConstant);
        }
    }
}
