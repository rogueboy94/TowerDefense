using UnityEngine.EventSystems;
using UnityEngine;

public class Node : MonoBehaviour {

    public Color hoverColor;
    public Color notEnoughMoney;
    public Vector3 positionOffset;


    [Header("Optional")]
    public GameObject turret;
    public TurretBluePrint turretBluePrint;
    public bool isUpgraded = false;

    private Renderer rend;
    private Color nodeColor;

    BuildManager buildManager;


    void Start()
    {
        rend = GetComponent<Renderer>();
        nodeColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (turret == null)
            buildManager.DeselectNode();

        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
        {
            Debug.Log("CANT");
            return;
        }
        BuildTurret(buildManager.GetTurretToBuild());

        //
    }

    void BuildTurret(TurretBluePrint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("NO MONEY!!!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        //Instantiate Turret
        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        //Instantiate Effect
        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 1f);

        turretBluePrint = blueprint;

        buildManager.EmptyTurretSelection();

        rend.material.color = notEnoughMoney;
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBluePrint.upgradeCost)
        {
            Debug.Log("NO MONEY!!!");
            return;
        }

        PlayerStats.Money -= turretBluePrint.upgradeCost;

        //Get rid of the old turret
        Destroy(turret);

        //Build a new turret
        GameObject _turret = Instantiate(turretBluePrint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        //Instantiate Effect
        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 1f);

        isUpgraded = true;

        buildManager.EmptyTurretSelection();

        Debug.Log("Turret built!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBluePrint.GetSellAmount();

        Destroy(turret);
        turretBluePrint = null;
    }

	void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;
        else
            rend.material.color = hoverColor;

        if (turret != null)
            rend.material.color = notEnoughMoney;
    }

    void OnMouseExit()
    {
        rend.material.color = nodeColor;
    }
}
