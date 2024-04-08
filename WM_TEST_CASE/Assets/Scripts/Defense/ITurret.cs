// Interface for controlling a turret.
public interface ITurret
{
    void FindTarget(); // Finds the target to aim at.
    void RotateTowardsTarget(); // Rotates the turret towards the target.
    void Shoot(); // Fires a shot at the target.
    bool CheckTargetIsInRange(); // Checks if the target is within range.
}

