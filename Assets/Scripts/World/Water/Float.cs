using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Float : MonoBehaviour 
{
    [SerializeField] private float Depth = 50f;
    [SerializeField] private float Byonancy = 0.1f;
    [SerializeField] private GameObject Splashes;
    
    // private List<GameObject> _colliding = new();
    //private void FixedUpdate()
    //{
    //    foreach (var collidingObject in _colliding)
    //    {
    //        if (collidingObject.TryGetComponent<Floatable>(out var _))
    //        {
    //            var ray = Ray(collidingObject.transform.position, Vector3.up);
    //            Debug.Log("Float Raycast" + ray);
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider collision)
    {
        Instantiate(Splashes, collision.transform.position, Quaternion.LookRotation(collision.transform.up));
        Debug.Log("FloatEnterCollision");
        var collidingObject = collision.gameObject;
        if (collidingObject.TryGetComponent<Floatable>(out var floatable))
        {
            floatable.difference = Byonancy;
            floatable.isUnderWater = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("FloatExitCollision");
        var collidingObject = collision.gameObject;
        if (collidingObject.TryGetComponent<Floatable>(out var floatable))
        {
            floatable.difference = -Byonancy;
            floatable.isUnderWater = false;
        }
    }
    private void Start()
    {
        var collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.center = new Vector3(
            collider.center.x, 
            collider.center.y - Depth / 2, 
            collider.center.z
        );
        collider.size = new Vector3(
            collider.size.x, 
            collider.size.y + Depth, 
            collider.size.z
        );
    }
}
