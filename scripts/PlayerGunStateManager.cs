using Godot;
using System;

public partial class PlayerGunStateManager : Node2D
{
	private IGunState _currentGunState;
	public override void _Ready()
	{
		ChangeToDefaultState();
	}
	
	
	public override void _Process(double delta)
	{
		_currentGunState.HandleShooting();
	}

	public void ChangeState(IGunState newState)
	{
		_currentGunState = newState;
	}

	public void ChangeToDefaultState()
	{
		GD.Print("Swap to default gun");
		_currentGunState = GetNode<Marker2D>("Gun") as IGunState;
	}

	public void ChangeToFastGunState()
	{
		_currentGunState = GetNode<Marker2D>("FastGun") as IGunState;
	}
}
