using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "$" + PlayerStats.Money.ToString();
	}
}
