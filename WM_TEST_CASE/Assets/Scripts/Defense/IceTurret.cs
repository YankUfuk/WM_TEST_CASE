using System.Collections;
using UnityEngine;

// Note: Shooting can be added but it changes tank type so using it depends on the game developers decision
public class IceTurret : BaseTurret
{
    // Ice Turet attributes
    [SerializeField] private float freezeTime = 1f;
    [SerializeField] private float attackPerSec = 4;

    // Find the nearest target within rang
    public override void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / attackPerSec)
        {
            FreezeEnemies(); 
            timeUntilFire = 0f;
        }
    }

    // Check if the current target is within range
    public override bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    // Freeze enemies within the turret's range
    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovement enemyMovement = hit.transform.GetComponent<EnemyMovement>(); // Get enemy movement script
                enemyMovement.UpdateSpeed(0.5f); // Slow down enemy movement speed

                StartCoroutine(ResetEnemySpeed(enemyMovement)); // Coroutine to reset enemy speed
            }
        }
    }

    // Rotate the turret towards the target
    public override void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Rotate turret
    }

    // Shoot at the target.
    public override void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity); // Instantiate bullet
        Bullet bulletScript = bulletObj.GetComponent<Bullet>(); // Get bullet script
        bulletScript.SetTarget(target); // Set bullet target
    }

    // Reset enemy speed after a certain delay
    private IEnumerator ResetEnemySpeed(EnemyMovement enemyMovement)
    {
        yield return new WaitForSeconds(freezeTime); // Wait for freeze time

        enemyMovement.ResetSpeed(); 
    }
}



