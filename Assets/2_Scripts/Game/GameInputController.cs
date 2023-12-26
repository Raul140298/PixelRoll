using System.Linq;
using Game;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class GameInputController : MonoBehaviour
{
    [SerializeField] private GameUIController gameUIController;
    private static GameInputController _instance;
    private bool isClicking = false;
    private int _mode = 0;
    
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

    private int RemainingClicks { get; set; }
    
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
        
        // Checking if the player has released the mouse button
        if (!Input.GetMouseButton(0)) isClicking = false;
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
            _mode = 2;
            
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
            default:
                return;
        }
    }

    private static void ManagePixels(Vector3Int tpos, int width)
    {
        var index = tpos.y * width + tpos.x;
        if (PixelGeneratorController.Instance.Pixels[index].a == 0)return;
        GameController.Instance.TileMap.SetColor(tpos, PixelGeneratorController.Instance.Pixels[index]);
        PixelGeneratorController.Instance.PaintedPixels++;
        PixelGeneratorController.Instance.SetPixelColor(index, new Color(0, 0, 0, 0));
    }
    
    private void PaintPixel(Vector3Int tpos)
    {
        Tile tile = GameController.Instance.TileMap.GetTile<Tile>(tpos);

        if(tile)
        {
            // Retrieving the original color of the pixel from the Pixels array
            var width = PixelGeneratorController.Instance.imageSprite.texture.width;
            var index = tpos.y * width + tpos.x;

            // Checking if the pixel is transparent and skipping it
            if (PixelGeneratorController.Instance.Pixels[index].a == 0) return;
                
            // Applying the original color to the pixel
            GameController.Instance.TileMap.SetColor(tpos, PixelGeneratorController.Instance.Pixels[index]);
            
            Debug.Log(GameController.Instance.TileMap.GetColor(tpos));
                
            // Incrementing PaintedPixels
            PixelGeneratorController.Instance.PaintedPixels++;

            // Changing the pixel color in the array to transparent
            PixelGeneratorController.Instance.SetPixelColor(index, new Color(0, 0, 0, 0));
            
            RemainingClicks--;
        }
    }

    private void PaintColumn(Vector3Int position)
    {
        // Checking if the mouse button is still being held down and we've already processed a click
        if (isClicking && Input.GetMouseButton(0)) return;
        
        var width = PixelGeneratorController.Instance.imageSprite.texture.width;
        var height = PixelGeneratorController.Instance.imageSprite.texture.height;
        
        // Iterating through the column
        for (int i = 0; i < height; i++)
        {
            var tpos = new Vector3Int(position.x, i, position.z);
            var tile = GameController.Instance.TileMap.GetTile<Tile>(tpos);

            //Checking if the tile is not null
            if (tile)
            {
                // Managing the pixel array and the tilemap
                ManagePixels(tpos, width);
            }
        }
        RemainingClicks--;
        isClicking = true;
        // Checking if the game is over
        CheckGameOver();
    }

    private void PaintRow(Vector3Int position)
    {
        // Checking if the mouse button is still being held down and we've already processed a click
        if (isClicking && Input.GetMouseButton(0)) return;
        
        var width = PixelGeneratorController.Instance.imageSprite.texture.width;
        var height = PixelGeneratorController.Instance.imageSprite.texture.height;

        // Iterating through the row
        for (int i = 0; i < width; i++)
        {
            var tpos = new Vector3Int(i, position.y, position.z);
            Tile tile = GameController.Instance.TileMap.GetTile<Tile>(tpos);

            // Checking if the tile is not null
            if (tile)
            {
                // Managing the pixel array and the tilemap
                ManagePixels(tpos, width);
            }
        }
        RemainingClicks--;
        isClicking = true;
        // Checking if the game is over
        CheckGameOver();
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
