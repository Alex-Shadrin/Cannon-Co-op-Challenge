using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] private float RotationAngle;
    [SerializeField] private float RotationSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, RotationSpeed) * RotationSpeed);
    }
}
