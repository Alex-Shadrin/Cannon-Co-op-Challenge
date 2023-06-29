using UnityEngine;

public enum LoadLevel
{
    Selected,
    AutoNext,
    AutoPrevious
}

[RequireComponent(typeof(Collider))]
public class LoadLevelTrigger : MonoBehaviour
{
    [SerializeField] private Level level;

    [SerializeField] private LoadLevel loadLevel;
    bool _wasTriggered = false;

    private SimpleLevelManager _levelManager;
    void Start()
    {
        _wasTriggered = false;
        _levelManager = SimpleLevelManager.Instance;
    }

    private async void OnTriggerEnter(Collider other)
    {
        if (_wasTriggered)
            return;

        if (_levelManager == null)
        {
            Debug.LogError(
                "LoadLevelTrigger._levelManager is not loaded! AutoNext & AutoPrevious won't work.",
            this);
            return;
        }
        else
        {
            switch (loadLevel)
            {
                case LoadLevel.Selected:
                    break;
                case LoadLevel.AutoNext:
                    level = _levelManager.CurrentLevel.Next;
                    break;
                case LoadLevel.AutoPrevious:
                    level = _levelManager.CurrentLevel.Previous;
                    break;
            }
        }

        if (level == null)
            Debug.LogError("LoadLevelTrigger.level is null!", this);

        await SimpleLevelManager.Instance.LoadLevelAsync(level);

        _wasTriggered = true;
    }
}
