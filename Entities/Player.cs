using Godot;
using Godot.NativeInterop;
using System;

public partial class Player : CharacterBody2D
{

	private AnimatedSprite2D _AnimatedSprite2D;
	private Camera2D _Camera2D;
	
	[Export]
	public InputComponent inputComponent;
	[Export]
	public VelocityComponent velocityComponent;
	public override void _Ready()
	{
		_AnimatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_Camera2D = GetNode<Camera2D>("Camera2D");
		
		GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").SetMultiplayerAuthority(int.Parse(Name));
		_Camera2D.SetMultiplayerAuthority(int.Parse(Name));

		if (_Camera2D.GetMultiplayerAuthority() == Multiplayer.GetUniqueId()) {
			_Camera2D.MakeCurrent();
		}

		_AnimatedSprite2D.Play("Idle");
	}

	public void UpdateAnimations(float Direction) {
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

    public override void _PhysicsProcess(double delta)
	{
		if (GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").GetMultiplayerAuthority() == Multiplayer.GetUniqueId()) 
		{	
			
			inputComponent.GetInputs(delta);
			
			velocityComponent.SetVelocity(delta);
		
		}
	}
}
