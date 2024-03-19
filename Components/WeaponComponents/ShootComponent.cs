using Godot;
using System;

public partial class ShootComponent : Node2D
{
	[Export]
	public BulletComponent bulletComponent;

	[Export]
	public AmmoComponent ammoComponent;

    public override void _Input(InputEvent @event)
    {
		if (Input.IsActionJustPressed("LeftClick")) {
			if (ammoComponent?.ClipAmmo > 0) {
				ammoComponent.ClipAmmo -= 1;
				Shoot();
			}
			else {
				ammoComponent.Reload();
			}
		}
    }

    public void Shoot() {
		bulletComponent.SpawnBullet();
	}
}
