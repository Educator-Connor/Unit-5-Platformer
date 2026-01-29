using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This is an interface, it does not derive from monobehaviour
/// we implement this when we have a large number of units that need to process the same information in different ways.
/// So all enemies that implement the Enemy Movement Interface, will NEED to have the Movement Coroutine, and the Idle
/// Coroutine. Alongside the Flip Method.
/// </summary>
public interface IEnemyMovement
{
   
    IEnumerator Movement();
    IEnumerator Idle();
    void Flip();
}
