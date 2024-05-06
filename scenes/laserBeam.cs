using Godot;
using System;

public partial class laserBeam : Area2D
{
	public float BulletSpeed = 2000;
	public int BulletDamage = 20;


	public override void _PhysicsProcess(double delta)
	{
		var direction = Vector2.Right.Rotated(Rotation);
		Position += direction * BulletSpeed * (float)delta;
	}
	
	
	private void OnBodyEntered(Node2D body)
	{
		// GD.Print(body.Name);
	}
	
	private void OnVisibleOnScreenNotifier2dScreenExited()
	{
		QueueFree();
	}
	
}


