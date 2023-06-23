using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        var trackingPosition = Input.mousePosition;

        Ray ray = _mainCamera.ScreenPointToRay(trackingPosition);
        new Plane(-Vector3.forward, transform.position).Raycast(ray, out var enter);
        Vector3 mouseInWorld = ray.GetPoint(enter);

        Vector3 speed = (mouseInWorld - transform.position);
        transform.rotation = Quaternion.LookRotation(speed);
    }
}
