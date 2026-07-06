using UnityEngine;

public class ObstacleView : MonoBehaviour
{
    [Header("Projectile Hole")]
    public CustomRenderTexture targetCRT;
    public Material targetMaterial;
    public Vector2 holeSize = new Vector2(0.2f, 0.2f);
}
