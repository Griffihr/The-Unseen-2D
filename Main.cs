using Godot;
using System;

public partial class Main : Node
{
	
		public Node ServerNode;	
	
	// Called when the node enters the scene tree for the first time.
	
	public override void _Ready()
	{
		var ServerNode = GD.Load<PackedScene>("res://Server.tscn");	
		var _ServerInstance = ServerNode.Instantiate<Node>();
		AddChild(_ServerInstance);	

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
