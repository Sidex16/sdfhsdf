using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    private const string DELETE_NOTIFICATION = "In game must be at least three players";
    private const string ENTER_NOTIFICATION = "You have to fill all fields";
    private const string ADD_NOTIFICATION = "You cannot add more than nine players";

    [SerializeField] private Button delete;
    private LobbyScene lobbyScene;
    private LobbyNotification notification;

    private List<GameObject> players;

    private void Awake()
    {
        lobbyScene = LobbyScene.Instance;
        notification = LobbyNotification.Instance;

        delete.onClick.AddListener(() =>
        {
            if (players.Count > 3)
            {
                int index = players.IndexOf(gameObject);

                if (index != -1)
                {
                    players.Remove(gameObject);
                    Destroy(gameObject);
                    lobbyScene.UpdatePlayerPositions();
                }
            }
            else
            {
                notification.ShowNotification(DELETE_NOTIFICATION);
            }
        });
    }

    private void Start()
    {
        players = lobbyScene.GetPlalyers();
    }

    private void OnEnable()
    {
        lobbyScene.OnInputError += LobbyScene_OnInputError;
        lobbyScene.OnAddError += LobbyScene_OnAddError;
    }

    private void OnDisable()
    {
        lobbyScene.OnInputError -= LobbyScene_OnInputError;
        lobbyScene.OnAddError -= LobbyScene_OnAddError;
    }
    private void LobbyScene_OnAddError(object sender, System.EventArgs e)
    {
        notification.ShowNotification(ADD_NOTIFICATION);
    }
    private void LobbyScene_OnInputError(object sender, System.EventArgs e)
    {
        notification.ShowNotification(ENTER_NOTIFICATION);
    }
}
