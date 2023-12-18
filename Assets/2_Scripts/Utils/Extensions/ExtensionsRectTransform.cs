/*
 *  by @gontzalve
 *  July 11th, 2018
 */

using UnityEngine;
using System.Collections;

public static class ExtensionsRectTransform
{
    // SETTING ANCHORED POSITIONS ======================================================

    public static void SetAnchoredPositionX(this RectTransform rt, float newX)
    {
        rt.anchoredPosition = new Vector2(newX, rt.anchoredPosition.y);
    }

    public static void SetAnchoredPositionY(this RectTransform rt, float newY)
    {
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, newY);
    }
		
    // GETTING ANCHORED POSITIONS ======================================================

    public static float GetAnchoredPositionX(this RectTransform rt)
    {
        return rt.anchoredPosition.x;
    }

    public static float GetAnchoredPositionY(this RectTransform rt)
    {
        return rt.anchoredPosition.y;
    }
}