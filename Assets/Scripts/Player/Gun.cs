using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // public interface members
    [SerializeField] private GameObject BulletPrefab;

    [SerializeField] private float MaxShootDurationSec = 4f;

    [SerializeField] private float MinShootPower = 5;
    [SerializeField] private float MaxShootPower = 20;
    [SerializeField] private float RecoilPowerMultiplier = 50f;

    [SerializeField] private float MinCooldownDurationSec = 2f;
    [SerializeField] private float MaxCooldownDurationSec = 4f;

    [SerializeField] private Trajectory Trajectory;
    [SerializeField] private Rigidbody PlayerRigidBody;
    [SerializeField] private ButtonHolder ButtonHolder;

    private Vector3 _shotDirection;
    private bool _isReloading = false;

    private void Start()
    {
        ButtonHolder.OnHoldComplete += OnFireButtonHoldComplete;
        ButtonHolder.OnHolding += OnFireButtonIsHeld;
    }
    private void OnDestroy()
    {
        ButtonHolder.OnHoldComplete -= OnFireButtonHoldComplete;
        ButtonHolder.OnHolding -= OnFireButtonIsHeld;
    }

    private void OnFireButtonIsHeld(float heldTime)
    {
        var sp = CalculateShootPower(heldTime);
        if (!_isReloading)
        {
            Trajectory.Show(transform.position, _shotDirection * sp);
        }
    }
    private void OnFireButtonHoldComplete(float heldTime)
    {
        Trajectory.Hide();
        var sp = CalculateShootPower(heldTime);
        if (!_isReloading)
            StartCoroutine(Fire(sp));
    }

    private float CalculateShootPower(float heldTime)
    {
        var shootTime = Mathf.Min(MaxShootDurationSec, heldTime);
        var powerPerTime = (MaxShootPower - MinShootPower) / MaxShootDurationSec;
        var shootPower = Mathf.Max(MinShootPower, Mathf.Min(MinShootPower + shootTime * powerPerTime, MaxShootPower));
        return shootPower;
    }

    private float CalculateCooldownTime(float shootPower)
    {
        var cooldownPerShootPower = 
            (MaxCooldownDurationSec - MinCooldownDurationSec) /
            (MaxShootPower - MinShootPower);
        
        var cooldown = MathF.Max(
            MinCooldownDurationSec,
            MathF.Min(MinCooldownDurationSec + cooldownPerShootPower * shootPower, MaxCooldownDurationSec)
        );

        return cooldown;
    }
   
    private readonly Queue<GameObject> _bullets = new();

    private Camera _camera;

    private async void Awake()
    {
        _camera = await CameraHelper.AwaitMainCamera();
    }
    private async void Update()
    {
        if (this.gameObject == null)
            return;

        if (_camera == null)
            _camera = await CameraHelper.AwaitMainCamera();

        var trackingPosition = Input.mousePosition;
        Ray ray = new Ray();
        float enter = 0;
        try
        {
            ray = _camera.ScreenPointToRay(trackingPosition);
            new Plane(-Vector3.forward, transform.position).Raycast(ray, out enter);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        Vector3 mouseInWorld = ray.GetPoint(enter);
        _shotDirection = (mouseInWorld - transform.position).normalized;
    }

    public IEnumerator Fire(float shotPower = 10)
    {
        var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        _bullets.Enqueue(bullet);

        bullet.GetComponent<Rigidbody>()
            .AddForce(_shotDirection * shotPower, ForceMode.VelocityChange);

        if (_bullets.Count >= 5)
        {
            Destroy(_bullets.Dequeue());
        }

        var recoilDirection = new Vector3(
            _shotDirection.x * -1,
            _shotDirection.y * -1,
            _shotDirection.z
        );

        var recoilPower = shotPower * RecoilPowerMultiplier;
        PlayerRigidBody.AddForce(recoilDirection * recoilPower);


        var cooldownTime = CalculateCooldownTime(shotPower);


        Debug.Log("Shoot! Power: " + shotPower + " Cooldown: " + cooldownTime);
        _isReloading = true;
        yield return new WaitForSeconds(cooldownTime);
        _isReloading = false;
    }
}
