using System;
using System.Collections;
using System.Collections.Generic;
using Combat_System;
using Core.Scriptable_Variables;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    [SerializeField] private FloatVariable _playerHealth;

    protected override void Awake()
    {
        base.Awake();
        _playerHealth.ChangeValue(CurrentHealth);
    }

    private void OnEnable()
    {
        OnDamageTaken += UpdateHealthVariable;
    }

    private void OnDisable()
    {
        OnDamageTaken -= UpdateHealthVariable;
    }

    private void UpdateHealthVariable()
    {
        _playerHealth.ChangeValue(CurrentHealth);
    }
}
