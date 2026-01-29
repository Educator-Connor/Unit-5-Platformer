using System;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    public float knockbackForce;
    public float destroyTime;
    /// <summary>
    ///On entering the trigger above the enemy head, if it is the player it will destroy the enemy
    ///</summary>
    public void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.CompareTag("Player"))
        {
            trig.gameObject.GetComponent<UpdatedPlayerController>().Knockback(this.gameObject, knockbackForce);
            Debug.Log("Enemy Destroyed");
            Destroy(gameObject, destroyTime);
        }
    }

    /// <summary>
    /// On entering the collider of the enemy, the player will be knocked back, using the Knockback() method on the
    /// player. Applying the damage.
    ///</summary>
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<UpdatedPlayerController>().Knockback(this.gameObject, knockbackForce);
            col.gameObject.GetComponent<HealthHandler>().Damage();
            Debug.Log("Hit Player");
        }
    }
}
