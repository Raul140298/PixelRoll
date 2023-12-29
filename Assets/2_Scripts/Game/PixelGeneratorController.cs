using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PixelGeneratorController : MonoBehaviour
{
    public static PixelGeneratorController Instance { get; private set; }
    public int PaintedPixels;
    [SerializeField] public Sprite[] imageSprites;
    [SerializeField] public Sprite imageSprite;
    [SerializeField] private Tilemap imageMap;
    [SerializeField] private Tile tilePixel;
    public int NonTransparentPixels { get; private set; }

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
    
    // Making pixels a public array so that we can read it from other scripts
    public Color[] Pixels { get; private set; }
    
    private const float Tolerance = 0.01f;

    private void Start()
    {
        // Assigning a random image sprite to the imageSprite variable
        imageSprite = imageSprites[UnityEngine.Random.Range(0, imageSprites.Length)];
        
        Texture2D tex = SpriteToTexture2D(imageSprite);
        Pixels = tex.GetPixels();
        int width = tex.width;
        
        // Centering the image map to the camera
        CenterImageMapToScreen(width, tex.height);
        
        // Counting the non-transparent pixels
        NonTransparentPixels = Pixels.Count(pixel => pixel.a != 0);

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
                imageMap.SetColor(pos, new Color(1,1,1,0.5f));
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
        
        // Scaling the image to take up to 80% of the camera's width or 90% of its height
        var scaleFactor = ScaleImageToCameraWidth(width, height);

        // Calculating the right edge of the screen in world coordinates
        var worldRightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 
            Screen.height / 2, Camera.main.nearClipPlane));

        // Adjusting the position of the Tilemap to align with the right edge of the screen
        var transform1 = imageMap.transform;
        var scaledWidth = width * scaleFactor;

        // Adding a small margin to the right edge
        const float rightMargin = 0.4f;
        transform1.position = new Vector3(worldRightEdge.x - scaledWidth - rightMargin, worldRightEdge.y - 
            height * scaleFactor / 2, transform1.position.z);
    }
    
    private float ScaleImageToCameraWidth(int width, int height)
    {
        float cameraWidth, cameraHeight;
        
        //Checking if the camera is orthographic (the view is a parallel projection onto the view plane)
        if (Camera.main.orthographic)
        {
            // Calculating the world width of the camera's view
            var main = Camera.main;
            cameraWidth = main.orthographicSize * 2 * main.aspect;
            cameraHeight = main.orthographicSize * 2;
        }
        else
        {
            // Calculating the distance between the camera and the image
            var distance = Mathf.Abs(imageMap.transform.position.z - Camera.main.transform.position.z);
            // Calculating the world width of the camera's view
            cameraWidth = 2.0f * distance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
            cameraHeight = cameraWidth / Camera.main.aspect;
        }
        
        var scaleFactorWidth = cameraWidth / width * 0.8f;
        var scaleFactorHeight = (cameraHeight / height) * 0.9f;

        // Using the smaller of the two scale factors
        var scaleFactor = Mathf.Min(scaleFactorWidth, scaleFactorHeight);

        // Applying the scale factor to the image's transform
        imageMap.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);

        return scaleFactor;
    }    
}