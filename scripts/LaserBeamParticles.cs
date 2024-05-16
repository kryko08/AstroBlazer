using Godot;
using System;

public partial class LaserBeamParticles : CpuParticles2D
{
	
	public override void _Ready()
	{
		Emitting = true;
	}
	
}
