using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DiceHelper
{
    public static int ThrowDice(int faces)
    {
        // Checking if the dice has at least 3 faces
        if (faces < 3)
        {
            Debug.LogError("The dice must have at least 3 faces");
            return 0;
        }
        
        var result = Random.Range(1, faces + 1);
        return result;
    }
}
