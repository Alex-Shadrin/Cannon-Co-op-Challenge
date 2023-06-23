using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHelth = 100;

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
            SceneManager.LoadScene("SampleScene");
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
