using UnityEngine;

public class Test : MonoBehaviour
{
    [Header("Projectile Hole")]
    public LayerMask collisionMask;
    public CustomRenderTexture paintCRT;
    public Material paintMaterial;
    public float brushSize = 0.02f;
  // private ProjectileHolePainter projectileHolePainter;

    void Start()
    {
      //  projectileHolePainter = new ProjectileHolePainter(collisionMask, paintCRT, paintMaterial, brushSize);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleRaycast();
        }
    }

    void HandleRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 100, collisionMask))
        {

     
          //  projectileHolePainter.HolePaint(hit);


        }
    }
}
