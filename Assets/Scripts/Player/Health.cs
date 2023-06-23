using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _smoke;
    public int health;
    public int maxHelth = 100;
    bool once;
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
            StartCoroutine(GameOver());
        }
    }

    public IEnumerator GameOver()
    {
        if (!once)
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Instantiate(_smoke, transform.position, Quaternion.identity);
            once = true;
        }
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("SampleScene");
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
