using UnityEngine;

public class Camera_BackGround_Color : MonoBehaviour
{
    Camera cameraMain;
    // Start is called before the first frame update
    void Start()
    {
        cameraMain = GetComponent<Camera>();
        int i = Random.Range(0, 10);
        switch (i)
        {
            case 0:
                cameraMain.backgroundColor = Color.black;
                break;
            case 1:
                cameraMain.backgroundColor = Color.blue;
                break;
            case 2:
                cameraMain.backgroundColor = Color.clear;
                break;
            case 3:
                cameraMain.backgroundColor = Color.cyan;
                break;
            case 4:
                cameraMain.backgroundColor = Color.gray;
                break;
            case 5:
                cameraMain.backgroundColor = Color.green;
                break;
            case 6:
                cameraMain.backgroundColor = Color.grey;
                break;
            case 7:
                cameraMain.backgroundColor = Color.magenta;
                break;
            case 8:
                cameraMain.backgroundColor = Color.red;
                break;
            case 9:
                cameraMain.backgroundColor = Color.yellow;
                break;
        }
        
    }
}
