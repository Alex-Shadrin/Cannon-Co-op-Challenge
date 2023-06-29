using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float Length;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _ray;

    [SerializeField] private ParticleController _muzzleParticles;
    [SerializeField] private ParticleController _hitParticles;

    public LayerMask LayerMask;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, -gameObject.transform.up);

        bool cast = Physics.Raycast(ray, out var hit, Length);

        Vector3 hitPosition = cast ? hit.point : _ray.position + -gameObject.transform.up * Length;
        _lineRenderer.SetPosition(0, _ray.position);
        _lineRenderer.SetPosition(1, hitPosition);

        if (cast && hit.collider.gameObject.TryGetComponent<Health>(out var health))
        {
            health.TakeHit(10);
        }

        _hitParticles.transform.position = hitPosition;
    }
}
