using UnityEngine;

namespace AS.Gameplay.Core.Utils
{
    public sealed class SurfacePainter
    {
        private static readonly int BrushTex = Shader.PropertyToID("_BrushTex");
        private static readonly int BrushPos = Shader.PropertyToID("_BrushPos");
        private static readonly int BrushSize = Shader.PropertyToID("_BrushSize");

        private readonly RenderTexture _paintTexture;
        private readonly Material _paintMaterial;

        public SurfacePainter(RenderTexture paintTexture, Material paintMaterial)
        {
            _paintTexture = paintTexture;
            _paintMaterial = paintMaterial;
        }
        /// <summary>
        /// Рисует на поверхности текстуру кисти в указанной позиции с заданным размером.
        /// </summary>
        /// <param name="hit"></param>
        /// <param name="brush"></param>
        /// <param name="size"></param>
        public void Paint(RaycastHit hit, Texture brush, float size)
        {
            _paintMaterial.SetTexture(BrushTex, brush);
            _paintMaterial.SetVector(BrushPos, hit.textureCoord);
            _paintMaterial.SetFloat(BrushSize, size);

            RenderTexture current = RenderTexture.active;

            Graphics.Blit(_paintTexture, _paintTexture, _paintMaterial);

            RenderTexture.active = current;
        }
    }
}