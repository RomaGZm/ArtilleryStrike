using System;
using UnityEngine;

namespace AS.Gameplay.Player.Weapons.Shared.Projectiles
{
    public sealed class ProjectilePhysics
    {
        private readonly RaycastHit[] _hits = new RaycastHit[1];

        /// <summary>
        /// Симуляция движения снаряда с учетом столкновений и отражений.
        /// </summary>
        /// <param name="projectile"></param>
        /// <param name="deltaTime"></param>
        /// <param name="radius"></param>
        /// <param name="collisionMask"></param>
        /// <param name="onCollision"></param>
        /// <returns></returns>
        public bool Simulate(Projectile projectile, float deltaTime, float radius, LayerMask collisionMask, Action<Projectile, RaycastHit> onCollision = null)
        {
            Vector3 position = projectile.Position;
            Vector3 velocity = projectile.Velocity;

            velocity.y -= projectile.Gravity * deltaTime;

            float remainingDistance = velocity.magnitude * deltaTime;

            if (remainingDistance <= Mathf.Epsilon)
            {
                projectile.Velocity = velocity;
                return true;
            }

            Vector3 direction = velocity.normalized;

            while (remainingDistance > 0f)
            {
                // Грубая проверка столкновений для определения ближайшего препятствия.
                int hitCount = Physics.SphereCastNonAlloc(position, radius, direction, _hits, remainingDistance, collisionMask, QueryTriggerInteraction.Ignore);
 

                if (hitCount == 0)
                {
                    position += direction * remainingDistance;
                    break;
                }

                RaycastHit hit = _hits[0];

                position = hit.point + hit.normal * (radius + 0.0005f);

                if (hitCount > 0)
                {
                    //Точная проверка столкновения с использованием Raycast для определения точного места столкновения.
                    if (Physics.Raycast(position, direction, out RaycastHit exactHit, hit.distance + radius * 2f, collisionMask, QueryTriggerInteraction.Ignore))
                    {
                        hit = exactHit;
                    }

                    onCollision?.Invoke(projectile, hit);
                }
               // onCollision?.Invoke(projectile, hit);

                if (projectile.BounceCount <= 0)
                    return false;

                projectile.BounceCount--;

                velocity = Vector3.Reflect(velocity, hit.normal) * projectile.Bounce;

                if (velocity.sqrMagnitude < 0.01f)
                    return false;

                remainingDistance -= hit.distance;

                direction = velocity.normalized;
            }

            projectile.Position = position;
            projectile.Velocity = velocity;

            return true;
        }

#if UNITY_EDITOR

        public static void DrawGizmos(
            Projectile projectile,
            float radius,
            Color color)
        {
            Gizmos.color = color;

            Gizmos.DrawWireSphere(projectile.Position, radius);

            Gizmos.DrawLine(
                projectile.Position,
                projectile.Position + projectile.Velocity.normalized);
        }

#endif
    }
}