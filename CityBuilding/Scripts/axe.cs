using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe : MonoBehaviour
{
    public Collider cox;
    public static int t = 0;
    // Start is called before the first frame update
    void Start()
    {
        t = 0;
    }
    public void Liga() 
    {
        cox.enabled = true;
    }
    public void Desliga() 
    {
        cox.enabled = false;
    }
}
