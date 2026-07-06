using UnityEngine;

namespace AS.Gameplay.Player.Weapons.Shared.Projectiles
{

    public class ProjectileHolePainter
    {
        
        public ProjectileHolePainter(){}


        public void HolePaint(RaycastHit hit)
        {
            Vector2 uv = hit.textureCoord;

            ObstacleView obstacleView = hit.collider.GetComponent<ObstacleView>();

            Vector4 drawPosition = new Vector4(uv.x, uv.y, obstacleView.holeSize.x, obstacleView.holeSize.y);
            obstacleView.targetMaterial.SetVector("_DrawPosition", drawPosition);
            obstacleView.targetCRT.Update();


        }

    }
}