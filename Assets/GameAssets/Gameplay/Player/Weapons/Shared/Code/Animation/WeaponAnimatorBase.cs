using UnityEngine;

namespace AS.Gameplay.Player.Weapons.Shared.Animation
{

    public class WeaponAnimatorBase
    {
        private static readonly int MoveHash = Animator.StringToHash("Move");
        protected readonly Animator animator;


        public WeaponAnimatorBase(Animator animator)
        {
            this.animator = animator;
        }

        public virtual void Move(bool isMove)
        {
            animator.SetBool(MoveHash, isMove);

        }

    }
}