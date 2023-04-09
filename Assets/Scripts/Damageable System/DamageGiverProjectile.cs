using System;
using System.Collections;
using System.Collections.Generic;
using Combat_System;
using RegularDuck;
using UnityEngine;

public class DamageGiverProjectile : DamageGiver
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out DamageReceiver damageReceiver))
            TryGiveDamage(damageReceiver);
    }
}
