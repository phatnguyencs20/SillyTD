using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public int totalCredit;

    public TMP_Text waveCountdownText;

    void Awake()
    {
        if (gameManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            gameManager = this;
        }
        waveCountdownText = GameObject.Find("TotalCreditText").GetComponent<TMP_Text>();
    }

    void Update()
    {
        waveCountdownText.text = "Credit: " + totalCredit.ToString();
    }
}
