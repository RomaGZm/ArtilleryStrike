using UnityEngine;

namespace AS.Gameplay.Player.Weapons.Shared.Code.Projectiles
{
    public sealed class TrajectoryPredictor
    {
        private LineRenderer line;
        public TrajectoryPredictor(LineRenderer line, float lineSize = 0.05f)
        {
            this.line = line;
            line.startWidth = lineSize;
            line.endWidth = lineSize;
        }
        /// <summary>
        /// Рисует траекторию полета снаряда на основе начальной позиции, направления, скорости, силы гравитации, количества шагов и времени между шагами.
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="direction"></param>
        /// <param name="speed"></param>
        /// <param name="gravity"></param>
        /// <param name="steps"></param>
        /// <param name="stepTime"></param>
        public void Draw(Vector3 startPosition, Vector3 direction, float speed, float gravity, int steps, float stepTime)
        {
            Vector3 position = startPosition;
            Vector3 velocity = direction.normalized * speed;

            line.positionCount = steps;

            for (int i = 0; i < steps; i++)
            {
                line.SetPosition(i, position);

                velocity += Vector3.down * gravity * stepTime;
                position += velocity * stepTime;
            }
        }
    }
}