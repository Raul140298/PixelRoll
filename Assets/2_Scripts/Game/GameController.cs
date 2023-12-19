using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
	public class GameController : MonoBehaviour
	{
		private static GameController instance;
		[SerializeField] private Tilemap tileMap;

		void Awake()
		{
			instance = this;
		}

		public Tilemap TileMap => tileMap;

		void OnDestroy()
		{
			instance = null;
		}

		public static GameController Instance
		{
			get { return instance; }
		}
	}
}
