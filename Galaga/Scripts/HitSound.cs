using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSound : MonoBehaviour
{
    [SerializeField] AudioClip hit;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(hit, transform.position);
        Destroy(gameObject, 0.6f);
    }
}
