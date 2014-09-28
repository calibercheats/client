using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenFX.Core
{
    public static class MathHelper
    {
        public static float RadianToDegree(float angle)
        {
            return angle * (180.0f / (float)Math.PI);
        }

        public static float DegreeToRadian(float angle)
        {
            return (float)Math.PI * angle / 180.0f;
        }
    }
}
