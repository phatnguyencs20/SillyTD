using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    private Color hoverColor;

    private Color initialColor;
    private Renderer nodeRenderer;
    private GameObject turret;

    private void Awake()
    {
        nodeRenderer = GetComponent<Renderer>();
        initialColor = nodeRenderer.material.color;
    }

    private void OnMouseEnter()
    {
        nodeRenderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        nodeRenderer.material.color = initialColor;
    }

    private void OnMouseDown()
    {
        NodeUI.nodeUI.SelectNode(this);
        BuildManager.buildManager.SelectNode(this);
    }

    public GameObject GetTurret()
    {
        return turret;
    }

    public void SetTurret(GameObject turretToSet)
    {
        turret = turretToSet;
    }
}

