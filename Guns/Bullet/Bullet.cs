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

  public void OnTimerTimeout()
  {
    this.QueueFree();
  }

  public void SpawnBullet(float Rotation, float speed, float damage)
  {
    BulletDamage = damage;
    Velocity = new Vector2(speed, 0).Rotated(Rotation);
  }

  public override void _PhysicsProcess(double delta)
  {
    var kc = MoveAndCollide(Velocity);
    
    if (kc != null) {
      this.QueueFree();
    }
  }
}
