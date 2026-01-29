using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillOnCollision : MonoBehaviour
{
    public string sceneToLoad;
    /// <summary>
    /// On entering the trigger 2D, reloads the scene to simulate death.
    /// Historically this would not be handled this way for an arcade game with a life system.
    /// The player would have their last checkpoint loaded, or just be moved to their last checkpoint, and have their health reset.
    /// </summary>
    public void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
