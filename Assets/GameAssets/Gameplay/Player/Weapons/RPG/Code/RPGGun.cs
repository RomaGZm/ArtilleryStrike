
using As.Gameplay.Player.Cameras;
using AS.Gameplay.Player.Weapons.RPG.Animation;
using AS.Gameplay.Player.Weapons.Shared;
using AS.Gameplay.Player.Weapons.Shared.Code.Projectiles;
using AS.Gameplay.Player.Weapons.Shared.Interfaces;
using AS.Gameplay.Player.Weapons.Shared.Projectiles;
using AS.Gameplay.Player.Weapons.Shared.VFX;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;
using VContainer.Unity;

namespace AS.Gameplay.Player.Weapons.RPG
{

    [System.Serializable]
    public sealed class RPGGun : GunBase, IAttack, IFixedTickable, ITickable
    {

        private RPGData rpgData;
        private RPGGunView rpgGunView;
        public RPGAnimator rpgAnimator;

        private bool isReloading;
        private ProjectilePool projectilePool;
        private ExplosionPool explosionPool;
        private ProjectileManager projectileManager;
        private TrajectoryPredictor trajectoryPredictor;
        private CameraEffects cameraEffects;

        public Action OnAttack { get; set; }

        public RPGGun(GunViewBase view) : base(view)
        {
            weaponAnimator = new RPGAnimator(view.animator);
            rpgAnimator = (RPGAnimator)weaponAnimator;
            rpgGunView = (RPGGunView)view;
            rpgData = (RPGData)view.config.data;

            projectilePool = new ProjectilePool(rpgGunView.projectileContainer, rpgData.projectilePrefab, 10);
            explosionPool = new ExplosionPool(rpgGunView.projectileContainer, rpgData.explosionPrefab, 10, 2);
            projectileManager = new ProjectileManager(projectilePool, explosionPool, rpgData.collisionMask, rpgData.projectileRadius, rpgData);
            trajectoryPredictor = new TrajectoryPredictor(rpgGunView.trajectoryLine, rpgData.trajectorylineSize);
            cameraEffects = new CameraEffects(rpgGunView.cameraRoot);
        }

        public void Attack()
        {
            TryAttack().Forget();
        }

        /// <summary>
        /// Атакует, если оружие не перезаряжается.
        /// </summary>
        /// <returns></returns>
        public async UniTask TryAttack()
        {
            if (isReloading) return;

            isReloading = true;

            cameraEffects.Shoot(0.5f);
            rpgAnimator.Attack1();
            OnAttack?.Invoke();

            Projectile projectile = projectilePool.Get();
            projectileManager.Spawn(rpgGunView.firePoint.position, rpgGunView.transform.forward, rpgData.projectileSpeed, rpgData.projectileGravity, 0.6f, 1);

            await UniTask.Delay(TimeSpan.FromSeconds(rpgData.fireRate));
            rpgAnimator.Reload();
            await UniTask.Delay(TimeSpan.FromSeconds(rpgData.reloadTime));

            isReloading = false;
        }

        public void Tick()
        {
            trajectoryPredictor.Draw(rpgGunView.firePoint.position, rpgGunView.firePoint.forward, rpgData.projectileSpeed, rpgData.projectileGravity, 64, 0.05f);
        }

        public void FixedTick()
        {
            projectileManager.Simulate(Time.fixedDeltaTime);

        }
    }
}