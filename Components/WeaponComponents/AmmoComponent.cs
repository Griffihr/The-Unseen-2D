using Godot;
using System;

public partial class AmmoComponent : Node
{

	[Export]
	public int CurrentAmmo {
		get => currentAmmo;
		set {
			currentAmmo = value;
		}
	}
	[Export]
	public int MaxAmmo {
		get => maxAmmo;
		set {
			maxAmmo = value;
		}
	}
	[Export]
	public int ClipAmmo {
		get => clipAmmo;
		set {
			clipAmmo = value;
		}
	}
	[Export]
	public int ClipMaxAmmo {
		get => clipMaxAmmo;
		set {
			clipMaxAmmo = value;
		}
	}
	[Export]
	public Timer _GunCooldown;

	private int currentAmmo;
	private int maxAmmo;
	private int clipAmmo;
	private int clipMaxAmmo;

	public void Reload() {
		if (currentAmmo >= clipMaxAmmo) {
			currentAmmo -= (clipMaxAmmo - clipAmmo);
			clipAmmo = clipMaxAmmo;
		}
		else {
			clipAmmo = currentAmmo;
			currentAmmo = 0;
		}
	}

	public void AmmoPickup(int Ammo ) {
		currentAmmo = Ammo;
		if (currentAmmo > maxAmmo) {
			currentAmmo = maxAmmo;
		}
	}

	public void InitializeAmmo(int Ammo) {
		CurrentAmmo = Ammo;
	}

}
