using UnityEngine;

namespace AS.Gameplay.Core.Input
{
    public interface IInputService
    {
        public Vector2 Look { get; }
        public bool FirePressed { get; }
    }
}