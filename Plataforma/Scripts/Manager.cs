using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    

    public void Restart() 
    {
        SceneManager.LoadScene("cena1");
    }
    public void Quit() 
    {
        SceneManager.LoadScene("menu");
    }
}
