using UnityEngine;
using UnityEditor;

// This is class is a blueprint for turrets
public abstract class BaseTurret : MonoBehaviour, ITurret
{
    [SerializeField] protected LayerMask enemyMask; // Checks enemy layer

    // Turret attributes
    [SerializeField] protected float targetingRange = 5f; 
    [SerializeField] protected float rotationSpeed = 5f; 
    [SerializeField] protected float bulletPerSec = 1f;

    [SerializeField] protected Transform turretRotationPoint;
    [SerializeField] protected Transform firingPoint; 

    [SerializeField] protected GameObject bulletPrefab; 
    

    

    protected Transform target; // The current target of the turret
    protected float timeUntilFire; // The time until the turret can fire again

    // Method to find the target
    public abstract void FindTarget();

    // Method to rotate the turret towards the target
    public abstract void RotateTowardsTarget();

    // Method to perform shooting
    public abstract void Shoot();

    // Method to check if the target is within range
    public abstract bool CheckTargetIsInRange();

    protected virtual void Update()
    {
        // If there's no target, try to find one
        if (target == null)
        {
            FindTarget();
            return;
        }

        // Rotate towards the target
        RotateTowardsTarget();

        // If the target is out of range, reset the target
        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            // Update time until firing
            timeUntilFire += Time.deltaTime;

            // If it's time to fire, shoot
            if (timeUntilFire >= 1f / bulletPerSec)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    // Draw gizmos to visualize the targeting range
    protected virtual void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}


