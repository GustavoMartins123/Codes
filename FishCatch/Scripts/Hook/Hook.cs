using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Hook : MonoBehaviour
{
    Camera main;
    [SerializeField] Transform hookedTransform;
    int lenght;
    int strenght;
    int fishCount;
    Collider2D coll;
    public bool CanMove;
    List<Fish> hookedFishes;

    Tweener cameraTween;
    public static Hook instance;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        main = Camera.main;
        coll = GetComponent<Collider2D>();
        hookedFishes = new List<Fish>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CanMove && Input.GetMouseButton(0)) 
        {
            Vector3 vector = main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = transform.position;
            position.x = vector.x;
            transform.position = position;
        }
    }

    public void StartFishing() 
    {
        lenght = IdleManager.instance.length -20;
        strenght = IdleManager.instance.strenght;
        fishCount = 0;
        float time = (-lenght) * 0.1f;

        cameraTween = main.transform.DOMoveY(lenght, 1 + time * 0.25f, false).OnUpdate(delegate //Camera indo até a posição do tamanho em um determinado tempo
        {
            if (main.transform.position.y <= -11)
            {
                transform.SetParent(main.transform);
            }
        }).OnComplete(delegate //QUando ela chegar no final, o colisor ativara
        {
            coll.enabled = true;
            cameraTween = main.transform.DOMoveY(0, time * 5, false).OnUpdate(delegate 
            {
                if(main.transform.position.y >= -25f) 
                {
                    StopFishing();
                }
            });
        });
        ScreensManager.instance.ChangeScreen(Screen.GAME);
        coll.enabled = false;
        CanMove = true;
        hookedFishes.Clear();
    }

    public void StopFishing()
    {
        CanMove = false;
        cameraTween.Kill(false);
        cameraTween = main.transform.DOMoveY(0, 2, false).OnUpdate(delegate 
        {
            if(main.transform.position.y >= -11) 
            {
                transform.SetParent(null);
                transform.position = new Vector2(transform.position.x, -6);
            }
        }).OnComplete(delegate 
        {
            transform.position = Vector2.down * 6;
            coll.enabled = true;
            int num = 0;
            //Limpando o numero de peixes
            for(int i =0; i < hookedFishes.Count; i++) 
            {
                hookedFishes[i].transform.SetParent(null);
                hookedFishes[i].ResetFish();
                num += hookedFishes[i].Type.price;
            }
            IdleManager.instance.totalGain = num;
            ScreensManager.instance.ChangeScreen(Screen.END);
        });

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Fish") && fishCount != strenght) 
        {
            fishCount++;
            Fish component = collision.GetComponent<Fish>();
            component.Hooked();
            hookedFishes.Add(component);
            collision.transform.SetParent(transform);
            collision.transform.position = hookedTransform.position;
            collision.transform.rotation = hookedTransform.rotation;
            collision.transform.localScale = Vector3.one;

            collision.transform.DOShakeRotation(5, Vector3.forward * 45, 10, 90, false).SetLoops(1, LoopType.Yoyo).OnComplete(delegate 
            {
                collision.transform.rotation = Quaternion.identity;
            });
            if(fishCount == strenght) 
            {
                StopFishing();
            }
        }
    }

}
