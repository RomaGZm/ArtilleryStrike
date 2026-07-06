
using AS.Gameplay.Core.Input;
using GameAssets.Gameplay.Player.Code;
using System;
using UnityEngine;
using VContainer.Unity;

namespace AS.Gameplay.Player.Cameras
{

    public sealed class PlayerCamera : ILateTickable, IInitializable, IDisposable
    {
        private readonly PlayerView view;
        private readonly InputService input;
        private readonly CameraConfig config;
        private Camera worldCamera;

        private float pitch;
        private float yaw;

        private float currentPitch;
        private float currentYaw;

        public bool freeze = false;

        public PlayerCamera(PlayerView view, InputService input)
        {

            this.view = view;
            this.input = input;
            config = view.playerConfig.cameraConfig;
            worldCamera = view.WorldCamera;
        }
        /// <summary>
        /// Инициализация камеры игрока.
        /// </summary>
        public void Initialize()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            yaw = view.PlayerBody.eulerAngles.y;
            pitch = 0;

            currentYaw = yaw;
            currentPitch = pitch;
            freeze = view.freezeCam;
        }
        /// <summary>
        /// Обновление камеры игрока каждый кадр.
        /// </summary>
        public void LateTick()
        {
            if (freeze) return;

            Vector2 look = input.Look * config.sensitivity * Time.deltaTime;

            yaw += look.x;
            pitch -= look.y;

            pitch = Mathf.Clamp(pitch, config.minPitch, config.maxPitch);

            currentYaw = Mathf.Lerp(currentYaw, yaw, config.smoothSpeed * Time.deltaTime);
            currentPitch = Mathf.Lerp(currentPitch, pitch, config.smoothSpeed * Time.deltaTime);

            view.PlayerBody.rotation = Quaternion.Euler(0f, currentYaw, 0f);

            view.CameraRoot.localRotation = Quaternion.Euler(currentPitch, 0f, 0f);
            worldCamera.transform.SetPositionAndRotation(view.CameraRoot.position, view.CameraRoot.rotation);
        }

        /// <summary>
        /// Добавление отдачи к камере игрока.
        /// </summary>
        /// <param name="vertical"></param>
        /// <param name="horizontal"></param>
        public void AddRecoil(float vertical, float horizontal)
        {
            pitch -= vertical;
            yaw += UnityEngine.Random.Range(-horizontal, horizontal);
        }

        public void Dispose()
        {

        }

    }
}