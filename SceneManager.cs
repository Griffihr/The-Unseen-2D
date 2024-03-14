using Godot;
using System;
using System.Data.Common;
public partial class SceneManager : Node2D
{
	
	[Export]
	private PackedScene PlayerScene;
	public override void _Ready()
	{
		int Index = 0;

		foreach (var Item in GameManager.Players) {
			Player _CurrentPlayer = PlayerScene.Instantiate<Player>();
			_CurrentPlayer.Name = Item.Id.ToString();
			AddChild(_CurrentPlayer);

			foreach (Node2D SpawnPoint in GetTree().GetNodesInGroup("SpawnPoints")) {
				if (int.Parse(SpawnPoint.Name) == Index) {
					
					_CurrentPlayer.GlobalPosition = SpawnPoint.GlobalPosition;
				}
			}

			Index++;
			
 		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
