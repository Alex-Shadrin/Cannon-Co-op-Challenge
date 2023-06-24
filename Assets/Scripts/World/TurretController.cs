using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    public float Disabled;
    public Transform Gun;
    public GameObject BulletPrefab;

    private Gun gun;
    Transform _player;
    float _distanc;
    private void Start()
    {
        gun = GetComponent<Gun>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //_distanc = Vector3.Distance(_player.position, transform.position);

        //Vector3 speed = (_player.position - transform.position);
        //if (_distanc <= Disabled)
        //{
        //    transform.rotation = Quaternion.LookRotation(speed);
        //}
    }
}
