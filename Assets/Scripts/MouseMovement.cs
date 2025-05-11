using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public Transform PlayerBody;
    private float XRotation = 0;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * SettingsManager.instance.Sensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * SettingsManager.instance.Sensitivity * Time.deltaTime;
        XRotation -= MouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(XRotation, 0, 0);
        PlayerBody.Rotate(Vector3.up * MouseX);

    }
}
