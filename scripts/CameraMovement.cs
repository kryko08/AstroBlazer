using Godot;
using System;
using AstroBlazer.scripts.CameraState;

public partial class CameraMovement : Camera2D
{
	public Global GlabalVars;
	
	// States 
	private CameraMovingState _movingState;
	private CameraStoppedState _stoppedState;

	private CameraState _currentState;
	
	
	// Signals
	[Signal]
	public delegate void CameraPositionChangedEventHandler(Vector2 position);

	public override void _Ready()
	{
		GlabalVars = GetNode<Global>("/root/GlobalVars");

		_movingState = new CameraMovingState();
		_movingState.SetContext(this);

		_stoppedState = new CameraStoppedState();
		_stoppedState.SetContext(this);

		_currentState = _stoppedState;
	}

	public override void _Process(double delta)
	{
		_currentState.onProcess(delta);
	}

	public void StartMovingCamera()
	{
		_currentState = _movingState;
	}

	public void StopMovingCamera()
	{
		_currentState = _stoppedState;
	}

}
