using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour
{
    public static LobbyScene Instance { get; private set; }

    public event EventHandler OnInputError;
    public event EventHandler OnAddError;

    [SerializeField] private Button start;
    [SerializeField] private Button addPlayer;

    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject prefab;

    [SerializeField] private float offset;

    private List<GameObject> players = new List<GameObject>();
    private List<string> playerNames = new List<string>();

    private GameObject initialPrefabClone;

    private bool canClick = true;
    private float timeToMove = .2f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        initialPrefabClone = Instantiate(prefab);
        initialPrefabClone.SetActive(false);

        start.onClick.AddListener(() =>
        {
            if (ReadInputFields())
            {
                SavePlayerNames();
                start.onClick.RemoveAllListeners();
                SceneTransition.SwitchScene("GameScene");
            }
        });

        addPlayer.onClick.AddListener(() =>
        {
            if (canClick)
            {
                GenerateField();
                StartCoroutine(ClickCooldown());
            }
        });
    }

    private void Start()
    {
        GenerateFirstFields();
    }

    private void GenerateFirstFields()
    {
        for (int i = 0; i < 3; i++)
        {
            GenerateField().SetActive(true);
        }
    }

    private GameObject GenerateField()
    {
        if (CanAddPlayer())
        {
            GameObject newPlayer = CreateNewPlayer();
            PositionNewPlayer(newPlayer);
            players.Insert(0, newPlayer);
            MoveExistingPlayers(newPlayer);
            return newPlayer;
        }
        else
        {
            OnAddError?.Invoke(this, EventArgs.Empty);
            return null;
        }
    }

    private bool CanAddPlayer()
    {
        return players.Count < 9;
    }

    private GameObject CreateNewPlayer()
    {
        GameObject newPlayer = Instantiate(initialPrefabClone);
        newPlayer.SetActive(false);
        newPlayer.transform.SetParent(canvas.transform, false);
        return newPlayer;
    }
    private void PositionNewPlayer(GameObject newPlayer)
    {
        RectTransform rectTransform = newPlayer.GetComponent<RectTransform>();
        if (players.Count > 0)
        {
            RectTransform firstPlayerRect = players[0].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = firstPlayerRect.anchoredPosition;
        }
    }

    private void MoveExistingPlayers(GameObject newPlayer)
    {
        for (int i = 1; i < players.Count; i++)
        {
            RectTransform playerRect = players[i].GetComponent<RectTransform>();
            Vector2 targetPosition = new Vector2(playerRect.anchoredPosition.x, playerRect.anchoredPosition.y - offset);
            if (players.Count > 3)
                StartCoroutine(MoveToPosition(playerRect, targetPosition, timeToMove, newPlayer));
            else
                playerRect.anchoredPosition = targetPosition;
        }
    }

    private IEnumerator ClickCooldown()
    {
        canClick = false;
        yield return new WaitForSeconds(timeToMove);
        canClick = true;
    }
    private IEnumerator MoveToPosition( RectTransform transform, Vector2 target, float duration,GameObject newPlayer = null)
    {
        Vector2 start = transform.anchoredPosition;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            transform.anchoredPosition = Vector2.Lerp(start, target, time / duration);
            yield return null;
        }

        transform.anchoredPosition = target;
        newPlayer.SetActive(true);
    }

    private bool ReadInputFields()
    {
        foreach (GameObject player in players)
        {
            TMP_InputField inputField = player.GetComponentInChildren<TMP_InputField>();
            if (inputField != null)
            {
                if (inputField.text == string.Empty)
                {
                    playerNames.Clear();
                    OnInputError?.Invoke(this, EventArgs.Empty);
                    return false;
                }
                else
                {
                    string text = inputField.text;
                    playerNames.Add(text);
                }
            }
        }

        return true;
    }

    private void SavePlayerNames()
    {
        for (int i = 0; i < playerNames.Count; i++)
        {
            PlayerPrefs.SetString("PlayerName_" + i, playerNames[i]);
        }
        PlayerPrefs.SetInt("PlayerCount", playerNames.Count);
        PlayerPrefs.Save();
    }

    public void UpdatePlayerPositions()
    {
        if (players.Count == 0)
            return;

        RectTransform firstRectTransform = initialPrefabClone.GetComponent<RectTransform>();

        for (int i = 0; i < players.Count; i++)
        {
            RectTransform rectTransform = players[i].GetComponent<RectTransform>();
            Vector2 targetPosition;
            if (i == 0)
                targetPosition = firstRectTransform.anchoredPosition;
            else
                targetPosition = new Vector2(firstRectTransform.anchoredPosition.x, firstRectTransform.anchoredPosition.y - i * offset);

            StartCoroutine(MoveToPosition(rectTransform, targetPosition, timeToMove));
        }
    }

    public List<GameObject> GetPlalyers() { return players; }
}
