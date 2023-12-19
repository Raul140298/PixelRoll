using System;
using System.Collections;
using System.Collections.Generic;

using Game;

using UnityEngine;
using UnityEngine.Tilemaps;

public class GameInputController : MonoBehaviour
{
    
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
                Debug.Log(GameController.Instance.TileMap.GetColor(tpos));
            }
        }
    }
}
