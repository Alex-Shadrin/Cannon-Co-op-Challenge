using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button JoinButton;
    [SerializeField] private TMP_InputField JoinCodeField;
    [SerializeField] private Button HostButton;

    void Start()
    {
        JoinButton.onClick.AddListener(OnJoinClick);
        HostButton.onClick.AddListener(OnHostClick);
    }

    private void EnableButtons(bool areEnabled)
    {
        JoinButton.enabled = areEnabled;
        JoinCodeField.enabled = areEnabled;

        HostButton.enabled = areEnabled;
    }

    private async void OnJoinClick()
    {
        EnableButtons(false);
        var joinCode = JoinCodeField.text.Trim().ToUpper();
        var lobby = await MultiplayerLobbyManager.Instance.Join(joinCode);
        if (lobby == null)
            JoinCodeField.text = "Lobby is not found";
        EnableButtons(true);

        // created and joining the lobby should hide the menu
        if (lobby != null)
            gameObject.SetActive(false);
    }

    private async void OnHostClick()
    {
        EnableButtons(false);
        var lobby = await MultiplayerLobbyManager.Instance.Host();
        if (lobby == null)
            JoinCodeField.text = "Lobby could not be hosted";
        EnableButtons(true);

        // found and joining the lobby should hide the menu
        if (lobby != null)
            gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        JoinButton.onClick.RemoveListener(OnJoinClick);
        HostButton.onClick.AddListener(OnHostClick);
    }
}
