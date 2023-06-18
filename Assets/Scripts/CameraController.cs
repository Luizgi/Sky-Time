using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivy = 0.001f;
    public Transform target;

    private float xRotation = 0f;
    private float yRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivy;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivy;

        if(target != null)
        {
            target.Rotate(Vector3.up * mouseX);
        }

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation += mouseY;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation , 0f);
    }
}
