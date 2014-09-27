using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenFX.Core
{
    public sealed class World
    {
        private World() { }

        public static ScriptedFire StartFire(Vector3 position, int unknown1, int unknown2)
        {
            int fire = Function.Call<int>(Natives.START_SCRIPT_FIRE, position.X, position.Y, position.Z, unknown1, unknown2);
            if (fire == 0)
                return null;

            return ObjectCache<ScriptedFire>.Get(fire);
        }

        public static ScriptedFire StartFire(Vector3 position)
        {
            return StartFire(position, 1, 1);
        }

        public static void ExtinguishFire(Vector3 position, float radius)
        {
            Function.Call(Natives.EXTINGUISH_FIRE_AT_POINT, position.X, position.Y, position.Z, radius);
        }

        public static void ExtinguishAllScriptFires()
        {
            Function.Call(Natives.REMOVE_ALL_SCRIPT_FIRES);
        }
    }
}
