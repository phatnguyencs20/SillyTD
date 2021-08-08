using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallNode : MonoBehaviour
{
    [SerializeField]
    Vector3 turretPositionOffset;

    GameObject turret;

    void OnMouseDown()
    {
        if (turret != null)
        {
            Destroy(turret);
            return;
        }

        GameObject turretToBuild = TurretManager.turretManager.GetArtilleryTurret();
        if (turretToBuild.tag == "SmallTurret")
        {
            turret = Instantiate(TurretManager.turretManager.GetArtilleryTurret(), transform.position + turretPositionOffset, transform.rotation);
        } 
        else
        {
            Debug.Log("Turret too big!!!");
        }
    }
}
