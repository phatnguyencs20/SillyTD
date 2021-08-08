using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public static TurretManager turretManager;

    [SerializeField]
    GameObject artilleryTurret;
    [SerializeField]
    GameObject standardTurret;
    
    void Awake()
    {
        if(turretManager != null)
        {
            Destroy(gameObject);
        } 
        else
        {
            turretManager = this;
        }
    }

    public GameObject GetArtilleryTurret()
    {
        return artilleryTurret;
    }

    public GameObject GetStandardTurret()
    {
        return standardTurret;
    }
}
