using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAvan : MonoBehaviour
{
    Vector3 camVel = Vector3.zero;
    [SerializeField] GameObject followObj;
    [SerializeField] float ang = 25f;
    [SerializeField] float inputSens = 155f;
    float mouseX, mouseY;
    float rotY= 0, rotX= 0;
    Vector3 rot;
    Camera cam;
    Quaternion localRot;
    Vector3 cameraDirection;
    float camDistance;
    RaycastHit hit;
    Vector2 camDistanceMinMax = new Vector2(1f, 3.3f);
    WaitForSeconds wait = new WaitForSeconds(0.1f);
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        cameraDirection = cam.transform.localPosition.normalized;
        camDistance = camDistanceMinMax.y;
        if (followObj== null)
        {
            StartCoroutine(GettingHead());
        }
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAng();
    }

    void Init()
    {
        rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }
    IEnumerator GettingHead()
    {
        yield return wait;
        if (PlayerDisplay.instance.players[Save.pchar].activeSelf)
        {
            followObj = GameObject.FindGameObjectWithTag("Head");
            //followObj = PlayerDisplay.instance.players[Save.pchar].gameObject;
        }
        
    }
    void UpdateAng()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        rotY += mouseX * inputSens * Time.deltaTime;
        rotX -= mouseY * inputSens * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, -ang, ang);
        localRot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = localRot;
    }

    private void LateUpdate()
    {
        if(followObj!= null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, followObj.transform.position, ref camVel, 0.15f);
            CheckCam(cam.transform);
        }

    }

    void CheckCam(Transform camPos)
    {
        Vector3 cameraDes = transform.TransformPoint(cameraDirection * camDistanceMinMax.y);
        if (Physics.Linecast(transform.position, cameraDes, out hit))
        {
            camDistance = Mathf.Clamp(hit.distance, camDistanceMinMax.x, camDistanceMinMax.y);
        }
        else
        {
            camDistance = camDistanceMinMax.y;
        }
        camPos.transform.localPosition = cameraDirection * camDistance;
    }
}
