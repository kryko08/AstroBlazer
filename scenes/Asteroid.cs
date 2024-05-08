using Godot;
using System;

public partial class Asteroid : Area2D
{
	private int _health;
	public int AsteroidSpeed;
	
	
	public override void _Ready()
	{

		AsteroidSpeed = 30;
		_health = 60;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var direction = Vector2.Right.Rotated(Rotation);
		Position += direction * AsteroidSpeed * (float)delta;
		CheckHealth();
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
		QueueFree();
	}

	private void TakeDamage(int damage)
	{
		_health -= damage;
	}
	
	private void OnAreaEntered(Area2D area)
	{
		if (area.IsInGroup("Beam"))
		{
			var script = area as laserBeam;
			var damage = script.BulletDamage;
			TakeDamage(damage);
			area.QueueFree();
		} 
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			var script = body as PlayerMovement;
			script.AsteroidCollision();
		}
	}
	
}



