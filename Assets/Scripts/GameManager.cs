using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool GameIsOver;
	public static bool GameWon;
	public GameObject gameOverUI;
	public GameObject WinUI;


    void Start()
    {
        GameIsOver = false;
    }

	// Update is called once per frame
	void Update () {
        if (GameIsOver)
            return;

		if(PlayerStats.Lives <= 0)
            EndGame();

		if (GameWon)
			WinUI.SetActive (true);
	}

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
