using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera controlCamera;
    [SerializeField] float zoomSensitivity = 1f;
    [SerializeField] float moveSensitivity = 1f;


    private void Start()
    {
        controlCamera = gameObject.GetComponent<Camera>();
        controlCamera.fieldOfView = 60;
    }

    private void Update()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
        {
            controlCamera.fieldOfView -= scroll * zoomSensitivity;
        }

        if (Input.GetKey("up"))
        {
            transform.Translate(Vector3.up * Time.deltaTime * moveSensitivity);
        }

        if (Input.GetKey("down"))
        {
            transform.Translate(Vector3.down * Time.deltaTime * moveSensitivity);
        }


        if (Input.GetKey("left"))
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSensitivity);
        }

        if (Input.GetKey("right"))
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSensitivity);
        }
    }
}
