using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;
namespace AS.Gameplay.Player.Weapons.Shared.VFX
{

    public sealed class ExplosionPool
    {
        public VisualEffect[] Items => items;

        private readonly VisualEffect[] items;
        private readonly Queue<VisualEffect> available = new();

        private readonly int lifeTimeMs;

        public ExplosionPool(Transform container, GameObject prefab, int count, float lifeTime)
        {
            lifeTimeMs = Mathf.RoundToInt(lifeTime * 1000f);

            items = new VisualEffect[count];

            for (int i = 0; i < count; i++)
            {
                VisualEffect vfx = Object.Instantiate(prefab, container).GetComponent<VisualEffect>();

                vfx.gameObject.SetActive(false);

                items[i] = vfx;
                available.Enqueue(vfx);
            }
        }
        /// <summary>
        /// Возвращает доступный объект VisualEffect из пула. Если все объекты заняты, возвращает null. После использования объект автоматически возвращается в пул через заданное время жизни.
        /// </summary>
        /// <returns></returns>
        public VisualEffect Get()
        {
            if (available.Count == 0)
                return null;

            VisualEffect vfx = available.Dequeue();
            vfx.gameObject.SetActive(true);

            ReturnAfterDelay(vfx).Forget();

            return vfx;
        }

        private async UniTaskVoid ReturnAfterDelay(VisualEffect vfx)
        {
            await UniTask.Delay(lifeTimeMs);

            Return(vfx);
        }
        /// <summary>
        /// Возвращает объект VisualEffect обратно в пул.
        /// </summary>
        /// <param name="vfx"></param>
        private void Return(VisualEffect vfx)
        {
            if (vfx == null || !vfx.gameObject.activeSelf)
                return;

            vfx.Stop();
            vfx.gameObject.SetActive(false);

            available.Enqueue(vfx);
        }
    }
}