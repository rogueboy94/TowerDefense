using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class Cost : MonoBehaviour {
    
    List<int> shop = new List<int>();

    [Header("Assign index for a turret.")]
    [Tooltip("StandardTurret = 1 \nRocketLauncher = 2 \nLaserBeamer = 3")]
    [Range(1, 4)]
    public int TurretNumber;

    // Use this for initialization
    void OnEnable ()
    {
        shop.Add(GetComponentInParent<Shop>().standardTurret.cost);
        shop.Add(GetComponentInParent<Shop>().rocketLauncer.cost);
        shop.Add(GetComponentInParent<Shop>().laserBeamer.cost);

        GetComponent<Text>().text = "$" + shop[TurretNumber - 1];
	}

}
