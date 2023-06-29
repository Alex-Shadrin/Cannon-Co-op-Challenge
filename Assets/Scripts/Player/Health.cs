using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHelth = 100;

    public event Action OnDeath;

    private void Start()
    {
        health = maxHelth;
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
