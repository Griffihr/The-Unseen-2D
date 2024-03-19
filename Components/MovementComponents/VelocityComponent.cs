using Godot;
using System;
using System.Data.Common;

public partial class VelocityComponent : Node2D
{
	[Export]
	public float Speed;
	[Export]
	public float JumpSpeed;
	[Export]
	public float RunSpeed;
	[Export]
	public float CrouchSpeed;
	[Export]
	public CharacterBody2D Character;
	[Export]
	public InputComponent Input;
	public Vector2 velocity;
	private float SpeedModifier;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	public void SetVelocity(double delta) {
		velocity = Character.Velocity;
		SpeedModifier = 0;
		
		if (!Character.IsOnFloor()) {
			velocity.Y += gravity * (float)delta;
		}

		if (Input.GetJumpInput() && Character.IsOnFloor()) {
			velocity.Y = JumpSpeed;
		}

		if (Input.GetRunInput()) {
			SpeedModifier += RunSpeed;
		}
		else if(Input.GetCrouchInput()) {
			SpeedModifier -= CrouchSpeed;
		}	

		var Direction = Input.GetMoveInput();
		if (Direction != 0) {
			velocity.X = Direction * (Speed + SpeedModifier);
		}
		else {
			velocity.X = Mathf.MoveToward(velocity.X, 0, Speed);
		}

		Character.Velocity = velocity;
		Character.MoveAndSlide();
	}
}
