using Godot;
using System;

public partial class Asteroid : Area2D
{
	private int _health;
	
	
	public override void _Ready()
	{
		_health = 60;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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



