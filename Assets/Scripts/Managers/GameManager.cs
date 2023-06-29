using UnityEngine;

public class GameManager : PersistentSingleton<MonoBehaviour>
{
    [SerializeField] MainMenu menu;
    public void Start()
    {



        //Start(CurrentLevel);
    }

    public void LoadNextLevel(string level)
    {
        //SceneManager.LoadScene(CurrentLevel, LoadSceneMode.Single);
    }

    public void RestartLevel()
    {
        //Debug.Log("reloading level: " + CurrentLevel);
        //SceneManager.LoadScene(CurrentLevel, LoadSceneMode.Single);
    }
}
