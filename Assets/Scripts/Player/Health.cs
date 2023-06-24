using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public int health;
    public int maxHelth = 100;

    public event Action OnDeath;

    private void Start()
    {
        health = maxHelth;
        this.OnDeath += Health_OnDeath;
    }
    private void Health_OnDeath()
    {
        gameManager.GameOver();
    }

    private void OnDestroy()
    {
        this.OnDeath -= Health_OnDeath;
    }

    public void TakeHit(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            health = 0;
            OnDeath?.Invoke();
            
        }
    }

    public void SetHealth(int bonusHealth)
    {
        health += bonusHealth;
        if (health > maxHelth)
        {
            health = maxHelth;
        }
    }
}
