using DefaultNamespace;

namespace Surfaces
{
    public class Fire : Surface
    {
        protected override void EnterSurface(Snowball snowball)
        {
            snowball.TakeDamage(snowball.getCurrentLifetime()/3);
        }
    }
}