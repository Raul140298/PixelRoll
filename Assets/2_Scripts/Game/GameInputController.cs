using Game;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameInputController : MonoBehaviour
{
    private static GameInputController Instance { get; set; }
    
    private int RemainingClicks { get; set; }
    
    private void SetMaxPixels(int maxPixels)
    {
        RemainingClicks = maxPixels;
    }
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    [SerializeField] private PixelGeneratorController pixelGeneratorController;

    public void Start()
    {
        // Testing the max pixels
        GameInputController.Instance.SetMaxPixels(5);
    }

    private void Update()
    {
        CheckPlayerClick();
    }

    private void CheckPlayerClick()
    {
        // Checking if there are no remaining clicks
        if (RemainingClicks <= 0)
        {
            return;
        }
        
        // Checking if the player has clicked once
        if(Input.GetMouseButtonDown(0)) 
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var tpos = GameController.Instance.TileMap.WorldToCell(worldPoint);
            Tile tile = GameController.Instance.TileMap.GetTile<Tile>(tpos);

            if(tile)
            {
                // Retrieving the original color of the pixel from the PixelGeneratorController
                Color originalColor = PixelGeneratorController.Instance.GetOriginalColor(tpos);

                // Applying the original color to the pixel in the image
                GameController.Instance.TileMap.SetColor(tpos, originalColor);
                
                Debug.Log(GameController.Instance.TileMap.GetColor(tpos));
                
                // Decreasing the number of remaining clicks
                RemainingClicks--;
            }
        }
    }
}
