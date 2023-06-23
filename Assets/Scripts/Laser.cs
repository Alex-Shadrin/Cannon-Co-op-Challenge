using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float Length;
    public LayerMask LayerMask;

    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        //_lineRenderer.SetPosition(0, transform.position);
        Ray ray = new Ray(transform.position, -Vector3.up);

        //Debug.DrawRay(transform.position, ray.direction * Length, Color.green);
        //_lineRenderer.SetPosition(1, ray.direction * Length );

        if (Physics.Raycast(ray,out var hit, Length, LayerMask))
        {
            Debug.Log("Hit");
            //_lineRenderer.SetPosition(1, hit.point);
            if (hit.transform.gameObject.TryGetComponent<Health>(out var health))
            {
                health.TakeHit(10);
            }
        }
    }


    //private void Update()
    //{
    //    _lineRenderer.SetPosition(0, transform.position);
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, -transform.up, out hit))
    //    {
    //        if (hit.collider)
    //        {
    //            _lineRenderer.SetPosition(1, hit.point);
    //        }
    //    }
    //    else _lineRenderer.SetPosition(1, -transform.up * 10);
    //}
}
