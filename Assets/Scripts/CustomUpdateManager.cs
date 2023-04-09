using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomUpdateManager : MonoBehaviour
{
    [SerializeField] private float _fps;
    public static List<CustomUpdateBehaviour> UpdateBehaviours = new List<CustomUpdateBehaviour>();
    private float _timer;
    private bool _isEnabled = true;

    private void FixedUpdate()
    {
        if(!_isEnabled) return;
        float deltaTime = Time.fixedDeltaTime;
        _timer += deltaTime;
        if ((_timer <= _fps/100))
            return;

        _timer = 0;
        for (int i = 0; i < UpdateBehaviours.Count; i++)
        {
            UpdateBehaviours[i].OnCustomUpdate(deltaTime);
        }

    }

}
