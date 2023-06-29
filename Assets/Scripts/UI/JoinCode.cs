using TMPro;
using UnityEngine;

[RequireComponent (typeof(TMP_Text))]
public class JoinCode : MonoBehaviour
{
    private TMP_Text _text;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = "";
        _text.gameObject.SetActive(false);
        MultiplayerLobbyManager.Instance.HostedLobby += OnLobbyHosted;
        MultiplayerLobbyManager.Instance.LeftLobby += OnLobbyLeft;
    }
    void OnLobbyHosted(LobbyInfo info)
    {
        _text.text = info.JoinCode;
        _text.gameObject.SetActive(true);
    }

    private void OnLobbyLeft()
    {
        _text.text = "";
        _text.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        if (MultiplayerLobbyManager.Instance == null) { return; }

        MultiplayerLobbyManager.Instance.HostedLobby -= OnLobbyHosted;
        MultiplayerLobbyManager.Instance.LeftLobby -= OnLobbyLeft;
    }
}
