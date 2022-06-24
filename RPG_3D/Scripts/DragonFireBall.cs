using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFireBall : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject particleBall;

    void Attack()
    {
        spawnPoint.LookAt(PlayerDisplay.instance.players[Save.pchar].transform.position+ Vector3.up);
        particle.Play();
        Instantiate(particleBall, spawnPoint.position, spawnPoint.rotation);
    }
}
