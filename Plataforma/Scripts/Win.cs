using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Restart() 
    {
        SceneManager.LoadScene("cena1");
    }


    public void Quit() 
    {
        SceneManager.LoadScene("menu");
    }
}
