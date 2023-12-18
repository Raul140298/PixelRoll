using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePoolableObjectType
{ 
    
}

public class Poolable : MonoBehaviour 
{
	[SerializeField]
    private ePoolableObjectType type;

	[SerializeField]
    private bool isActive;

    public void InitializeActiveStatus()
    {
        isActive = gameObject.activeInHierarchy;
    }

    public void Activate()
    {
        isActive = true;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        isActive = false;
        gameObject.SetActive(false);
    }

    public ePoolableObjectType AskForType()
    {
        return type;
    }

    public bool IsActive()
    {
        return isActive;
    }
}
