using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestruction : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private GameObject _explosion;
    public GameObject destroyedVersion;
    public Transform PlayerTransform;

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
        Instantiate(_explosion, transform.position, Quaternion.identity);
        var replacement = Instantiate(destroyedVersion, PlayerTransform.position, PlayerTransform.rotation);

        var rbs = replacement.GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rbs)
        {
            rb.AddExplosionForce(100f, destroyedVersion.transform.position, 5f);
        }

        Destroy(gameObject);
    }
}
