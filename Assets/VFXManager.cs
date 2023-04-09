using System;
using System.Collections;
using System.Collections.Generic;
using Core.Scriptable_Variables;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private FloatVariable _playerHealth;

    private void OnEnable()
    {
        _playerHealth.OnValueChanged += OnPlayerHealthChanged;
    }

 

    private void OnDisable()
    {
        throw new NotImplementedException();
    }

    #region Event Listeners

    
    private void OnPlayerHealthChanged(float changeamount)
    {
        if(changeamount > 0) return;

        DamageTaken();
    }

    private void DamageTaken()
    {
        throw new NotImplementedException();
    }

    #endregion
    
}
