using UnityEngine;

namespace GameAssets.Gameplay.Player.Code
{

    [System.Serializable]
    public class CameraConfig
    {
        public float sensitivity = 1.5f;
        public float smoothSpeed = 1.0f;

        [Header("Clamp")]
        public float minPitch = -85f;
        public float maxPitch = 85f;
    }
    [System.Serializable]
    public class MovementConfig
    {
        public float moveSpeed = 7f;
        public float acceleration = 20f;
        public float airAcceleration = 8f;
        public float gravity = -20f;
        public float jumpForce = 8f;
        public float friction = 6f;
    }

    [CreateAssetMenu(menuName = "Configs/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        public CameraConfig cameraConfig;
        public MovementConfig movementConfig;

    }
}