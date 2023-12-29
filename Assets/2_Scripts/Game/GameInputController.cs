using Game;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class GameInputController : MonoBehaviour
{
    [SerializeField] private GameUIController gameUIController;
    private static GameInputController _instance;
    private int _mode = 0;
    private int RemainingClicks { get; set; }
    
    // Singleton pattern (restricts the instantiation of a class to one object)
    public static GameInputController Instance
    {
        get
        {
            // Checking if the instance is not null
            if (_instance != null) return _instance;
            // Finding the instance in the scene
            _instance = FindObjectOfType<GameInputController>();
            // Checking if the instance is not null
            if (_instance != null) return _instance;
            // Creating a new instance
            var go = new GameObject
            {
                name = "GameInputController"
            };
            // Adding the GameInputController component to the instance
            _instance = go.AddComponent<GameInputController>();

            return _instance;
        }
        // Making the setter private so that the instance can only be set from inside the class
        private set => throw new System.NotImplementedException();
    }
    
    private void Awake()
    {
        // Checking if the instance is not null
        if (Instance != null) return;
        
        // Setting the instance to this object
        Instance = this;
        RemainingClicks = 0;
        OnAllClicksUsed = new UnityEvent();
    }
    
    public void SetMaxPixels(int maxPixels)
    {
        RemainingClicks = maxPixels;
    }

    private void Update()
    {
        // Checking if the player has clicked
        CheckPlayerClick();
    }

    // Creating an event that will be invoked when all clicks are used
    public UnityEvent OnAllClicksUsed;
    
    // ReSharper disable Unity.PerformanceAnalysis
    private void CheckPlayerClick()
    {
        // Checking if there are no remaining clicks
        if (RemainingClicks <= 0)
        {
            return;
        }
        
        // Checking if the player has clicked
        if(Input.GetMouseButton(0)) 
        {
            // Calculating the tile position from the mouse click
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var tpos = GameController.Instance.TileMap.WorldToCell(worldPoint);
            _mode = 3;
            
            // Selecting the paint mode
            SelectPaintMode(tpos, _mode);
            
            // Checking if the player has clicks remaining
            if (RemainingClicks > 0) return;
            
            // Restarting the pick button
            gameUIController.RestartButtons();
            
            // Checking if the player has used all rolls
            if (gameUIController.currentRolls == 0)
            {
                // Activating the game over object
                gameUIController.ShowGameOver();
            }
        }
    }

    private void SelectPaintMode(Vector3Int tpos, int mode)
    {
        // Checking which mode of paint is selected
        switch (mode)
        {
            case 1:
                PaintPixel(tpos);
                break;
            case 2:
                PaintColumn(tpos);
                break;
            case 3:
                PaintRow(tpos);
                break;
            case 4:
                Fill(tpos);
                break;
            case 5:
                ShakeImage();
                break;
            case 6:
                UnpaintRandomly();
                break;
            default:
                return;
        }
    }

    private static void ManagePixels(Vector3Int tpos, int width)
    {
        var index = tpos.y * width + tpos.x;
        if (PixelGeneratorController.Instance.Pixels[index].a == 0) return;
        GameController.Instance.TileMap.SetColor(tpos, PixelGeneratorController.Instance.Pixels[index]);
        PixelGeneratorController.Instance.PaintedPixels++;
        PixelGeneratorController.Instance.SetPixelColor(index, new Color(0, 0, 0, 0.5f));
    }
    
    private static void RemovePixels(Vector3Int tpos, int width)
    {
        var index = tpos.y * width + tpos.x;
        if (PixelGeneratorController.Instance.Pixels[index].a <= 0.5f) return;
        PixelGeneratorController.Instance.SetPixelColor(index, GameController.Instance.TileMap.GetColor(tpos));
        PixelGeneratorController.Instance.PaintedPixels--;
        GameController.Instance.TileMap.SetColor(tpos, new Color(1, 1, 1, 0.5f));
    }
    
    private static void Fill(Vector3Int tpos)
    {
        
    }
    
    private static void ShakeImage()
    {
        
    }
    
    private void PaintPixel(Vector3Int tpos)
    {
        Tile tile = GameController.Instance.TileMap.GetTile<Tile>(tpos);

        if(tile)
        {
            // Retrieving the original color of the pixel from the Pixels array
            var width = PixelGeneratorController.Instance.imageSprite.texture.width;

            ManagePixels(tpos, width);
            
            RemainingClicks--;
        }
    }

    private void PaintColumn(Vector3Int position)
    {
        
        var width = PixelGeneratorController.Instance.imageSprite.texture.width;
        var height = PixelGeneratorController.Instance.imageSprite.texture.height;
        var painted = false;
        
        // Checking if the clicked position is within the image boundaries
        if (position.x < 0 || position.x >= width || position.y < 0 || position.y >= height) return;

        var initialPixelIndex = position.y * width + position.x;
    
        // Checking if the initially clicked pixel is transparent and skipping it
        if (PixelGeneratorController.Instance.Pixels[initialPixelIndex].a == 0) return;
        
        // Iterating through the column
        for (var i = position.y + 1; i < height; i++)
        {
            var tpos = new Vector3Int(position.x, i, position.z);
            var tile = GameController.Instance.TileMap.GetTile<Tile>(tpos);

            //Checking if the tile is not null
            if (!tile) break;
            
            // Managing the pixel array and the tilemap
            ManagePixels(tpos, width);
            painted = true;
        }
        
        for (var i = position.y; i >= 0; i--)
        {
            var tpos = new Vector3Int(position.x, i, position.z);
            var tile = GameController.Instance.TileMap.GetTile<Tile>(tpos);

            //Checking if the tile is not null
            if (!tile) break;
            
            // Managing the pixel array and the tilemap
            ManagePixels(tpos, width);
            painted = true;
        }
        
        // Checking if a column was painted and decrementing the remaining clicks
        if (painted) RemainingClicks--;
        
        // Checking if the game is over
        CheckGameOver();
    }

    private void PaintRow(Vector3Int position)
    {
        var width = PixelGeneratorController.Instance.imageSprite.texture.width;
        var height = PixelGeneratorController.Instance.imageSprite.texture.height;
        var painted = false;
        
        // Checking if the clicked position is within the image boundaries
        if (position.x < 0 || position.x >= width || position.y < 0 || position.y >= height) return;

        var initialPixelIndex = position.y * width + position.x;
    
        // Checking if the initially clicked pixel is transparent and skipping it
        if (PixelGeneratorController.Instance.Pixels[initialPixelIndex].a == 0) return;
        
        // Iterating through the row
        for (var i = position.x + 1; i < width; i++)
        {
            var tpos = new Vector3Int(i, position.y, position.z);
            Tile tile = GameController.Instance.TileMap.GetTile<Tile>(tpos);

            // Checking if the tile is not null
            if (!tile) break;
            
            // Managing the pixel array and the tilemap
            ManagePixels(tpos, width);
            painted = true;
        }
        
        for (var i = position.x; i >= 0; i--)
        {
            var tpos = new Vector3Int(i, position.y, position.z);
            Tile tile = GameController.Instance.TileMap.GetTile<Tile>(tpos);

            // Checking if the tile is not null
            if (!tile) break;
            
            // Managing the pixel array and the tilemap
            ManagePixels(tpos, width);
            painted = true;
        }
        
        // Checking if a column was painted and decrementing the remaining clicks
        if (painted) RemainingClicks--;
        
        // Checking if the game is over
        CheckGameOver();
    }
    
    private void UnpaintRandomly()
    {
        var width = PixelGeneratorController.Instance.imageSprite.texture.width;
        var height = PixelGeneratorController.Instance.imageSprite.texture.height;
        
        while (true)
        {
            Vector3Int tpos = new Vector3Int(Random.Range(0, width), Random.Range(0, height));
            Tile tile = GameController.Instance.TileMap.GetTile<Tile>(tpos);
            var index = tpos.y * width + tpos.x;
            
            if(tile && PixelGeneratorController.Instance.Pixels[index].a != 0.5f)
            {
                RemovePixels(tpos, width);
            }
        }
    }
    
    private void CheckGameOver()
    {
        // Checking if all pixels have been painted
        if (PixelGeneratorController.Instance.PaintedPixels == PixelGeneratorController.Instance.NonTransparentPixels)
        {
            gameUIController.ShowGameOver();
        }
    }
}
