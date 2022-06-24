using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 40f, scrollWhell = 7f;
    [SerializeField] Rigidbody rig;
    float mouseSensitivity= 150;
    float rotationX,rotationY;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f && transform.position.y > 10f)
        {
            transform.Translate(transform.forward * scrollWhell);
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f && transform.position.y < 97f)
        {
            transform.Translate(transform.forward * -scrollWhell);
        }
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotationX-= mouseY;
        rotationX = Mathf.Clamp(rotationX, 0, 60f);
        rotationY -= mouseX;
        rotationY = Mathf.Clamp(rotationY, -75, 75);
        transform.Rotate(mouseX * Vector3.up);
        transform.localRotation = Quaternion.Euler(rotationX, -rotationY, 0);
    }
    private void FixedUpdate()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        rig.velocity = new Vector3(h, 0, v) * scrollSpeed;


        if (Input.GetAxis("Horizontal") != 0 && Input.GetKey(KeyCode.LeftShift) || Input.GetAxis("Vertical") != 0 && Input.GetKey(KeyCode.LeftShift))
        {

            scrollSpeed = 100f;
        }
        else
        {
            scrollSpeed = 40f;
        }
        float meux = Mathf.Clamp(transform.position.x, -30, 280);
        float meuz = Mathf.Clamp(transform.position.z, -70, 230);
        transform.position = new Vector3(meux, transform.position.y, meuz);
    }
}
