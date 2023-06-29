using System.Linq;
using UnityEngine;

[RequireComponent (typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerDestruction : MonoBehaviour
{
    [SerializeField] private bool ShouldDestroyGameObject;
    [SerializeField] private Health health;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject destroyedVersion;
    
    private Collider _playerColider;
    private Rigidbody _rigidbody;
    private Transform[] childern;
    private void Start()
    {
        _playerColider = GetComponent<Collider>();
        var self = gameObject.transform;
        childern = Enumerable.Range(0, self.childCount).Select(i => self.GetChild(i)).ToArray();

        _rigidbody = GetComponent<Rigidbody>();
        health.OnDeath += OnDeath;
    }
    private void OnDestroy()
    {
        health.OnDeath -= OnDeath;
    }
    private void OnDeath()
    {
        var playerTransform = gameObject.transform;

        Instantiate(explosion, playerTransform.position, Quaternion.identity);
        var replacement = Instantiate(destroyedVersion, playerTransform.position, playerTransform.rotation);

        var rbs = replacement.GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rbs)
        {
            rb.AddExplosionForce(100f, destroyedVersion.transform.position, 5f);
        }

        // turning off collider rigidbody physics and underlying meshes
        _playerColider.enabled = false;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        foreach (var child in childern)
        {
            child.gameObject.SetActive(false);
        }

        if (ShouldDestroyGameObject)
            Destroy(gameObject, 1);
    }
}
