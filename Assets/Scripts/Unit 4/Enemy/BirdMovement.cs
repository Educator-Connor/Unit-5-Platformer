using System.Collections;
using UnityEngine;

public class BirdMovement : MonoBehaviour, IEnemyMovement
{
    public float idleTime;
    public bool[] idleOnPoint;
    public bool[] facingRight;
    public Transform[] startPoint = new Transform[2];
    public Transform[] targetPoint =  new Transform[2];
    public Transform[] endPoint = new Transform[2];
    public int patrolIndex = 0;
    public float count;
    
    public float movementSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("Movement");
    }
    /// <summary>
    /// An example of a much more complex coroutine, this coroutine uses a mathematical formula to derive a curve from
    /// three points. This gives the enemy a curving motion, based upon the start and the end points, and a control point.
    /// </summary>
    public IEnumerator Movement()
    {
        while (Vector2.Distance(transform.position, endPoint[patrolIndex].position) > 0.1f)
        {
            yield return new WaitForSecondsRealtime(.01f);
            if (count < 1.0f)
            {
                count += movementSpeed * Time.deltaTime;

                Vector3 m1 = Vector3.Lerp(startPoint[patrolIndex].position, targetPoint[patrolIndex].position, count);
                Vector3 m2 = Vector3.Lerp(targetPoint[patrolIndex].position, endPoint[patrolIndex].position, count);
                transform.position = Vector3.Lerp(m1, m2, count);
            }
        }

        if (Vector2.Distance(transform.position, endPoint[patrolIndex].position) < 0.1f)
        {
            Flip();

            if (idleOnPoint[patrolIndex])
            {
                IncrementPatrolIndex();
                count = 0;
                yield return StartCoroutine("Idle");
            }
            else
            {
                IncrementPatrolIndex();
                count = 0;
                yield return StartCoroutine("Movement");
            }
        }
    }

    public void IncrementPatrolIndex()
    {
        if (patrolIndex >= startPoint.Length - 1)
        {
            patrolIndex = 0;
        }
        else
        {
            patrolIndex++;
        }
    }

    public IEnumerator Idle()
    {
        yield return new WaitForSeconds(idleTime);
        yield return StartCoroutine("Movement");
    }

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
}
