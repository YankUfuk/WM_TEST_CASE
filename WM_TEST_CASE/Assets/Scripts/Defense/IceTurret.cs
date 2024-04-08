using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IceTurret : MonoBehaviour
{
    [SerializeField] private LayerMask enemyMask;

    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float attackPerSec = 4;
    [SerializeField] private float freezeTime = 1f;
    private float timeUntilFire;

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / attackPerSec)
        {
            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }

    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovement enemyMovement = hit.transform.GetComponent<EnemyMovement>();
                enemyMovement.UpdateSpeed(0.5f);

                StartCoroutine(ResetEnemySpeed(enemyMovement));
            }
        }
    }
    private IEnumerator ResetEnemySpeed(EnemyMovement enemyMovement)
    {
        yield return new WaitForSeconds(freezeTime);

        enemyMovement.ResetSpeed();
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
