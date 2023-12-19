using System;
using UnityEngine;

public class PixelGeneratorController : MonoBehaviour
{
    [SerializeField] private Sprite imageSprite;
    [SerializeField] private Pooler pixelPooler;
    
    private const float Tolerance = 0.01f;

    private void Start()
    {
        // Charging image from file
        var tex = SpriteToTexture2D(imageSprite);
        var pixels = tex.GetPixels();
        var width = tex.width;

        const float pixelSize = 1.0f;
        
        // Calculating scale factor to fit the image to the camera
        var scaleX = 1.0f / width;
        var scaleY = 1.0f / tex.height;
        var scale = Math.Min(scaleX, scaleY);

        for (var y = tex.height - 1; y >= 0; y--)
        {
            for (var x = 0; x < width; x++)
            {
                var pixelColor = pixels[y * width + x];
                // Creating pixel
                GameObject pixelInstance = pixelPooler.GetObject();
                pixelInstance.transform.SetPositionXY(new Vector2(x * scale, y * scale));
                // Adjusting pixel size according to scale
                pixelInstance.transform.localScale = new Vector3(pixelSize, pixelSize, pixelSize) * scale;
                // Setting pixel color
                pixelInstance.GetComponent<SpriteRenderer>().color = pixelColor;
                pixelInstance.GetComponent<Poolable>().Activate();
            }
        }
    }

    private static Texture2D SpriteToTexture2D(Sprite sprite)
    {
        // Checking if the sprite is not a texture
        if (!(Math.Abs(sprite.rect.width - sprite.texture.width) > Tolerance)) return sprite.texture;
        
        // Creating new texture and copying the sprite texture to it
        var newText = new Texture2D((int)sprite.rect.width,(int)sprite.rect.height);
        var newColors = sprite.texture.GetPixels((int)sprite.textureRect.x, (int)sprite.textureRect.y, 
            (int)sprite.textureRect.width, (int)sprite.textureRect.height );
        newText.SetPixels(newColors);
        newText.Apply();
        
        return newText;
    }
}