using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        if (Input.GetKey(KeyCode.Z))
        {
            transform.position += transform.forward * .025f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * .025f;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position -= transform.right * .025f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * .025f;
        }
    }
}
