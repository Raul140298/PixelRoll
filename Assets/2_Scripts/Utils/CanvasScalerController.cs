using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class CanvasScalerController : MonoBehaviour
{
    private const int TARGET_WIDTH = 1920;
    private const int TARGET_HEIGHT = 1080;

    private float DesiredAspectRatio => TARGET_WIDTH * 1.0f / TARGET_HEIGHT;
    private float CurrentAspectRatio => Screen.width * 1.0f / Screen.height;

    private RectTransform canvas;

    private void Awake()
    {
	    canvas = this.GetComponent<RectTransform>();
    }

    private void OnRectTransformDimensionsChange()
    {
	    if (canvas == null) return;
	    
	    MatchWidthOrHeight();
    }

    private void MatchWidthOrHeight()
    {
	    GetComponent<CanvasScaler>().matchWidthOrHeight = (CurrentAspectRatio < DesiredAspectRatio) ? 0 : 1;
    }
}