using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// On entering an area with the ladder script it accesses the player enabling vertical movement.
/// </summary>
public class Ladder : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Player"))
        {
            trig.gameObject.GetComponent<UpdatedPlayerController>().SetOnLadder(true);
        }
    }

    public void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Player"))
        {
            trig.gameObject.GetComponent<UpdatedPlayerController>().SetOnLadder(false);
        }
    }
}
