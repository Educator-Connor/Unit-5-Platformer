using System;
using UnityEngine;
/// <summary>
/// The pickup abstract class derives from monobehaviour, which means all classes that inherit from it will also
/// inherit from monobehaviour. The function of this class is to create a script that all pickups can inherit from that
/// shares the same underlying logic, without the need to change that logic.
/// </summary>
public abstract class Pickup : MonoBehaviour
{
   public int value;

   public virtual void OnTriggerEnter2D(Collider2D trig)
   {
      Debug.Log("Pickup: " + value);
      Destroy(this.gameObject);
   }
}
