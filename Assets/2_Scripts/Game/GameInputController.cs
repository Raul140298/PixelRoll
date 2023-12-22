using Game;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class GameInputController : MonoBehaviour
{
    [SerializeField] private GameUIController gameUIController;
    private static GameInputController _instance;
    
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
    }

    // Creating an event that will be invoked when all clicks are used
    public UnityEvent OnAllClicksUsed;
    
    private void CheckPlayerClick()
    {
        // Checking if there are no remaining clicks
        if (RemainingClicks <= 0)
        {
            return;
        }
        
        // Checking if the player has clicked once
        if(Input.GetMouseButton(0)) 
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var tpos = GameController.Instance.TileMap.WorldToCell(worldPoint);
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

                // Changing the pixel color in the array to transparent
                PixelGeneratorController.Instance.SetPixelColor(index, new Color(0, 0, 0, 0));

                // Decreasing the remaining clicks
                RemainingClicks--;
            }
            // If no more clicks are remaining, invoke the OnAllClicksUsed event
            if (RemainingClicks <= 0)
            {
                gameUIController.RestartButtons();
            }
        }
    }
}
