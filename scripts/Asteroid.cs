using Godot;
using System;

public partial class Asteroid : Node2D
{
	private int _health = 60;
	public int AsteroidSpeed = 30;

	public Global GlobalVars;
	
	private AnimationPlayer _hitFlashAnimationPlayer;
	private AnimationPlayer _destroyAnimationPlayer;
	
	
	public override void _Ready()
	{
		
		GlobalVars = GetNode<Global>("/root/GlobalVars");
		_hitFlashAnimationPlayer = GetNode<AnimationPlayer>("HitFlashAnimationPlayer");
		_destroyAnimationPlayer = GetNode<AnimationPlayer>("DestroyAnimationPlayer");
	}
	
	public override void _Process(double delta)
	{
		MoveAsteroid(delta);
	}

	private void CheckHealth()
	{
		if (_health <= 0)
		{
			DestroySelf();
		}
	}

	private void DestroySelf()
	{
		GlobalVars.IncreasePlayerScore();
		_destroyAnimationPlayer.Play("destroy");
	}

	public void TakeDamage(int damage)
	{
		_health -= damage;
		_hitFlashAnimationPlayer.Play("hit_flash");
		CheckHealth();
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			var script = body as PlayerMovement;
			script.AsteroidCollision();
		}
	}
	
	private void OnAsteroidNotifierScreenExited()
	{
		QueueFree();
	}
	

	private void MoveAsteroid(double delta)
	{
		var direction = Vector2.Right.Rotated(Rotation);
		Position += direction * AsteroidSpeed * (float)delta;
	}
	
}
