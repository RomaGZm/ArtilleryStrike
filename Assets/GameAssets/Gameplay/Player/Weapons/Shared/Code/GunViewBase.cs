using System;
using UnityEngine;

namespace AS.Gameplay.Player.Weapons.Shared
{
    public abstract class GunViewBase : MonoBehaviour
    {

        [Header("Components")]
        public Animator animator;
        public AudioSource audioSource;
        [Header("Configs")]
        public GunConfig config;

        public abstract GunBase CreateGun();
    }
}