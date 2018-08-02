using UnityEngine;

[System.Serializable]
public class TurretBluePrint{
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;
    
    public float GetSellAmount()
    {
        return cost / 2;
    }

    public int Cost()
    {
        return cost;
    }
}
