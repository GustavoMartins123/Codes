using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruidor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnLevelWasLoaded(int level)
    {
        Destroy(gameObject);
    }
}
