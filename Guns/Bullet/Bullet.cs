using Godot;
using System;

using System.Collections.ObjectModel;
public partial class Bullet : RigidBody2D
{
	private Vector2 Velocity; 

  public void _on_timer_timeout() {
		this.QueueFree();
	}

  public void SpawnBullet(float Rotation, float speed) {
	Velocity = new Vector2(speed, 0).Rotated(Rotation);
  }

  public override void _PhysicsProcess(double delta)
  {
	  KinematicCollision2D _KC = MoveAndCollide(Velocity);

	  if (_KC != null) {
		this.QueueFree();
	  } 
	  
  }
}
