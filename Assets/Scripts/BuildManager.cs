using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager buildManager;

    public int totalCredit;

    [SerializeField]
    private Vector3[] turretVariantOffsets;
    [SerializeField]
    private GameObject[] turretVariants;

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

    public void BuildTurret(int turretVariantIndex)
    {
        int turretCost = turretVariants[turretVariantIndex].GetComponent<Turret>().GetCost();

        if(totalCredit < turretCost)
        {
            Debug.Log("Not enough credit!!!");
            return;
        }

        totalCredit -= turretCost;
        GameObject turret = Instantiate(turretVariants[turretVariantIndex], selectedNode.transform.position + turretVariantOffsets[turretVariantIndex], selectedNode.transform.rotation);
        selectedNode.SetTurret(turret);
        NodeUI.nodeUI.HideBuildPanel();
    }

    public void SelectNode(Node nodeToSelect)
    {
        selectedNode = nodeToSelect;
    }
}
