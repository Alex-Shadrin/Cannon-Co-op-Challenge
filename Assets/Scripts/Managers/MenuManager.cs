using UnityEngine;

public class MenuManager : PersistentSingleton<MenuManager>
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject PauseMenu;

    private void Start()
    {
        ShowMainMenu();
    }

    public void HideAll()
    {
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        HideAll();
        MainMenu.SetActive(true);
    }

    public void ShowPauseMenu()
    {
        HideAll();
        PauseMenu.SetActive(true);
    }
}
