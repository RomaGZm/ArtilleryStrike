using DG.Tweening;
using UnityEngine;

namespace As.Gameplay.Player.Cameras
{
    public sealed class CameraEffects
    {
        private readonly Transform camera;
        private readonly Vector3 defaultLocalPosition;
        private Tween shakeTween;
        private float walkTime;

        public CameraEffects(Transform camera)
        {
            this.camera = camera;
            defaultLocalPosition = camera.localPosition;
        }

        /// <summary>
        /// ќбновление эффектов камеры при ходьбе 
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <param name="moveSpeed"></param>
        /// <param name="grounded"></param>
        public void Tick(float deltaTime, float moveSpeed, bool grounded)
        {
            Vector3 target = defaultLocalPosition;

            if (grounded && moveSpeed > 0.05f)
            {
                walkTime += deltaTime * 8f * moveSpeed;

                target += new Vector3(Mathf.Cos(walkTime * 0.5f) * 0.015f, Mathf.Abs(Mathf.Sin(walkTime)) * 0.03f, 0f);
            }
            else
            {
                walkTime = 0f;
            }

            if (shakeTween == null || !shakeTween.IsActive())
            {
                camera.localPosition = Vector3.Lerp(
                    camera.localPosition, target, deltaTime * 12f);
            }
        }
        /// <summary>
        /// Ёффект отдачи при выстреле.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="strength"></param>
        public void Shoot(float duration = 0.2f, float strength = 0.08f)
        {
            shakeTween?.Kill();

            camera.localPosition = defaultLocalPosition;

            shakeTween = camera.DOShakePosition(duration, strength, vibrato: 18, randomness: 40, snapping: false, fadeOut: true)
                .OnComplete(() =>
                {
                    camera.localPosition = defaultLocalPosition;
                });
        }
    }
}