using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResolutionChecker 
{
    private const float DESIRED_RATIO_WIDTH = 16;
    private const float DESIRED_RATIO_HEIGHT = 9;

    public static void FixAspectRatio()
    {
        if (Screen.fullScreen)
        {
            int width = Screen.width;
            int height = Screen.height;

            float currentRatio = width * 1f / height;
            float desiredRatio = DESIRED_RATIO_WIDTH / DESIRED_RATIO_HEIGHT;

            // Debug.Log("SPLASH: CURRENT RESOLUTION: " + width + "x" + height);

            if (currentRatio < desiredRatio) // 4:3 RESOLUTIONS, FOR EXAMPLE
            {
                int desiredHeight = Mathf.FloorToInt((width / DESIRED_RATIO_WIDTH) * DESIRED_RATIO_HEIGHT);
                Screen.SetResolution(width, desiredHeight, true);
                // Debug.Log("FIXING LOW RESOLUTION TO 16:9");
            }
            else if (currentRatio == desiredRatio)
            {
                // Debug.Log("SAME ASPECT RATIO");
            }
            else if (currentRatio > desiredRatio) // 21:9 RESOLUTIONS, FOR EXAMPLE
            {
                int desiredWidth = Mathf.FloorToInt((height / DESIRED_RATIO_HEIGHT) * DESIRED_RATIO_WIDTH);
                Screen.SetResolution(desiredWidth, height, true);
                // Debug.Log("FIXING HIGH RESOLUTION TO 16:9");
            }
        }
    }
}
