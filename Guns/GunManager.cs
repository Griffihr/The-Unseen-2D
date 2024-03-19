using Godot;
using Godot.Collections;
using System;

public partial class GunManager : Node2D
{

	[Export]
	public Array<PackedScene> _PrimaryGuns { get; set;}

	[Export]
	public string Primary;
	[Export]
	public string Secondary;

	[Export]
	public Resource GunResource;


    public override void _Ready()
    {
		InstantiateGuns();
    }

    public void InstantiateGuns() {
		
	}
}
