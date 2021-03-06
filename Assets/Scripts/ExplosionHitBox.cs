using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHitBox : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    private void Awake()
    {
        ParticleSystem particle = Instantiate(explosion, transform.position, explosion.transform.rotation).GetComponent<ParticleSystem>();
        Destroy(particle.gameObject, particle.main.duration);
        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Mob")
        {
            Destroy(other.gameObject);
        }
    }
}
