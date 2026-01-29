using UnityEngine;

public class HealthPickup : Pickup
{
   /// <summary>
   /// Overriding the trigger enter, you can see we are changing the inherent logic to be used to show we picked
   /// up a health pickup.
   /// </summary>
   public override void OnTriggerEnter2D(Collider2D trig)
   {
      if (trig.gameObject.CompareTag("Player"))
      {
         trig.GetComponent<HealthHandler>().Heal(value);
         Debug.Log("Health: " + trig.GetComponent<HealthHandler>().health);
      }
   }
}
