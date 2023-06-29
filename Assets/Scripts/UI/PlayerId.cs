using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class PlayerId : MonoBehaviour
{
    private TMP_Text _text;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = "";
        _text.gameObject.SetActive(false);
        MultiplayerLobbyManager.Instance.SignedIn += OnSignIn;
    }

    void OnSignIn(string playerId)
    {
        _text.gameObject.SetActive(true);
        _text.text = $"Player: {playerId}";
    }

    void OnDestroy()
    {
        if (MultiplayerLobbyManager.Instance == null)
            return;

        MultiplayerLobbyManager.Instance.SignedIn -= OnSignIn;
    }
}
