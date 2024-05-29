using Godot;

namespace AstroBlazer.scripts.SpawnerState;

public abstract class SpawnerState
{
	public Spawner _context;

	public void SetContext(Spawner context)
	{
		this._context = context;
	}

	public abstract void OnProcess(double delta);
}

public class SpawnerMovingState : SpawnerState
{
	public override void OnProcess(double delta)
	{
		var velocity = new Vector2();
		velocity.Y += -1;
		velocity = velocity * _context.GlobalVars.GameSpeed;
		_context.Position += velocity * (float)delta;
	}
}

public class SpawnerStoppedState : SpawnerState
{
	public override void OnProcess(double delta)
	{
		_context.Position = _context.Position;
	}
}
