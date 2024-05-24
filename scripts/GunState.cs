using Godot;

public interface IGunState
{
	void HandleShooting();
	
	float GunCoolDown { get; }
	ulong LastShotAt { get; set; }
	
	PackedScene Beam { get; }
	
	AudioStreamPlayer2D Sound { get; set; }
}
