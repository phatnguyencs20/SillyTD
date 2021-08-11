using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager buildManager;

    [SerializeField]
    private Vector3[] buildOffsets;
    [SerializeField]
    private GameObject[] turretBlueprints;

    private Node selectedNode;

    private void Awake()
    {
        // Keep the new BuildManager for the new scene
        if(buildManager != null)
        {
            Destroy(buildManager.gameObject);
        } 

        buildManager = this;
    }

    public void BuildTurret(int turretBluePrintIndex)
    {
        int turretCost = turretBlueprints[turretBluePrintIndex].GetComponent<Turret>().GetCost();

        // Don't build if not enough credit
        if(PlayerStats.credit < turretCost)
        {
            // Play some effect here
            Debug.Log("Not enough credit!!!");
            return;
        }

        // Build turret
        PlayerStats.credit -= turretCost;
        GameObject turretToBuild = Instantiate(turretBlueprints[turretBluePrintIndex], selectedNode.transform.position + buildOffsets[turretBluePrintIndex / 4], selectedNode.transform.rotation);
        selectedNode.SetTurret(turretToBuild);
        NodeGUI.nodeUI.HideBuildPanel();

        // Play some effect here
    }

    public void SellTurret()
    {
        Turret turretToSell = selectedNode.GetTurret().GetComponent<Turret>();
        int turretCost = turretToSell.GetCost();

        PlayerStats.credit += turretCost / 2;
        Destroy(turretToSell.gameObject);
        NodeGUI.nodeUI.HideModificationPanel();

        // Play some effect here
    }

    public void UpgradeTurret()
    {
        GameObject turretToUpgrade = selectedNode.GetTurret();
        int lvlUp = turretToUpgrade.GetComponent<Turret>().GetLevel() + 1;
        if(lvlUp > 3)
        {
            // Play some effect here
            Debug.Log("Max lvl!!!");
            return;
        }
        
        GameObject tempTurret = null;
        for (int i = 0; i < turretBlueprints.Length; i += 4)
        {
            if (turretToUpgrade.name.Contains(turretBlueprints[i].name))
            {
                tempTurret = turretBlueprints[i + lvlUp % 4];
                Debug.Log(i + lvlUp % 4);
                break;
            }
        }

        try
        {
            // Don't upgrade if not credit
            int turretCost = tempTurret.GetComponent<Turret>().GetCost();
            if (PlayerStats.credit < turretCost)
            {
                // Play some effect here
                Debug.Log("Not enough credit!!!");
                return;
            }

            // Destroy old turret and replace with new one
            tempTurret.GetComponent<Turret>().CopyTransform(turretToUpgrade.GetComponent<Turret>());
            Destroy(turretToUpgrade.gameObject);
            turretToUpgrade = tempTurret;

            // Build turret
            PlayerStats.credit -= turretCost;
            turretToUpgrade = Instantiate(turretToUpgrade, turretToUpgrade.transform.position, selectedNode.transform.rotation);
            selectedNode.SetTurret(turretToUpgrade);
            NodeGUI.nodeUI.HideModificationPanel();

            // Play some effect here
        }
        catch (MissingReferenceException)
        {
            Debug.LogError("Null reference at BuildManager in UpgradeTurret method");
        }
    }

    public void ChangeTurretMode()
    {

    }

    public void SelectNode(Node nodeToSelect)
    {
        selectedNode = nodeToSelect;
    }
}
