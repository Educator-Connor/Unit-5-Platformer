using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the health values of the player and tracks the players health
/// </summary>
public class HealthHandler : MonoBehaviour
{
    public int health;
    public int maxHealth;
    
    // Handles the player losing health, and if they are reduced to 0 health the scene is reloaded.
    public void Damage()
    {
        health--;
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        Debug.Log(health);
    }
    
    // Handles the player gaining health, clamping the amount of health the player can have 
    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        Debug.Log(health);
    }
    
    // Sets health if necessary
    public void SetHealth(int health)
    {
        this.health = health;
    }
    
    // Returns the health value
    public int GetHealth()
    {
        return health;
    }
}
