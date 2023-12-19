using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PixelGeneratorController : MonoBehaviour
{
    [SerializeField] private Sprite imageSprite;
    [SerializeField] private Tilemap imageMap;
    [SerializeField] private Tile tilePixel;
    
    private const float Tolerance = 0.01f;

    private void Start()
    {
        Texture2D tex = SpriteToTexture2D(imageSprite);
        Color[] pixels = tex.GetPixels();
        int width = tex.width;

        for (int y = tex.height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixelColor = pixels[y * width + x];

                if (pixelColor.a == 0) continue;

                Vector3Int pos = new Vector3Int(x, y);
                
                imageMap.SetTile(pos, tilePixel);
                imageMap.SetTileFlags(pos, TileFlags.None);
                imageMap.SetColor(pos, pixelColor);
            }
        }
    }

    private static Texture2D SpriteToTexture2D(Sprite sprite)
    {
        // Checking if the sprite is not a texture
        if (!(Math.Abs(sprite.rect.width - sprite.texture.width) > Tolerance)) return sprite.texture;
        
        // Creating new texture and copying the sprite texture to it
        Texture2D newText = new Texture2D((int)sprite.rect.width,(int)sprite.rect.height);
        Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x, (int)sprite.textureRect.y, 
            (int)sprite.textureRect.width, (int)sprite.textureRect.height );
        newText.SetPixels(newColors);
        newText.Apply();
        
        return newText;
    }
}