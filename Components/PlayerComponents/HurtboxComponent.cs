using Godot;
using System;

public partial class HurtboxComponent : Node2D
{
	[Export]
	public HealthComponent healthComponent;

	public bool HasCollision() {
		if (healthComponent?.HasHealthRemaining == true) {
			return true;
		}
		return false;
	}

	public void HandleBulletCollision(float damage) {
		healthComponent?.Damage(damage);
	}
}
