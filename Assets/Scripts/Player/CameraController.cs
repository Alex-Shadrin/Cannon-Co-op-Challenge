using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform PlayerTransform;
    public GameObject PlayerObject;
    public Vector3 Offset;
    public float CameraSpeed = 5f;
    public float ZoomSpeed;

    void FixedUpdate()
    {
        if (PlayerTransform == null) return;
        Vector3 newCameraPosition = transform.position = PlayerTransform.position + Offset;
        transform.position = Vector3.Lerp(transform.position, newCameraPosition, CameraSpeed * Time.deltaTime);    
    }

        //if (Input.mouseScrollDelta.y != 0)
        //    Offset.z += 1 * ZoomSpeed;
}
