using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunData
{
    public string id;
    public string name;

}

[System.Serializable]
public class RPGData : GunData
{
    public GameObject projectilePrefab;
    public GameObject explosionPrefab;

    [Header("Gun")]
    
    public float fireRate = 0.2f;
    public float reloadTime = 2;
    [Header("Projectile")]
    public float projectileLifeTime = 5f;

    [Header("Projectile Physics")]
    public LayerMask collisionMask;
    public float projectileRadius = 0.05f;
    public float projectileSpeed = 1f;
    public float projectileGravity = 1f;
    public float projectileBounce = 0.6f;
    public int projectileMaxBounce = 2;

    [Header("Trajectory Line")]
    public float trajectorylineSize = 0.05f;




}

[CreateAssetMenu(menuName = "Configs/Gun Config")]
public sealed class GunConfig : ScriptableObject
{
    [SerializeReference]
    public GunData data;

}