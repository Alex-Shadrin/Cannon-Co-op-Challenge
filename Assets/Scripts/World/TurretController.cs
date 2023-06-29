using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    public Transform Gun;
    public GameObject BulletPrefab;
    public float Disabled;
    public float RotSpeed;

    private bool _isReloading = false;

    float _distanc;
    Transform _player;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void Update()
    {
        if (_player == null) return;
        _distanc = Vector3.Distance(_player.position, transform.position);

        Vector3 speed = (_player.position - transform.position);
        if (_distanc <= Disabled)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_player.position - transform.position), RotSpeed * Time.deltaTime);

            if (!_isReloading)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    private readonly Queue<GameObject> _bullets = new();
    private IEnumerator Shoot()
    {
        var bullet = Instantiate(BulletPrefab,Gun.position, Quaternion.identity);
        _bullets.Enqueue(bullet);
        bullet.GetComponent<Rigidbody>().AddForce(Gun.forward * 1500);

        if (_bullets.Count >= 5)
        {
            Destroy(_bullets.Dequeue());
        }

        _isReloading = true;
        yield return new WaitForSeconds(1);
        _isReloading = false;
    }

}
