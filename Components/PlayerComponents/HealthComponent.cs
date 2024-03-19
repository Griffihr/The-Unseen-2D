using Godot;
using System;

public partial class HealthComponent : Node2D
{
	[Export]
	public float MaxHealth {
		get => 	maxHealth;
		private set 
		{
			maxHealth = value;
			if (CurrentHealth >	MaxHealth)
			{
				CurrentHealth = maxHealth;
			}  
		}
	}

	public bool HasHealthRemaining => !Mathf.IsEqualApprox(CurrentHealth, 0.0f);
	public float CurrentHealthPercent => MaxHealth > 0 ? currentHealth / MaxHealth : 0;

	public float CurrentHealth {
		get => currentHealth;
		private set 
		{
			var previousHealth = currentHealth;
			currentHealth = Mathf.Clamp(value, 0, MaxHealth);
			var healtUpdate = new HealthUpdate 
			{
				PreviousHealth = previousHealth,
				CurrentHealth = currentHealth,
				MaxHealth = maxHealth,
				HealthPercent = CurrentHealthPercent,
				IsHeal = previousHealth <= CurrentHealth
			};
		}
	}
	public bool IsDamaged => CurrentHealth < MaxHealth;
	private bool hasDied;
	private float maxHealth = 10;
	private float currentHealth = 10;

	public void Heal(float heal) {
		currentHealth = heal;
	}

	public void Damage(float damage) {
		currentHealth -= damage;
	}

	public void SetMaxHealth(float Health) {
		MaxHealth = Health;
	}

	public void InitializeHealth() {
		CurrentHealth = MaxHealth;
	}

	public partial class HealthUpdate {
		public float PreviousHealth;
		public float CurrentHealth;
		public float MaxHealth;
		public float HealthPercent;
		public bool IsHeal;
	}

}
