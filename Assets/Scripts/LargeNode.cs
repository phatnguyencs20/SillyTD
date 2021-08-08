using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeNode : MonoBehaviour
{
    [SerializeField]
    Vector3 turretPositionOffset;

    GameObject turret;

    void OnMouseDown()
    {
        int turretCost;
        if (turret != null)
        {
            turretCost = turret.GetComponent<ArtilleryTurret>().GetCost() / 2;
            GameManager.gameManager.totalCredit += turretCost;
            Destroy(turret);
            return;
        }

        GameObject turretToBuild = TurretManager.turretManager.GetArtilleryTurret();
        if (GameManager.gameManager.totalCredit >= turretToBuild.GetComponent<ArtilleryTurret>().GetCost())
        {
            if (turretToBuild.tag == "LargeTurret")
            {
                turret = Instantiate(TurretManager.turretManager.GetArtilleryTurret(), transform.position + turretPositionOffset, transform.rotation);
                turretCost = turret.GetComponent<ArtilleryTurret>().GetCost();
                GameManager.gameManager.totalCredit -= turretCost;
            }
            else
            {
                Debug.Log("Turret too small!!!");
            }
        }
        else
        {
            Debug.Log("Not enough credit");
        }
    }
}
