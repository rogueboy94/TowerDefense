﻿using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static float Money;
    public int startMoney = 150;

    public static int Lives;
    public int startLives = 20;

    public static int Rounds;
    
    void Start()
    {
        Money = startMoney;
        Lives = startLives;

        Rounds = 0;
    }    
}
