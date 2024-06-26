using Godot;
using System;

public partial class laserBeam : Node2D
{
	public float BulletSpeed = 2000;
	public int BulletDamage = 20;

	private AnimationPlayer _hitAnimationPlayer;

	public override void _Ready()
	{
		_hitAnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	public override void _PhysicsProcess(double delta)
	{
		var direction = Vector2.Right.Rotated(Rotation);
		Position += direction * BulletSpeed * (float)delta;
	}
	
	
	private void OnVisibleOnScreenNotifier2dScreenExited()
	{
		QueueFree();
	}
	
	private void OnAreaEntered(Area2D area)
	{
		if (area.IsInGroup("Asteroids"))
		{
			var asteroid = area.GetParent() as Asteroid;
			asteroid.TakeDamage(BulletDamage);
			_hitAnimationPlayer.Play("hit_animation");
		}
		
	}
	
}
