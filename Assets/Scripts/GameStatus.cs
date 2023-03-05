using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStatus : MonoBehaviour
{
    public static bool isGameOver;
    public static int actualGameEnemiesKilled;
    /*
        0 => On Game
        1 => Pause
        2 => Game Over
    */
    public static int focus;
    public static int maxEnemiesKilled;
    
    // Start is called before the first frame update
    void Start(){
        maxEnemiesKilled = 0;
        actualGameEnemiesKilled = 0;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update(){
        ;
    }

    public static void UpdateScore(){
        maxEnemiesKilled = Math.Max(maxEnemiesKilled, actualGameEnemiesKilled);
    }

    public static void RestartGame(){
        actualGameEnemiesKilled = 0;
        isGameOver = false;
        focus = 0;
    }
}
