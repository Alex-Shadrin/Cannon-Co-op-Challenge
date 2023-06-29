using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject HitPrefab;
    //public GameObject MuzzlePrefab;
    public float Speed;

    Rigidbody rb;
    Vector3 velocity;

    void Awake()
    {
        TryGetComponent(out rb);
    }

    void Start()
    {
        //var muzzleEffect = Instantiate(MuzzlePrefab, transform.position, transform.rotation);
        //Destroy(muzzleEffect, 5f);
        velocity = transform.forward * Speed;
    }

    void FixedUpdate()
    {
        var displacement = velocity * Time.deltaTime;
        rb.MovePosition(rb.position + displacement);
    }

    void OnCollisionEnter(Collision other)
    {
        var hitEffect = Instantiate(HitPrefab, other.GetContact(0).point, Quaternion.identity);
        rb.useGravity = true;
        Destroy(hitEffect, 5f);
        Destroy(gameObject);

        if(other.gameObject.GetComponent<Health>())
        {
            other.gameObject.GetComponent<Health>().TakeHit(20);
        }

    }
}
