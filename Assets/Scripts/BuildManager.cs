using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Нельзя использовать BuildManager больше чем одного раза!");
            return;
        }
        instance = this;
    }  

    [HideInInspector]
    private TurretBluePrint turretToBuild;
    public GameObject buildEffect;

    private Node selectedNode;
    public NodeUI nodeUI;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void EmptyTurretSelection()
    {
        turretToBuild = null;
    }
    public void SelectTurretToPurchase(TurretBluePrint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
