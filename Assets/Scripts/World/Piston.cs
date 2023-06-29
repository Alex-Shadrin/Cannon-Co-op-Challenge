using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    [SerializeField] private CloseBox closeBox;
    [SerializeField] private float step;
    [SerializeField] private Vector3 endPosition;
    private float progress;
    Vector3 pos;
    bool playerInBox;
    private void Start()
    {
        closeBox.OnEnd += OnEnd;
    }
    private void OnDestroy()
    {
        closeBox.OnEnd -= OnEnd;
    }

    private void FixedUpdate()
    {
        if (playerInBox)
        {
            transform.position = Vector3.Lerp(gameObject.transform.position, endPosition, progress);
            progress += step;
        }
    }
    private void OnEnd()
    {
        playerInBox = true;
    }
}
