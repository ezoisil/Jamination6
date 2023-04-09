using System;
using System.Collections;
using System.Collections.Generic;
using Combat_System;
using RegularDuck;
using UnityEngine;

namespace RegularDuck
{
	public abstract class DamageGiver : MonoBehaviour
	{
		[Header("Damage Giver Settings")] [SerializeField]
		protected float _delay = 0;
		
		[SerializeField] private LayerMask _targetLayerMask;
		
		public float Damage;

		protected List<DamageReceiver> _currentTargets = new List<DamageReceiver>();
		protected Dictionary<DamageReceiver, float> _damageTimers= new Dictionary<DamageReceiver,float>();

		#region Events

		public delegate void DamageGiverHandler();

		public event DamageGiverHandler OnBeforeGivingDamage;
		public event DamageGiverHandler OnGivingDamage;
		public event DamageGiverHandler OnAfterGivingDamage;

		#endregion

		protected virtual void OnDestroy()
		{
			StopAllCoroutines();
		}

		public virtual void AddNewTarget(DamageReceiver damageReceiver)
		{
			_currentTargets.Add(damageReceiver);
			_damageTimers.Add(damageReceiver, 0);
		}

		protected virtual bool TryGiveDamage(DamageReceiver target)
		{
			if  (!IsOnTargetLayer(target.gameObject)) return false;
			StartCoroutine(DamageGivingProcess(target));
			return true;
		}

		protected virtual IEnumerator DamageGivingProcess(DamageReceiver damageReceiver)
		{
			OnBeforeGivingDamage?.Invoke();
			
			yield return new WaitForSeconds(_delay);
			
			GiveDamage(damageReceiver);
			OnAfterGivingDamage?.Invoke();
		}

		protected virtual void GiveDamage(DamageReceiver damageReceiver)
		{
			damageReceiver.TryReceiveDamage(Damage);
			OnGivingDamage?.Invoke();
		}

		protected virtual void RemoveTarget(DamageReceiver damageReceiver)
		{
			_currentTargets.Remove(damageReceiver);
			_damageTimers.Remove(damageReceiver);
		}

		#region Helpers

		protected bool IsOnTargetLayer(GameObject target)
		{
			return (_targetLayerMask.value & (1 << target.layer)) > 0;
		}

		#endregion


	}

}