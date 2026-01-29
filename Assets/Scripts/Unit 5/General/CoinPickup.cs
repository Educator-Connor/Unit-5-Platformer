using UnityEngine;

public class CoinPickup : Pickup
{
   /// <summary>
   /// Overriding the trigger enter, you can see we are changing the inherent logic to be used to show we picked
   /// up a coing pickup that will improve our score.
   /// </summary>
   public override void OnTriggerEnter2D(Collider2D trig)
   {
      if (trig.gameObject.CompareTag("Player"))
      {
         GameManager.Instance.AddScore(value);
         Debug.Log("Score " + GameManager.Instance.GetScore());
         Destroy(this.gameObject);
      }
   }
}
