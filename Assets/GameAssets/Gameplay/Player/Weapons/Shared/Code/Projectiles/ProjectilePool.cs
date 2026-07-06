using AS.Gameplay.Core.Utils;
using AS.Gameplay.Player.Weapons.Shared.Projectiles;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace AS.Gameplay.Player.Weapons.Shared.Projectiles
{

    public sealed class ProjectilePool
    {
        public Projectile[] Items => items;

        private readonly Projectile[] items;
        private readonly Stack<int> freeIndices;

        public ProjectilePool(Transform container, GameObject prefab, int count)
        {
            items = new Projectile[count];
            freeIndices = new Stack<int>(count);

            Mesh mesh = ProceduralCubeMesh.Create(1f, 0.23f);

            for (int i = 0; i < count; i++)
            {
                Transform tr = Object.Instantiate(prefab, container).transform;

                Projectile projectile = new Projectile(i, tr);

                projectile.Active = false;
                tr.GetComponent<MeshFilter>().sharedMesh = mesh;

                items[i] = projectile;
                freeIndices.Push(i);
            }
        }

        /// <summary>
        /// Возвращает свободный снаряд из пула. Если пул пуст, возвращает null.
        /// </summary>
        /// <returns></returns>
        public Projectile Get()
        {
            if (freeIndices.Count == 0)
                return null;

            Projectile projectile = items[freeIndices.Pop()];
            projectile.Active = true;

            return projectile;
        }
        /// <summary>
        /// Возвращает снаряд в пул. После вызова этого метода снаряд становится неактивным и может быть повторно использован.
        /// </summary>
        /// <param name="projectile"></param>
        public void Return(Projectile projectile)
        {

            projectile.Active = false;

            freeIndices.Push(projectile.Id);
        }
    }
}