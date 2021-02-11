using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MouseSensitivity = 3.0f;
    public Transform player;
    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float PlayerMouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float PlayerMouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        xRotation -= PlayerMouseY;
        xRotation = Mathf.Clamp(xRotation, -90F, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * PlayerMouseX);
    }
}
