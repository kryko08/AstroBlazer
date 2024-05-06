using Godot;
using System;

public partial class Gun : Marker2D
{
	private PackedScene _laserBeam;
	private Marker2D _gun;

	private float _gunCoolDown = 300;
	private ulong _lastShotAt;
	public override void _Ready()
	{
		_laserBeam = GD.Load<PackedScene>("res://scenes/laserBeam.tscn");
		_lastShotAt = Time.GetTicksMsec();
	}

	
	public override void _PhysicsProcess(double delta)
	{
		
		if (IsShooting() && Time.GetTicksMsec() > _lastShotAt + _gunCoolDown)
		{
			Area2D instance = _laserBeam.Instantiate() as Area2D;
			GetTree().Root.AddChild(instance);
			instance.GlobalPosition = GlobalPosition;
			instance.Rotation = GlobalRotation;
			
			_lastShotAt = Time.GetTicksMsec();
		}
	}

	private bool IsShooting()
	{
		if (Input.IsActionPressed("click"))
		{
			return true;
		}

		return false;
	}
}
