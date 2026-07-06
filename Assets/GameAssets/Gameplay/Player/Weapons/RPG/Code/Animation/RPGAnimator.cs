using AS.Gameplay.Player.Weapons.Shared.Animation;
using UnityEngine;

namespace AS.Gameplay.Player.Weapons.RPG.Animation
{
    public class RPGAnimator : WeaponAnimatorBase
    {

        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int JumpHash = Animator.StringToHash("Jump");
        private static readonly int GroundedHash = Animator.StringToHash("Grounded");
        private static readonly int Attack1Hash = Animator.StringToHash("Attack1");
        private static readonly int ReloadHash = Animator.StringToHash("Reload");

        public RPGAnimator(Animator animator) : base(animator)
        {

        }

        public virtual void SetGrounded(bool value)
        {
            animator.SetBool(GroundedHash, value);
        }

        public virtual void TriggerJump()
        {
            animator.SetTrigger(JumpHash);
        }

        public virtual void Attack1()
        {
            animator.SetTrigger(Attack1Hash);
        }
        public virtual void Reload()
        {
            animator.SetTrigger(ReloadHash);
        }

    }
}