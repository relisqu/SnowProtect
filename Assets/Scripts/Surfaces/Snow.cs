using DefaultNamespace;
using UnityEngine;

namespace Surfaces
{
    public class Snow : Surface
    {
        protected override void StayAtSurface(Snowball snowball)
        {
            if (snowball.GetMovementSpeed() > 1f)
            {
                snowball.AddLifetime(0.3f);
            }
        }
    }
}