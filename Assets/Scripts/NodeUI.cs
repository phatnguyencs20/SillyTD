using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public static NodeUI nodeUI;

    [SerializeField]
    private float heightOffset;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject buildPanel;
    [SerializeField]
    private GameObject modificationPanel;

    private Transform mainCameraTransform;
    private Transform canvasTransform;
    private Node selectedNode;

    private void Awake()
    {
        if (nodeUI != null)
        {
            Destroy(gameObject);
        }
        else
        {
            nodeUI = this;
        }

        canvasTransform = canvas.GetComponent<RectTransform>();
        mainCameraTransform = GameObject.Find("MainCamera").GetComponent<Transform>();
        HideAll();
    }

    private void Update()
    {
        // Rotate NodeUI to follow camera view
        Vector3 direction = canvasTransform.position - mainCameraTransform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        canvasTransform.rotation = Quaternion.Slerp(canvasTransform.rotation, lookRotation, 2f * Time.deltaTime);

        // Hide NodeUI when not selected
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            HideAll();
        }
    }

    public void SelectNode(Node nodeToSelect)
    {
        selectedNode = nodeToSelect;
        Vector3 selectedNodePosition = selectedNode.transform.position;

        // Show NodeUI according to node position
        if (selectedNode.GetTurret() != null)
        {
            ShowModificationPanel(selectedNodePosition);
            return;
        }

        ShowBuildPanel(selectedNodePosition);
    }

    public void ShowBuildPanel(Vector3 selectedNodePosition)
    {
        canvasTransform.position = new Vector3(selectedNodePosition.x, selectedNodePosition.y + heightOffset, selectedNodePosition.z);
        buildPanel.gameObject.SetActive(true);
    }

    public void HideBuildPanel()
    {
        selectedNode = null;
        buildPanel.gameObject.SetActive(false);
    }

    public void ShowModificationPanel(Vector3 selectedNodePosition)
    {
        Turret turret = selectedNode.GetTurret().GetComponent<Turret>();
        canvasTransform.position = new Vector3(selectedNodePosition.x, selectedNodePosition.y + turret.GetModelHeight() + heightOffset, selectedNodePosition.z);
        modificationPanel.gameObject.SetActive(true);
    }

    public void HideModificationPanel()
    {
        selectedNode = null;
        modificationPanel.gameObject.SetActive(false);
    }

    public void HideAll()
    {
        HideBuildPanel();
        HideModificationPanel();
    }
}
