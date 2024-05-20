using Godot;
using System;

using System.Collections.ObjectModel;
public partial class Bullet : Godot.CharacterBody2D
{

  [Export]
  public HitBoxComponent hitBoxComponent;
  public float BulletDamage
  {
    get { return bulletDamage; }
    set { bulletDamage = value; }
  }
  private float bulletDamage;

<<<<<<< HEAD
  public void OnTimerTimeout()
  {
    this.QueueFree();
  }

  public void SpawnBullet(float Rotation, float speed, float damage)
  {
    BulletDamage = damage;
    Velocity = new Vector2(speed, 0).Rotated(Rotation);
=======
  public void SpawnBullet(float Rotation, float speed) {
	Velocity = new Vector2(speed, 0).Rotated(Rotation);
>>>>>>> 87a8872a35c24054033efdee13689ac3607d9989
  }

  public override void _PhysicsProcess(double delta)
  {
<<<<<<< HEAD
    var kc = MoveAndCollide(Velocity);
    
    if (kc != null) {
      this.QueueFree();
    }
=======
	  KinematicCollision2D _KC = MoveAndCollide(Velocity);

	  if (_KC != null) {
		this.QueueFree();
	  } 
	  
>>>>>>> 87a8872a35c24054033efdee13689ac3607d9989
  }
}
