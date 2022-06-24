using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBallParticle : MonoBehaviour
{
    [SerializeField] float speed, damage;
    Player player;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (player == null)
        {
            Destroy(gameObject, 5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().Damage(damage);
            Destroy(gameObject);
        }

    }
}
