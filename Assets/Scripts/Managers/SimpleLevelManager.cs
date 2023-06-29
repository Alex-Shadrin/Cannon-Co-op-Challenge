using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleLevelManager : PersistentSingleton<SimpleLevelManager>
{
    public Level[] Levels { get; private set; }

    #region Level constants

    [SerializeField] private GameObject LevelContainer;

    public Level MainMenuLevel => Levels.Where(l => l.Name.Contains("0_MainMenu")).First();
    public Level LobbyLevel => Levels.Where(l => l.Name.Contains("Lobby")).First();

    #endregion

    public Level CurrentLevel => _currentLevel;
    [SerializeField] private Level _currentLevel;

    async void Start()
    {
        Levels = Resources.LoadAll<Level>("Levels");
        await LoadLevelAsync(_currentLevel == null ? MainMenuLevel : _currentLevel, unloadCurrent: false);
    }

    public List<GameObject> RootSceneObjects = new();

    public async Task ReloadLevelAsync()
    {
        await LoadLevelAsync(_currentLevel, unloadCurrent: true);
    }
    public async Task LoadLevelAsync(Level level, bool unloadCurrent = true)
    {
        if(_currentLevel != null && unloadCurrent)
        {
            Debug.Log("Unloading level: " + _currentLevel.Name + "...", _currentLevel);

            // delete player first

            var player = GameObject.FindGameObjectWithTag("Player");
            
            if(player != null)
                Destroy(player);

            var unload = SceneManager.UnloadSceneAsync(_currentLevel.Name);
            if (unload == null)
                return;

            while (!unload.isDone)
                await Task.Yield();

            //foreach (var oldSceneObject in RootSceneObjects)
            //{
            //    Destroy(oldSceneObject);
            //}

            //RootSceneObjects.Clear();
        }

        if (level != null)
        {
            Debug.Log("Loading level: " + level.name + "...", level);
            var load = SceneManager.LoadSceneAsync(level.Name, LoadSceneMode.Additive);
            while (!load.isDone)
                await Task.Yield();

            //var newScene = SceneManager.GetSceneByName(level.Name);
            //foreach (var newSceneObject in newScene.GetRootGameObjects())
            //{
            //    //newSceneObject.transform.parent = LevelContainer.transform;
            //    RootSceneObjects.Add(newSceneObject);
            //}
        }

        _currentLevel = level;
    }
}
