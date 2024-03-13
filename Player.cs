using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 150.0f;
	public float SprintSpeed = 0.0f;
	public const float JumpVelocity = -400.0f;

	public AnimatedSprite2D _AnimatedSprite2D;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		_AnimatedSprite2D = this.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").SetMultiplayerAuthority(int.Parse(Name));


		_AnimatedSprite2D.Play("Idle");
	}

	public override void _Input(InputEvent @event)
	{

	}

	public void UpdateAnimations(float Direction) {
		if (IsOnFloor()) {
			if (Direction == 0) {
				_AnimatedSprite2D.Play("Idle");
			}
			if (Direction < 0) {
				_AnimatedSprite2D.Play("Running");
				_AnimatedSprite2D.FlipH = true;
			}
			if (Direction > 0) {
				_AnimatedSprite2D.Play("Running");
				_AnimatedSprite2D.FlipH = false;
			}
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").GetMultiplayerAuthority() == Multiplayer.GetUniqueId()) 
		{	
			Vector2 velocity = Velocity;

			//var _MousePosition = GetGlobalMousePosition();
			var _MousePosition = this.GetLocalMousePosition();

			// Add the gravity.
			if (!IsOnFloor())
				velocity.Y += gravity * (float)delta;

			// Handle Jump.
			if (Input.IsActionJustPressed("jump") && IsOnFloor())
				velocity.Y = JumpVelocity;

			if (Input.IsActionPressed("sprint")) 
			{
				SprintSpeed = 100;
			}
			else
			{
				SprintSpeed = 0;
			}
			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.

			var HorizontalDirection	= Input.GetAxis("left", "right");
			velocity.X = (Speed + SprintSpeed) * HorizontalDirection;

			UpdateAnimations(HorizontalDirection);	

			Velocity = velocity;
			MoveAndSlide();
		}
	}
}