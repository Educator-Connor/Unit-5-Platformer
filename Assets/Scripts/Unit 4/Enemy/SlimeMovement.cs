using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class SlimeMovement : MonoBehaviour, IEnemyMovement
{
    public float idleTime;
    public bool[] idleOnPoint;
    public bool[] facingRight;
    public Transform[] patrolPoints;
    public int patrolIndex = 0;
    
    public float movementSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("Movement");
    }
    
    /// <summary>
    /// The movement coroutine transforms the position of the enemy from patrol point to patrol point.
    /// When it reaches a reasonable proximity to the patrol point if the enemy is set to idle on the point it will.
    /// This parallel array is setup so that if we want a smooth transition from a slope and we do not want the enemy
    /// to idle at any point along the slope it won't unless idle on point is true.
    /// We control flipping aswell with a parallel array, as to get predictable reactions.
    /// </summary>
    public IEnumerator Movement()
    {
        while (Vector2.Distance(transform.position, patrolPoints[patrolIndex].position) > 0.1f)
        {
            yield return new WaitForSecondsRealtime(.01f);
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[patrolIndex].position,
                movementSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, patrolPoints[patrolIndex].position) < 0.1f)
        {
            Flip();
           

            if (idleOnPoint[patrolIndex])
            {
                IncrementPatrolIndex();
                yield return StartCoroutine("Idle");
            }
            else
            {
                IncrementPatrolIndex();
                yield return StartCoroutine("Movement");
            }
            
        }
    }

    /// <summary>
    /// Increases the patrol index upon reaching the location of the current patrol point.
    /// </summary>
    public void IncrementPatrolIndex()
    {
        if (patrolIndex >= patrolPoints.Length - 1)
        {
            patrolIndex = 0;
        }
        else
        {
            patrolIndex++;
        }
    }

    /// <summary>
    /// Flips the sprite left and right depending on the designation by the array.
    /// </summary>
    public void Flip()
    {
        if (facingRight[patrolIndex])
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    
    /// <summary>
    /// The idle coroutine allows the enemy to sit on a patrol point for a period of time.
    /// It then calls the movement coroutine to continue the loop.
    /// </summary>
    public IEnumerator Idle()
    {
        yield return new WaitForSeconds(idleTime);
        yield return StartCoroutine("Movement");
    }
}
