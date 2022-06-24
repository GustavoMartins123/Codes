using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTarget : MonoBehaviour
{
    [SerializeField]float speed = 8f;
    [SerializeField]bool rotate = false;
    [SerializeField]bool particleTarget = true;
    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        if (particleTarget)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }
    }
}
