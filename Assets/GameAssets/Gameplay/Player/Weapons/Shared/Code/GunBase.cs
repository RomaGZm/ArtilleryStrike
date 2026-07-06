
using AS.Gameplay.Player.Weapons.Shared.Animation;

namespace AS.Gameplay.Player.Weapons.Shared
{
    public abstract class GunBase
    {
        public GunViewBase View { get; }
        protected GunConfig Config { get; }
        public WeaponAnimatorBase weaponAnimator;
        protected GunBase(GunViewBase view)
        {
            View = view;
            Config = view.config;
        }

        public virtual void Equip()
        {
            View.gameObject.SetActive(true);
        }

        public virtual void Unequip()
        {
            View.gameObject.SetActive(false);
        }
    }
}
