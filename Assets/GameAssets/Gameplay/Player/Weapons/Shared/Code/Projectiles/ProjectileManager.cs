using AS.Gameplay.Player.Weapons.Shared.VFX;
using UnityEngine;
using UnityEngine.VFX;

namespace AS.Gameplay.Player.Weapons.Shared.Projectiles
{
    public sealed class ProjectileManager
    {
        private readonly ProjectilePool projectilePool;
        private readonly ExplosionPool explosionPool;
        private readonly ProjectilePhysics physics;
        private readonly ProjectileHolePainter projectileHolePainter;
        private readonly LayerMask collisionMask;
        private readonly float radius;
        private RPGData rPGData;

        public ProjectileManager(ProjectilePool projectilePool, ExplosionPool explosionPool, LayerMask collisionMask, float radius, RPGData rPGData)
        {
            this.projectilePool = projectilePool;
            this.explosionPool = explosionPool;
            this.collisionMask = collisionMask;
            this.radius = radius;
            this.rPGData = rPGData;

            physics = new ProjectilePhysics();
            projectileHolePainter = new ProjectileHolePainter();
        }

        /// <summary>
        /// Создает и возвращает новый снаряд с заданными параметрами.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        /// <param name="speed"></param>
        /// <param name="gravity"></param>
        /// <param name="bounce"></param>
        /// <param name="bounceCount"></param>
        /// <returns></returns>
        public Projectile Spawn(Vector3 position, Vector3 direction, float speed, float gravity = 9.81f,  float bounce = 0.6f, int bounceCount = 2)
        {
            Projectile projectile = projectilePool.Get();

            projectile.Position = position;
            projectile.Velocity = direction.normalized * speed;

            projectile.Gravity = gravity;
            projectile.Bounce = bounce;
            projectile.LifeTime = 0f;
            projectile.MaxLifeTime = 3f;

            projectile.CollisionCount = 0;
            projectile.MaxCollisionCount = 2;
            projectile.BounceCount = bounceCount;

            projectile.Active = true;

            return projectile;
        }

        /// <summary>
        /// Симулирует движение всех активных снарядов, обновляя их позиции и проверяя столкновения.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Simulate(float deltaTime)
        {
            Projectile[] projectiles = projectilePool.Items;

            for (int i = 0; i < projectiles.Length; i++)
            {
                Projectile projectile = projectiles[i];

                if (!projectile.Active)
                    continue;

                projectile.LifeTime += deltaTime;

                if (projectile.LifeTime >= projectile.MaxLifeTime)
                {
                    OnDestroy(projectile, default);
                    
                    continue;
                }

                if (!physics.Simulate(projectile, deltaTime, radius, collisionMask, OnCollision))
                {
                    projectilePool.Return(projectile);
                    OnDestroy(projectile, default);
                }
            }
        }

        private void OnDestroy(Projectile projectile, RaycastHit hit)
        {
            projectilePool.Return(projectile);
            PlayVFX(projectile.Position);
          
        }

        private void PlayVFX(Vector3 pos)
        {
            VisualEffect explosion = explosionPool.Get();
            explosion.transform.position = pos;

            explosion.Play();
        }

        private void OnCollision(Projectile projectile, RaycastHit hit)
        {

            projectileHolePainter.HolePaint(hit);

        }
    }
}