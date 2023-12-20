using System;
using System.Collections;
using System.Collections.Generic;

using Game;

using UnityEngine;
using UnityEngine.Tilemaps;

public class GameInputController : MonoBehaviour
{
    public static GameInputController Instance { get; private set; }

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
    
    private void Update()
    {
        CheckPlayerClick();
    }

    private void CheckPlayerClick()
    {
        if(Input.GetMouseButton(0))
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
            }
        }
    }
}
