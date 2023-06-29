using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toxic : MonoBehaviour
{
    [SerializeField] private bool isToxic = false;
    private void OnTriggerStay(Collider collision)
    {
        if (isToxic & collision.gameObject.TryGetComponent<Health>(out var health))
        {
            health.TakeHit(1);
        }
    }
}
