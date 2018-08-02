using UnityEngine.UI;
using UnityEngine;

public class NodeUI : MonoBehaviour {

    private Node target;

    public Text upgradeCost;
    public Text sellCost;

    public Button upgradeButton;

    public GameObject UI;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBluePrint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "UnAvailable!!!";
            upgradeButton.interactable = false; 
        }

        sellCost.text = "$" + target.turretBluePrint.GetSellAmount();

        UI.SetActive(true);
    }

    public void Hide()
    {
        UI.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        //For closing Upgrade Menu
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        //For closing Sell Menu
        BuildManager.instance.DeselectNode();
    }
}
