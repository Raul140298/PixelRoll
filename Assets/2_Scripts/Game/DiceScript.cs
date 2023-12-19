using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int ThrowDice(int faces)
    {
        // Checking if the dice has at least 2 faces
        if (faces < 2)
        {
            Debug.LogError("The dice must have at least 2 faces");
        }
        
        var result = Random.Range(1, faces + 1);
        return result;
    }
}
