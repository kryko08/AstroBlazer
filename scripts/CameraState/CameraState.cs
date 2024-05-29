using Godot;

namespace AstroBlazer.scripts.CameraState;

public abstract class CameraState
{
    public CameraMovement _context;

    public void SetContext(CameraMovement context)
    {
        this._context = context;
    }

    public abstract void onProcess(double delta);
}

public class CameraMovingState : CameraState
{
    public override void onProcess(double delta)
    {
        var velocity = new Vector2();
        velocity.Y += -1;
        velocity = velocity * _context.GlabalVars.GameSpeed;
        _context.Position += velocity * (float)delta;
    }
}

public class CameraStoppedState : CameraState
{
    public override void onProcess(double delta)
    {
        _context.Position = _context.Position;
    }
}