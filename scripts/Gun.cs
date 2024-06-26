using Godot;
using System;

public partial class Gun : Marker2D, IGunState
{
	public float GunCoolDown => 200;
	public PackedScene Beam { get; private set; }
	public AudioStreamPlayer2D Sound { get; set; }
	public ulong LastShotAt { get; set; }
	private AudioStreamPlayer2D _soundPlayer;
	public override void _Ready()
	{
		Beam = GD.Load<PackedScene>("res://scenes/beams/laserBeam.tscn");
		LastShotAt = Time.GetTicksMsec();
		_soundPlayer = GetNode<AudioStreamPlayer2D>("StreamPlayer");
	}

	
	private bool IsShooting()
	{
		if (Input.IsActionPressed("click"))
		{
			return true;
		}

		return false;
	}

	private void Shoot()
	{
		var instance = Beam.Instantiate() as Node2D;
		GetTree().Root.AddChild(instance);
		instance.GlobalPosition = GlobalPosition;
		instance.Rotation = GlobalRotation;
		
		LastShotAt = Time.GetTicksMsec();
	}

	public void HandleShooting()
	{
		if (IsShooting() && Time.GetTicksMsec() > LastShotAt + GunCoolDown)
		{
			Shoot();
			_soundPlayer.Play();
		}
	}
}
