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
    
    [SerializeField] public Sprite imageSprite;
    [SerializeField] private Tilemap imageMap;
    [SerializeField] private Tile tilePixel;
    
    // Making pixels a public array so that we can read it from other scripts
    public Color[] Pixels { get; private set; }
    
    private const float Tolerance = 0.01f;
    

    private void Start()
    {
        Texture2D tex = SpriteToTexture2D(imageSprite);
        Pixels = tex.GetPixels();
        int width = tex.width;
        
        // Centering the image map to the camera
        CenterImageMapToScreen(width, tex.height);

        for (int y = tex.height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixelColor = Pixels[y * width + x];
                
                // Checking if the pixel is transparent and skipping it
                if (pixelColor.a == 0) continue;

                Vector3Int pos = new Vector3Int(x, y);
                
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
    
    public void SetPixelColor(int index, Color color)
    {
        // Checking if the index is within the array bounds
        if (index >= 0 && index < Pixels.Length)
        {
            Pixels[index] = color;
        }
    }
    
    private void CenterImageMapToScreen(int width, int height)
    {
        // Checking if the main camera exists
        if (Camera.main == null) return;
        
        // Calculating the center of the screen in world coordinates
        var worldCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 
            Screen.height / 2, Camera.main.nearClipPlane));

        // Adjusting the position of the Tilemap to the center of the screen
        var transform1 = imageMap.transform;
        transform1.position = new Vector3(worldCenter.x - width / 2, worldCenter.y - height / 2, 
            transform1.position.z);
    }
}