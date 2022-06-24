using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fundo : MonoBehaviour
{
    //Pegando o meu material
    private Renderer fundo;
    //posição do meu x offset
    private float xSet = 0f;
    //Posição da minha textura
    private Vector2 Texture;
    //Velocidade do fundo
    private float velocidade = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        fundo = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Diminuindo o valor X do meu xSet
        xSet += Time.deltaTime * velocidade;
        //Passando o xSet para o eixo X da minha textura
        Texture.y = xSet;
        //Movendo o xset X do meu renderer
        fundo.material.mainTextureOffset = Texture;
    }
}
