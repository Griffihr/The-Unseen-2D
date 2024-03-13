using Godot;
using System;

public partial class Camera : Camera2D
{

	public CharacterBody2D _CharacterBody2D;
	public Vector2 _Vector2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_CharacterBody2D = this.GetParent<CharacterBody2D>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		/*
		_Vector2 = _CharacterBody2D.GetLocalMousePosition();
		this.Offset = (_Vector2 / 3);
		*/
	}
}
