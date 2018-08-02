using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour {

    public Text livesText;
    
	// Update is called once per frame
	void Update () {
        StartCoroutine(Lives());        
    }

    IEnumerator Lives()
    {
        yield return livesText.text = "HP: " + PlayerStats.Lives;
    }
}
