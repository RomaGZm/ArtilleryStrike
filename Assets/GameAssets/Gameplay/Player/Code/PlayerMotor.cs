using AS.Gameplay.Core.Input;
using AS.Gameplay.Player.Weapons.Shared.Animation;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameAssets.Gameplay.Player.Code
{
    public sealed class PlayerMotor : ITickable, IDisposable
    {
        private readonly PlayerView view;
        private readonly InputService input;
        private readonly PlayerController _controller;
        private readonly MovementConfig movementConfig;
        private WeaponAnimatorBase weaponAnimator;
        public Vector3 velocity;
        public bool isGrounded;
        public bool isMoving => input.Move.x != 0 || input.Move.y != 0;

        public PlayerMotor(PlayerView view, InputService input, WeaponAnimatorBase animator)
        {
            this.view = view;
            this.input = input;
            movementConfig = view.playerConfig.movementConfig;
            weaponAnimator = animator;
        }
        
        public void Tick()
        {
            float dt = Time.deltaTime;

            isGrounded = view.Controller.isGrounded;

            Vector3 wishDir = GetMoveDirection();

            if (isGrounded)
            {
                ApplyFriction(dt);
                Accelerate(wishDir, movementConfig.moveSpeed, movementConfig.acceleration, dt);

                if (input.JumpPressed)
                    velocity.y = movementConfig.jumpForce;
                else if (velocity.y < 0f)
                    velocity.y = -2f;
            }
            else
            {
                Accelerate(wishDir, movementConfig.acceleration, movementConfig.airAcceleration, dt);
                velocity.y += movementConfig.gravity * dt;
            }

            view.Controller.Move(velocity * dt);

            if (isMoving)
            {
                weaponAnimator.Move(true);
            }
            else
            {
                weaponAnimator.Move(false);
            }
        }

        private Vector3 GetMoveDirection()
        {
            Vector2 move = input.Move;
            Vector3 forward = view.CameraRoot.forward;
            Vector3 right = view.CameraRoot.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            return (forward * move.y + right * move.x).normalized;
        }
        /// <summary>
        /// Ускоряет игрока в направлении wishDir с учетом максимальной скорости и ускорения
        /// </summary>
        /// <param name="wishDir"></param>
        /// <param name="maxSpeed"></param>
        /// <param name="accel"></param>
        /// <param name="dt"></param>
        private void Accelerate(Vector3 wishDir, float maxSpeed, float accel, float dt)
        {
            if (wishDir == Vector3.zero)
                return;

            float currentSpeed = Vector3.Dot(velocity, wishDir);
            float addSpeed = maxSpeed - currentSpeed;

            if (addSpeed <= 0f)
                return;

            float accelSpeed = accel * dt * maxSpeed;

            if (accelSpeed > addSpeed)
                accelSpeed = addSpeed;

            velocity += wishDir * accelSpeed;
        }
        /// <summary>
        /// Снижает скорость игрока на основе силы трения и времени, если игрок находится на земле
        /// </summary>
        /// <param name="dt"></param>
        private void ApplyFriction(float dt)
        {
            Vector3 horizontal = velocity;
            horizontal.y = 0f;

            float speed = horizontal.magnitude;

            if (speed < 0.01f)
                return;

            float drop = speed * movementConfig.friction * dt;
            float newSpeed = Mathf.Max(speed - drop, 0f);

            float scale = newSpeed / speed;

            velocity.x *= scale;
            velocity.z *= scale;
        }

        public void Dispose()
        {

        }
    }
}