using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PixelGeneratorController : MonoBehaviour
{
    public static PixelGeneratorController Instance { get; private set; }

    private void Awake()
    {
        // Singleton pattern (restricts the instantiation of a class to one object)
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    [SerializeField] private Sprite imageSprite;
    [SerializeField] private Tilemap imageMap;
    [SerializeField] private Tile tilePixel;
    
    private const float Tolerance = 0.01f;
    
    // Creating a dictionary to store the original colors of the pixels
    private Dictionary<Vector3Int, Color> originalColors = new Dictionary<Vector3Int, Color>();

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
                
                originalColors[pos] = pixelColor;
                
                imageMap.SetTile(pos, tilePixel);
                imageMap.SetTileFlags(pos, TileFlags.None);
                
                // Setting the color to white
                imageMap.SetColor(pos, Color.white);
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
    
    public Color GetOriginalColor(Vector3Int position)
    {
        // Checking if the position is in the dictionary
        if (originalColors.ContainsKey(position))
        {
            return originalColors[position];
        }
        
        // Returning white if the position is not in the dictionary
        return Color.white;
    }
}