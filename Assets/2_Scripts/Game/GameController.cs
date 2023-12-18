using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class GameController : MonoBehaviour
	{
		private static GameController instance;

		void Awake()
		{
			instance = this;
		}

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
