using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float Length;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _ray;

    [SerializeField] private ParticleSystem _muzzleParticles;
    [SerializeField] private ParticleSystem _hitParticles;

    public LayerMask LayerMask;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, -Vector3.up);

        //Debug.DrawRay(transform.position, ray.direction * Length, Color.green);
        //_lineRenderer.SetPosition(1, ray.direction * Length );

        //if (Physics.Raycast(ray,out var hit, Length, LayerMask))
        //{
        //    Debug.Log("Hit");

        //    if (hit.transform.gameObject.TryGetComponent<Health>(out var health))
        //    {
        //        health.TakeHit(10);
        //    }
        //}

        bool cast = Physics.Raycast(ray, out var hit, Length, LayerMask);

        Vector3 hitPosition = cast ? hit.point : _ray.position + -Vector3.up * Length;
        _lineRenderer.SetPosition(0, _ray.position);
        _lineRenderer.SetPosition(1, hitPosition);

        if (cast && hit.collider.gameObject.TryGetComponent<Health>(out var health))
        {
            health.TakeHit(10);
        }

        _hitParticles.transform.position = hitPosition;
    }
}
