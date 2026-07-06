using UnityEngine;

namespace AS.Gameplay.Core.Utils
{
    public static class ProceduralCubeMesh
    {
        /// <summary>
        /// Процедурная генерация куба с возможностью случайного смещения вершин
        /// </summary>
        /// <param name="size"></param>
        /// <param name="randomOffset"></param>
        /// <returns></returns>
        public static Mesh Create(float size, float randomOffset)
        {
            Mesh mesh = new Mesh();

            float h = size * 0.5f;

            Vector3[] vertices =
            {
            new(-h,-h,-h),
            new( h,-h,-h),
            new( h, h,-h),
            new(-h, h,-h),

            new(-h,-h, h),
            new( h,-h, h),
            new( h, h, h),
            new(-h, h, h),
        };

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] += Random.insideUnitSphere * randomOffset;
            }

            int[] triangles =
            {
            0,2,1, 0,3,2,
            1,2,6, 1,6,5,
            5,6,7, 5,7,4,
            4,7,3, 4,3,0,
            3,7,6, 3,6,2,
            4,0,1, 4,1,5
        };

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            
            
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            return mesh;
        }
    }
}