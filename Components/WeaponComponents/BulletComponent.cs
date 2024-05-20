using Godot;
using System;

public partial class BulletComponent : Node2D
{
	[Export]
	public PackedScene bulletScene;
	[Export]
	public float Speed;
	[Export]
	public Marker2D marker2D;

	public float Damage;

	public void SpawnBullet() {
		Bullet _B = bulletScene.Instantiate<Bullet>();
		_B.SpawnBullet(GlobalRotation, Speed, Damage);
		
		_B.Rotation = GlobalRotation;
		_B.Position = marker2D.GlobalPosition;

		Node2D _S = GetNode<Node2D>("/root/Main/SceneManager");
		_S.AddChild(_B);
	}

}
