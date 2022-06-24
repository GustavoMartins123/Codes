using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToEnemy : MonoBehaviour
{
    ZombieMovement zombie;
    [SerializeField] float damage = 0.1f;
    [SerializeField] GameObject hitEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            zombie = other.GetComponent<ZombieMovement>();
            if (zombie != null && other.gameObject != zombie)
            {
                zombie = null;
                zombie = other.GetComponent<ZombieMovement>();
            }
            zombie.Dmg(damage * (Save.strenghtPower* 7f));
            zombie.AtirouEmMim();
            if (zombie.iamBoss == false)
            {
                zombie.anim.SetTrigger("hit");
            }
            Instantiate(hitEffect, zombie.GetComponent<Collider>().ClosestPointOnBounds(transform.position), transform.rotation);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            zombie = null;
        }
    }
}
