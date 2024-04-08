using UnityEngine;

public class Turret : BaseTurret 
{
    public override void FindTarget() // Overrides the FindTarget method defined in the base class
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask); // Casts a circle from the turret's position to detect enemies within its range and stores the results in the hits array

        if (hits.Length > 0) // Checks if there are any enemies detected
        {
            target = hits[0].transform; // Sets the first detected enemy as the target
        }
    }

    public override bool CheckTargetIsInRange() // Overrides the CheckTargetIsInRange method defined in the base class
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange; // Returns true if the distance between the turret and the target is less than or equal to the targeting range
    }

    public override void RotateTowardsTarget() // Overrides the RotateTowardsTarget method defined in the base class
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f; // Calculates the angle to rotate the turret towards the target
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle)); // Creates a quaternion representing the target rotation
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Rotates the turret towards the target rotation
    }

    public override void Shoot() // Overrides the Shoot method defined in the base class
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity); // Instantiates a bullet object at the firing point
        Bullet bulletScript = bulletObj.GetComponent<Bullet>(); // Retrieves the Bullet component attached to the bullet object
        bulletScript.SetTarget(target); // Sets the target for the bullet
    }
}


