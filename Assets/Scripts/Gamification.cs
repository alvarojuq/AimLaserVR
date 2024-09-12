using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamification : MonoBehaviour
{
    //Used to easily reference the script in other scripts
    public static Gamification instance;

    // Keeps track of misses and hits
    private int hitCount, missCount;

    //Determines if the timer should be counting up or not
    private bool timerOn = true;
    //Keeps track of time in seconds
    private float timer;

    //Calculated based on users performance to determine how well they did
    public int score;

    private void Awake()
    {
        // Ensures only one gamification script exists in a scene
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Timer
    private void Update()
    {
        // Timer that counts up
        if(timerOn && UserMenu_Simulation.SimIsPaused.Equals(false))
        timer += Time.deltaTime;
    }

    public void SetTimer(bool value)
    {
        // Enables or disables the timer
        timerOn = value;
    }
    public float TimeSpent()
    {
        // Get the time spent on that round
        return timer;
    }

    // Hit / Miss
    public void Hit()
    {
        //Adds a hit to the hitcounter when laser hits the target
        if(timerOn)
            hitCount++;
    }
    public void Miss()
    {
        //Adds a miss to the misscounter when laser is off target
        if (timerOn)
            missCount++;
    }
    public float HitPercentage()
    {
        //Calculates the hit percentage/accuracy of the laser
        return ((float)hitCount / (hitCount + missCount)) * 100;
    }

    // Score
    public void NextBoard()
    {
        // Adds that rounds score to total score and resets the variables for the next round
        AddScore();
        hitCount = 0;
        missCount = 0;
        timer = 0;
    }

    public void AddScore()
    {
        // Calculates the score for the round and adds it to the total
        score += (int)((HitPercentage() * 3));
    }

    public string Grade()
    {
        if (score <= 750)
        {
            return "F";
        }
        else if (score <= 950)
        {
            return "D";
        }
        else if (score <= 1150)
        {
            return "C";
        }
        else if (score <= 1350)
        {
            return "B";
        }
        else
        {
            return "A";
        }

    }
}
