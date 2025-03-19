using UnityEngine;

namespace ZL.Unity
{
    public static partial class Texture2DExtensions
    {
        public static Sprite ToSprite(this Texture2D instance)
        {
            Rect rect = new(0f, 0f, instance.width, instance.height);

            Vector2 pivot = new(0.5f, 0.5f);

            return Sprite.Create(instance, rect, pivot);
        }

        public static Texture2D ResizeTo(this Texture2D instance, int width, int height)
        {
            var renderTexture = RenderTexture.GetTemporary(width, height);

            Graphics.Blit(instance, renderTexture);

            RenderTexture.active = renderTexture;

            Texture2D texture = new(width, height, TextureFormat.RGBA32, false);

            texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);

            texture.Apply();

            RenderTexture.active = null;

            RenderTexture.ReleaseTemporary(renderTexture);

            return texture;
        }
    }
}