using Godot;
using System;

public partial class Asteroid : Area2D
{
	private int _health;
	public int AsteroidSpeed;

	public Global GlobalVars;

	private PackedScene _beamParticles = GD.Load<PackedScene>("res://scenes/LaserBeamParticles.tscn");

	private AudioStreamPlayer2D _audioStreamPlayer2D;
	private CollisionShape2D _collisionShape;
	private Sprite2D _sprite;
	
	public override void _Ready()
	{

		AsteroidSpeed = 30;
		_health = 60;
		
		GlobalVars = GetNode<Global>("/root/GlobalVars");
		
		_audioStreamPlayer2D = GetNode<AudioStreamPlayer2D>("StreamPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
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
			DestroyBeam(area);
			CreateParticle();
			CheckHealth();
			
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
	
	private void OnAsteroidNotifierScreenExited()
	{
		QueueFree();
	}

	private void DestroyBeam(Area2D area)
	{
		var script = area as laserBeam;
		var damage = script.BulletDamage;
		TakeDamage(damage);
		area.QueueFree();
	}

	private void CreateParticle()
	{
		var particleInstance = _beamParticles.Instantiate() as CpuParticles2D;
		GetTree().Root.AddChild(particleInstance);
		particleInstance.GlobalPosition = GlobalPosition;
	}

	private void MoveAsteroid(double delta)
	{
		var direction = Vector2.Right.Rotated(Rotation);
		Position += direction * AsteroidSpeed * (float)delta;
	}
	
}






