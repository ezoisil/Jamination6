using System;
using System.Collections;
using System.Collections.Generic;
using Combat_System;
using UnityEngine;

namespace RegularDuck
{
    public class DamageGiverArea : DamageGiver
    {
        [SerializeField] private float _damageGivingFrequency;

        private float _delta = 0;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DamageReceiver damageable))
            {
                if(_currentTargets.Contains(damageable)) return;
                AddNewTarget(damageable);
                TryGiveDamage(damageable);
            }
        }

        private void Update()
        {
            _delta = Time.deltaTime;
            
            for (int i = _currentTargets.Count - 1; i >= 0; i--)
            {
                var target = _currentTargets[i];

                if (_damageTimers[target] >= _delay + _damageGivingFrequency)
                {
                    TryGiveDamage(target);
                    _damageTimers[target] = 0;
                }
                _damageTimers[target] += _delta;
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out DamageReceiver damageable) && _currentTargets.Contains(damageable))
            {
                RemoveTarget(damageable);
            }
        }
    }
}
