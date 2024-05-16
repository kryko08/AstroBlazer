using Godot;
using System;
using System.Diagnostics.Contracts;

public partial class BorderCheck : Node2D
{
	public CharacterBody2D Player;
	public Global GlobalVars;
	public VisibleOnScreenNotifier2D notifier;

	public ulong LastInSight;
	public bool IsVisible;
	
	[Signal]
	public delegate void TimeOutsideScreenEventHandler(ulong timeLeft);
	[Signal]
	public delegate void OnScreenEnterHudEventHandler();
	[Signal]
	public delegate void OnScreenExitedHudEventHandler();
	public override void _Ready()
	{
		Player = GetNode<CharacterBody2D>("/root/Player");
		GlobalVars = GetNode<Global>("/root/GlobalVars");

		notifier = Player.GetNode<VisibleOnScreenNotifier2D>("tst");
		notifier.ScreenEntered += OnScreenEntered;
		notifier.ScreenExited += OnScreenExited;

		IsVisible = true;
	}

	void OnScreenEntered()
	{
		IsVisible = true;
		EmitSignal(SignalName.OnScreenEnterHud);
	}

	void OnScreenExited()
	{
		IsVisible = false;
		LastInSight = Time.GetTicksMsec();
		EmitSignal(SignalName.OnScreenExitedHud);
	}

	public override void _Process(double delta)
	{
		if (IsVisible == false)
		{
			var currentTime = Time.GetTicksMsec();
			var timeLeft = 10000 - (currentTime - LastInSight);
			EmitSignal(SignalName.TimeOutsideScreen, timeLeft);
			
			if (currentTime - LastInSight > 10000)
			{
				GlobalVars.GameOver();
			}
		}
	}
}
