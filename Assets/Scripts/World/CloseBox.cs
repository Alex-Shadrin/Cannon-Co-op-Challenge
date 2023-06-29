using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CloseBox : MonoBehaviour
{
    [SerializeField] private GameObject Cup;
    [SerializeField] private Animator BoxClose = null;
    [SerializeField] private Rigidbody Box;
    [SerializeField] private Rigidbody Player;
    [SerializeField] private GameManager gameManager;

    public event Action OnEnd;

    bool closeBox;

    private void Start()
    {
        this.OnEnd += Player_OnEnd;
    }
    private void Player_OnEnd()
    {
        gameManager.GameOver();
    }
    private void OnDestroy()
    {
        this.OnEnd -= Player_OnEnd;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            closeBox = true;
            if (closeBox)
            {
                BoxClose.Play("BoxClose", 0, 0.0f);
            }
            OnEnd?.Invoke();
            other.attachedRigidbody.freezeRotation = false;
            Player.constraints = RigidbodyConstraints.FreezePositionX;
            Box.isKinematic = false;
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
    
}
