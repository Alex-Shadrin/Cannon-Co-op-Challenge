using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<MonoBehaviour>
{
    public GameObject SreenFade;
    private string CurrentLevel = "Level1";
   
    public void GameOver()
    {
        StartCoroutine(Reload());
    }
    public IEnumerator Reload()
    {
        SreenFade.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(CurrentLevel);
    }
}
