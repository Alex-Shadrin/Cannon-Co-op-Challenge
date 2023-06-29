using System.Threading.Tasks;
using UnityEngine;

public class ReloadLevelOnDeath : MonoBehaviour
{
    [Range(0f, 5f)]
    [SerializeField] private float waitSeconds;
    [SerializeField] private Health _health;
    void Start()
    {
        _health.OnDeath += _health_OnDeath;
    }

    private async void _health_OnDeath()
    {
        float delay = waitSeconds * 1000;
        Debug.Log($"Waiting [{delay}]ms after death...");
        await Task.Delay((int)delay);

        Debug.Log("Reloading on death... ");
        await SimpleLevelManager.Instance.ReloadLevelAsync();
    }

    private void OnDestroy()
    {
        _health.OnDeath -= _health_OnDeath;
    }
}
