using Godot;
using System;
using System.ComponentModel;

public partial class Pistol : Node2D
{

  public override void _PhysicsProcess(double delta)
  {
    LookAt(GetGlobalMousePosition());
  }
}