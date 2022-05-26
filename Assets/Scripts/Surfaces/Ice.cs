using DefaultNamespace;
using UnityEngine;

namespace Surfaces
{
    public class Ice : Surface
    {
        
        [SerializeField]private float SlipperyCoefficient=7f;
        [SerializeField]private float SlipperyBoost=2.48f;

        protected override void EnterSurface(PlayerMovement player)
        {
            player.ChangeMovementDrag(SlipperyCoefficient,SlipperyBoost);
            PlayerMovementAnimator.IsSliding = true;
        }

        protected override void ExitSurface(PlayerMovement player)
        {
            player.ChangeMovementToDefault();
            PlayerMovementAnimator.IsSliding = false;
        }
        protected override void EnterSurface(Snowball snowball)
        {
            snowball.ChangeDrag(0.2f);
        }

        protected override void ExitSurface(Snowball snowball)
        {
            snowball.ChangeDrag(snowball.GetDefaultDrag());
        }
    }
}