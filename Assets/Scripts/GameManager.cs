using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    private void Awake()
    {
        if (gameManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            gameManager = this;
        }
    }
}
