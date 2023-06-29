using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LeaveButton : MonoBehaviour
{
    private MultiplayerLobbyManager _lobby;
    private Button _leaveButton;

    // Start is called before the first frame update
    void Start()
    {
        _leaveButton = GetComponent<Button>();
        _leaveButton.onClick.AddListener(OnLeaveClick);

        _lobby = MultiplayerLobbyManager.Instance;
        _lobby.JoinedLobby += OnLobbyJoined;
        _lobby.HostedLobby += OnLobbyHosted;
        _lobby.LeftLobby += OnLobbyLeft;

        this.gameObject.SetActive(false);
    }

    private void OnLobbyLeft()
    {
        this.gameObject.SetActive(false);
    }

    private void OnLobbyJoined(LobbyInfo _)
    {
        this.gameObject.SetActive(true);
        _leaveButton.GetComponentInChildren<TMP_Text>().text = "Leave";
    }

    private void OnLobbyHosted(LobbyInfo _)
    {
        this.gameObject.SetActive(true);
        _leaveButton.GetComponentInChildren<TMP_Text>().text = "Stop server";
    }

    private async void OnLeaveClick()
    {
        await _lobby.Leave();
        UI.Instance.ToggleMainMenu(true);
    }

    private void OnDestroy()
    {
        _leaveButton.onClick.RemoveListener(OnLeaveClick);

        if(_lobby != null)
        {
            _lobby.JoinedLobby -= OnLobbyJoined;
            _lobby.HostedLobby -= OnLobbyHosted;
            _lobby.LeftLobby -= OnLobbyLeft;
        }
    }
}
