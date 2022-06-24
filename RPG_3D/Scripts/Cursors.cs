using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursors : MonoBehaviour
{
    [SerializeField] GameObject cursor;
    [SerializeField] Sprite[] cursors;
    [SerializeField] Image cursor_Image;
    private void Start()
    {
        cursor_Image.sprite = cursors[0];
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void Update()
    {
        cursor.transform.position = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            cursor_Image.sprite = cursors[1];
        }
        else if(Input.GetMouseButtonUp(0))
        {
            cursor_Image.sprite = cursors[0];
        }
    }
}
