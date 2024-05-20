using Godot;
using System;

public partial class HurtBoxComponent : Area2D
{
	[Export]
	public HealthComponent healthComponent;

	[Export]
	public CollisionShape2D collisionShape2D;

	public bool HasCollision() {
		if (healthComponent?.HasHealthRemaining == true) {
			return true;
		}
		return false;
	}

	public void HandleBulletCollision(float damage) {
		healthComponent?.Damage(damage);
	}

    public void OnAreaEntered(HitBoxComponent area) {
		
		if (area == null) {
			return;
		}

		HandleBulletCollision(area.Damage);

	}
}
