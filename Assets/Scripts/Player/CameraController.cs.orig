using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Health health;
    public Transform Target;
    public Vector3 Offset;
    public float smoothSpeed = 0.125f;
    public float CameraSpeed = 1f;
    public float ZoomSpeed;

    private void Start()
    {
        health.OnDeath += OnDeath;
    }
    private void OnDestroy()
    {
        health.OnDeath -= OnDeath;
    }

    private void OnDeath()
    {
        //StartCoroutine(Shake(.15f, .4f));
    }
    void FixedUpdate()
    {
        if (Target == null) return;
        Vector3 deseredPosition = Target.position + Offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, deseredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //transform.LookAt(Target);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.position - transform.position), CameraSpeed * Time.deltaTime);
    }

    //public IEnumerator Shake(float duration, float magnitude )
    //{
    //    Vector3 originalPositon = transform.localPosition;
    //    float elapsed = 0.0f;
    //    while(elapsed > duration)
    //    {
    //        float x = Random.Range(-1f, 1f) * magnitude;
    //        float y = Random.Range(-1f, 1f) * magnitude;

    //        transform.localPosition = new Vector3(x, y, originalPositon.z);

    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //    transform.localPosition = originalPositon;
    //}
}
