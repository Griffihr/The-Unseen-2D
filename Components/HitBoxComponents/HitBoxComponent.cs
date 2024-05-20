using Godot;
using System;

public partial class HitBoxComponent : Area2D
{

	public float Damage {
		get { return damage; }
		set { damage = value; }
	}
	
	private float damage;

}
