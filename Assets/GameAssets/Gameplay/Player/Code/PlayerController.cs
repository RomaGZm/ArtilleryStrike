
using AS.Gameplay.Core.Input;
using AS.Gameplay.Player.Cameras;
using AS.Gameplay.Player.Weapons.Shared;
using AS.Gameplay.Player.Weapons.Shared.Interfaces;
using GameAssets.Gameplay.Player.Code;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameAssets.Gameplay.Player
{
    public sealed class PlayerController : ITickable, ILateTickable, IFixedTickable, IInitializable, IDisposable
    {
        private InputService inputService;
        private PlayerMotor playerMotor;
        private PlayerCamera playerCamera;
        private GunBase playerGun;


        [Inject]
        public PlayerController(InputService input_, PlayerView playerView)
        {
            inputService = input_;


            playerCamera = new PlayerCamera(playerView, inputService);
            playerGun = playerView.GunView.CreateGun();
            playerMotor = new PlayerMotor(playerView, inputService, playerGun.weaponAnimator);



        }

        public void Initialize()
        {
            playerCamera.Initialize();

        }

        public void LateTick()
        {
            playerCamera.LateTick();
        }

        public void Tick()
        {
            playerMotor.Tick();
            if (inputService.FirePressed)
            {
                if (playerGun is IAttack)
                {
                    ((IAttack)playerGun).Attack();
                }
            }
            if (inputService.ReloadPressed)
            {
                if (playerGun is IReload)
                {
                    ((IReload)playerGun).Reload();
                }
            }
            if (playerGun is ITickable)
            {
                ((ITickable)playerGun).Tick();
            }
            // cameraEffects.Tick(Time.deltaTime, playerMotor.velocity.magnitude, playerMotor.isGrounded);

        }
        public void FixedTick()
        {
            if (playerGun is IFixedTickable)
            {
                ((IFixedTickable)playerGun).FixedTick();
            }
        }

        public void Dispose()
        {

            playerMotor.Dispose();
            playerCamera.Dispose();

        }

    }
}