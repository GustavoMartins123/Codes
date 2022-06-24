using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private Text txtPontosAtuais;
    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();
        PontosAtuais();
        txtPontosAtuais.text = gm.pontosAtuais.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void PontosAtuais() 
    {
        txtPontosAtuais.text = gm.pontosAtuais.ToString();
    }
}
