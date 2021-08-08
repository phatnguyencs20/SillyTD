using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    Color hoverColor;

    Color initialColor;
    Renderer nodeRenderer;

    void Awake()
    {
        nodeRenderer = GetComponent<Renderer>();
        initialColor = nodeRenderer.material.color;
    }

    void OnMouseEnter()
    {
        nodeRenderer.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        nodeRenderer.material.color = initialColor;
    }
}
