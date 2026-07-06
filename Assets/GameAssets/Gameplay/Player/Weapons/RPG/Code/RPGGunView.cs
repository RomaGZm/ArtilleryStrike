using AS.Gameplay.Player.Weapons.Shared;
using UnityEngine;

namespace AS.Gameplay.Player.Weapons.RPG
{
    public class RPGGunView : GunViewBase
    {

        public Transform projectileContainer;
        public Transform firePoint;
        public LineRenderer trajectoryLine;
        public Transform cameraRoot;

        public override GunBase CreateGun()
        {

            return new RPGGun(this);
        }
    }
}