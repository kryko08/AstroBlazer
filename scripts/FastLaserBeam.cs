using Godot;
using System;

public partial class FastLaserBeam : Node2D
{
	public float BulletSpeed = 3000;
	public int BulletDamage = 15;
	
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
	
	private void OnNotifierScreenExited()
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
