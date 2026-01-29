using System;
using UnityEngine;
/// <summary>
/// The Game Manager is a singleton script, meaning only one should exist. This script tracks and adds to the score of
/// the player. The Game Manager normally has more expansive functionality that is game dependent that will be discussed
/// in future videos.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Retrieves the score
    public int GetScore()
    {
        return score;
    }

    // Adds a unspecified value to the score allowing for a variety of pickups that can increase the score
    public void AddScore(int score)
    {
        this.score += score;
    }
}
