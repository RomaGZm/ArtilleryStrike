using UnityEngine;
using UnityEngine.InputSystem;

namespace AS.Gameplay.Core.Input
{

    public sealed class InputService : IInputService
    {
        private readonly InputSystem_Actions _actions;

        public Vector2 Look => _actions.Player.Look.ReadValue<Vector2>();
        public Vector2 Move => _actions.Player.Move.ReadValue<Vector2>();
        public bool FirePressed => _actions.Player.Attack.WasPressedThisFrame();
        public bool ReloadPressed => _actions.Player.Reload.WasPressedThisFrame();
        public bool JumpPressed => _actions.Player.Jump.WasPressedThisFrame();

        public InputService()
        {
            _actions = new InputSystem_Actions();
            _actions.Enable();

        }
    }
}