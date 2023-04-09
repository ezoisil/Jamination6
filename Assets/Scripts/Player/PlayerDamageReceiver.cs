using System;
using System.Collections;
using System.Collections.Generic;
using Combat_System;
using Core.Scriptable_Variables;
using RegularDuck;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    [SerializeField] private FloatVariable _playerHealth;
    [SerializeField] private float _protectionTimeOnDamage = 1;
    protected override void Awake()
    {
        base.Awake();
        _playerHealth.ChangeValue(CurrentHealth);
    }

    private void OnEnable()
    {
        OnDamageTaken += DamageTaken;
    }

    private void OnDisable()
    {
        OnDamageTaken -= DamageTaken;
    }

    private void DamageTaken()
    {
        UpdateHealthVariable();
        StartCoroutine(InvulnerableOnDamage());
    }
    private void UpdateHealthVariable()
    {
        _playerHealth.ChangeValue(CurrentHealth);
    }

    private IEnumerator InvulnerableOnDamage()
    {
        CanTakeDamage = false;
        yield return new WaitForSeconds(_protectionTimeOnDamage);
        CanTakeDamage = true;

    }
}
