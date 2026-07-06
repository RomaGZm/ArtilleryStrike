using AS.Gameplay.Player.Weapons.Shared;
using UnityEngine;

namespace GameAssets.Gameplay.Player.Code
{

    public sealed class PlayerView : MonoBehaviour
    {

        [Header("Refs")]
        [SerializeField] private CharacterController controller;
        [SerializeField] private Transform cameraRoot;

        [SerializeField] private Camera worldCamera;
        [SerializeField] private Camera weaponCamera;
        [SerializeField] private Transform playerBody;
        [SerializeField] private GunViewBase gunView;

        public bool freezeCam = false;

        [Header("Configs")]
        public PlayerConfig playerConfig;

        public CharacterController Controller => controller;
        public Transform CameraRoot => cameraRoot;
        public Transform PlayerBody => playerBody;
        public Camera WorldCamera => worldCamera;
        public GunViewBase GunView => gunView;
        public Camera WeaponCamera => weaponCamera;

    }
}