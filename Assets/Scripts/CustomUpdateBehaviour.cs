using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CustomUpdateBehaviour : MonoBehaviour
{
    
    private void OnEnable()
    {
        CustomUpdateManager.UpdateBehaviours.Add(this);
    }

    private void OnDisable()
    {
        CustomUpdateManager.UpdateBehaviours.Remove(this);
    }

    public virtual void OnCustomUpdate(float deltaTime)
    {
        
    }
}
