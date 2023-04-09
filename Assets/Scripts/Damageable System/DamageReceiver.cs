using UnityEngine;

namespace Combat_System
{
	public class DamageReceiver : MonoBehaviour
	{

		protected float MaxHealth;
		protected float CurrentHealth;
		protected bool CanTakeDamage;

		public DamageableState DamageableState { get; private set; }

		#region Events

		public delegate void DamageableHandler();

		public event DamageableHandler OnHealthChanged;
		public event DamageableHandler OnBeforeDamageReceived;
		public event DamageableHandler OnDamageTaken;
		public event DamageableHandler OnAfterDamageReceived;
		public event DamageableHandler OnHealthBelowZero;
		public event DamageableHandler OnDeath;

		public event DamageableHandler OnRevive;

		#endregion

		protected virtual void Awake()
		{
			Initialize();
		}

		private void Start()
		{
			ChangeVulnerability(true);
		}

		private void OnDestroy()
		{
		}

		public virtual bool TryReceiveDamage(float damage)
		{
			if (!CanTakeDamage) return false;
			OnBeforeDamageReceived?.Invoke();
			ProcessDamage(damage);
			OnAfterDamageReceived?.Invoke();
			return true;
		}

		public virtual void ChangeVulnerability(bool isVulnerable)
		{
			CanTakeDamage = isVulnerable;
		}

		protected virtual void ProcessDamage(float damage)
		{
			LoseHealth(damage);
			OnDamageTaken?.Invoke();

			if (CurrentHealth <= 0)
			{
				OnHealthBelowZero?.Invoke();
				Die();
			}
		}

		protected virtual void Heal(float healAmount)
		{
			if (DamageableState == DamageableState.Dead) return;
			CurrentHealth = Mathf.Min(CurrentHealth + healAmount, MaxHealth);
			OnHealthChanged?.Invoke();
		}

		protected virtual void LoseHealth(float damage)
		{
			CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
			OnHealthChanged?.Invoke();
		}
		

		protected virtual void Revive(bool maxHealth, float healAmount = 0)
		{
			DamageableState = DamageableState.Alive;
			ChangeVulnerability(true);
			if (maxHealth)
			{
				Heal(MaxHealth);
			}
			else
			{
				Heal(healAmount);
			}

			OnRevive?.Invoke();
		}
		
		protected virtual void Die()
		{
			ChangeVulnerability(false);
			DamageableState = DamageableState.Dead;
			OnDeath?.Invoke();
		}

		protected virtual void Initialize()
		{
			MaxHealth = CurrentHealth;
			DamageableState = DamageableState.Alive;
		}

	}

	public enum DamageableState
	{
		Alive,
		Dead
	}
}