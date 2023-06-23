using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Float : MonoBehaviour 
{
    [SerializeField] private float Depth = 50f;
    private List<GameObject> colliding = new();
    private void FixedUpdate()
    {
        //Debug.Log("FloatEnterCollision");

        foreach (var collidingObject in colliding)
        {
            if (collidingObject.TryGetComponent<Floatable>(out var buoyancy))
            {
                Ray ray = new Ray(collidingObject.transform.position, Vector3.up);
                Debug.Log("Float Raycast" + ray);


                //if (Physics.Raycast(ray, out var hit))
                //{
                //    bool isUnderWater = hit.transform.gameObject == this.gameObject;
                //    buoyancy.difference = hit.distance;
                //    buoyancy.isUnderWater = isUnderWater;
                //}
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        
        Debug.Log("FloatEnterCollision");
        var collidingObject = collision.gameObject;
        if (collidingObject.TryGetComponent<Floatable>(out var buoyancy))
        {
            bool isUnderWater = true;
            buoyancy.difference = 0.1f;
            buoyancy.isUnderWater = isUnderWater;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("FloatExitCollision");
        var collidingObject = collision.gameObject;
        if (collidingObject.TryGetComponent<Floatable>(out var buoyancy))
        {
            bool isUnderWater = false;
            buoyancy.difference = 0.1f;
            buoyancy.isUnderWater = isUnderWater;
        }
    }
    private void Start()
    {
        var collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.center = new Vector3(collider.center.x, collider.center.y - Depth/2, collider.center.z);
        collider.size = new Vector3(collider.size.x, collider.size.y + Depth , collider.size.z);
    }
}
