using UnityEngine;

public class UI : Singleton<UI>
{
    private MainMenu _mainMenu;

    private void Start()
    {
       _mainMenu = GetComponentInChildren<MainMenu>();
    }

    public void ToggleMainMenu(bool state)
    {
        _mainMenu.gameObject.SetActive(state);
    }
}