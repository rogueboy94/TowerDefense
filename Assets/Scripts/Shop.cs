using System;
using UnityEngine;

public class Shop : MonoBehaviour {

    public TurretBluePrint standardTurret;
    public TurretBluePrint rocketLauncer;
    public TurretBluePrint laserBeamer;
    BuildManager buildManager;
    
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        Debug.Log("Покупка!");
        buildManager.SelectTurretToPurchase(standardTurret);
    }

    public void SelectRocketLauncher()
    {
        Debug.Log("Покупка ракетницы!");
        buildManager.SelectTurretToPurchase(rocketLauncer);
    }

    public void SelectLaserBeamer()
    {
        Debug.Log("Покупка лазерной пушки!");
        buildManager.SelectTurretToPurchase(laserBeamer);
    }
}
