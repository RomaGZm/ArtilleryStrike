using UnityEngine;

namespace AS.Gameplay.Player.Weapons.Shared.Projectiles
{
    public sealed class Projectile
    {
        public float LifeTime;
        public float MaxLifeTime;

        public int CollisionCount;
        public int MaxCollisionCount;

        public readonly Transform Transform;
        public readonly int Id;

        /// <summary>
        /// Возвращает или задает активность объекта.
        /// </summary>
        public bool Active
        {
            get => Transform.gameObject.activeSelf;
            set => Transform.gameObject.SetActive(value);
        }

        /// <summary>
        /// Возвращает или задает позицию объекта.
        /// </summary>
        public Vector3 Position
        {
            get => Transform.position;
            set => Transform.position = value;
        }
        public Vector3 Velocity;

        public float Gravity;
        public float Bounce;
        public int BounceCount;

        public Projectile(int id, Transform transform)
        {
            Id = id;
            Transform = transform;
        }
    }
}

