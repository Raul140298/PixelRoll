using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class Helper 
{
	public static GameObject GetObjectAtPosition(Vector3 position, string layerName = "")
	{
		Vector3 wp = Camera.main.ScreenToWorldPoint(position);
		Vector2 pos = new Vector2(wp.x, wp.y);
		Collider2D col = null;

		col = (layerName == "") ? (Physics2D.OverlapPoint(pos)) : (Physics2D.OverlapPoint(pos, 1<<LayerMask.NameToLayer(layerName)));

		if(col != null)
		{
			return col.gameObject;
		}

		return null;
	}

    public static eScreen GetScreenEnumFromName(string screenName)
    {
        return (eScreen)Enum.Parse(typeof(eScreen), screenName);
    }

    public static string[] GetSentences(string message)
    {
        string[] sentences = Regex.Split(message, @"(?<=[.,;?!])");
        return sentences;
    }
    public static string[] GetSentencesInCredits(string message)
    {
        string[] sentences = Regex.Split(message, @"(?<=[.,;?!\n])");
        return sentences;
    }

    public static int ChooseRandomOption(int[] chances)
    { 
        int index = -1;
        int totalWeight = 0;

        foreach (int w in chances)
        {
            totalWeight += w;
        }

        int rand = UnityEngine.Random.Range(1, totalWeight + 1);

        for (index = 0; index < chances.Length; index++)
        {
            rand = rand - chances[index];

            if (rand < 0)
            {
                break;
            }
        }

        return index;
    }

    // public static eControllerType GetControllerType(Rewired.Controller controller)
    // {
    //     if (controller == null)
    //     { 
    //         return eControllerType.Keyboard;
    //     }
        
    //     if (controller.type == Rewired.ControllerType.Keyboard)
    //     {
    //         return eControllerType.Keyboard;
    //     }
    //     else if (controller.type == Rewired.ControllerType.Joystick)
    //     {
    //         if (controller.name.Contains("Sony"))
    //         {
    //             return eControllerType.PS4Joystick;
    //         }
    //         else if (controller.name.Contains("Switch"))
    //         {
    //             return eControllerType.SwitchJoystick;
    //         }

    //         return eControllerType.XBOXJoystick;
    //     }

    //     return eControllerType.Keyboard;
    // }
}
