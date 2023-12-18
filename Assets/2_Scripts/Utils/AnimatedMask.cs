using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedMask : MonoBehaviour 
{
    public SpriteMask mask;
	public SpriteRenderer targetRenderer;

    void LateUpdate () 
	{
        if (mask.sprite != targetRenderer.sprite)
        {
            mask.sprite = targetRenderer.sprite;
        }
    }
}
