using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float ShotPower = 100;
    public float RecoilPower = 5;
    public TrajectoryRenderer Trajectory;

    private Camera _mainCamera;
    private Vector3 _shotDirection;
    private Rigidbody _rb;
    private bool _isReloading = false;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    private readonly Queue<GameObject> _bullets = new();

    private void Update()
    {
        var trackingPosition = Input.mousePosition;

        Ray ray = _mainCamera.ScreenPointToRay(trackingPosition);
        new Plane(-Vector3.forward, transform.position).Raycast(ray, out var enter);
        Vector3 mouseInWorld = ray.GetPoint(enter);
        //Debug.Log(ray);

        _shotDirection = (mouseInWorld - transform.position).normalized;

        //transform.rotation = Quaternion.LookRotation(speed);
        Trajectory.ShowTranjectory(transform.position, _shotDirection * ShotPower);


        if (Input.GetMouseButtonDown(0))
        {
            if(!_isReloading)
                StartCoroutine(Fire());
        }
    }

    public IEnumerator Fire()
    {
        var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        _bullets.Enqueue(bullet);

        bullet.GetComponent<Rigidbody>()
            .AddForce(_shotDirection * ShotPower, ForceMode.VelocityChange);

        if (_bullets.Count >= 5)
        {
            Destroy(_bullets.Dequeue());
        }

        var recoilDirection = new Vector3(
            _shotDirection.x * -1,
            _shotDirection.y * -1,
            _shotDirection.z
        );

        _rb.AddForce(recoilDirection * RecoilPower);

        _isReloading = true;
        yield return new WaitForSeconds(1);
        _isReloading = false;
    }
}
