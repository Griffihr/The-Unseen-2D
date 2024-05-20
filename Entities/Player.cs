using Godot;
using Godot.NativeInterop;
using System;

public partial class Player : CharacterBody2D
{

	private Camera2D camera2D;
	
	[Export]
	public AnimationComponent animationComponent;
	[Export]
	public InputComponent inputComponent;
	[Export]
	public VelocityComponent velocityComponent;
	public override void _Ready()
	{
		camera2D = GetNode<Camera2D>("Camera2D");
		
		if (camera2D.GetMultiplayerAuthority() == Multiplayer.GetUniqueId()) {
			camera2D.MakeCurrent();
		}

	}

	private void SetCamera() {
		if (camera2D.GetMultiplayerAuthority() == Multiplayer.GetUniqueId()) {
			Vector2 pos = GetLocalMousePosition();
			camera2D.Position = (pos / 4);	
		}	
	}


	public override void _PhysicsProcess(double delta)
	{
		if (this.GetMultiplayerAuthority() == Multiplayer.GetUniqueId()) 
		{	
			SetCamera();

			inputComponent.GetInputs(delta);

			animationComponent.UpdateAnimations();
			
			velocityComponent.SetVelocity(delta);
<<<<<<< HEAD
			
=======
>>>>>>> 87a8872a35c24054033efdee13689ac3607d9989
		}
	}
}
