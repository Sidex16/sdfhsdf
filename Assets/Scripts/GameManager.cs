using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class GameManager// : MonoBehaviour
{
    const int MIN_CARDS = 4;
    const int MAX_CARDS = 9;

    public static GameManager Instance { get; private set; }

    public static List<Transform> Cards;


    private NotificationView _notificationView;
    private CardFactory _cardFactory;

    private List<Card> chosenCards = new List<Card>();
    private List<Transform> cards;

    Vector3 initialScale = new Vector3(.46f, .391f, .46f);

    private enum State
    {
        WaitingToStart,
        ChoosingKing,
        ChoosingPerformers,
        WaitingForReroll
    }

    private State state;

    private int performersNumber;
    private int choosenPerformers;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        state = State.WaitingToStart;
    }

    public void Initialize(NotificationView notificationView)
    {
        _notificationView = notificationView;
        cards = CardFactory.Cards;
    }

    private void Start()
    {
        performersNumber = DetermineNumberOfPlayers();
        EventManager.OnRerollClicked += OnRerollClicked;
        EventManager.OnDeckClicked += OnDeckClicked;
        EventManager.OnCardClicked += OnCardClicked;
    }

    private void OnCardClicked(Card obj)
    {
        if (state != State.ChoosingPerformers)
        {
            return;
        }

        bool isAllUnique = false;

        if (!chosenCards.Contains(obj))
        {
            chosenCards.Add(obj);
            isAllUnique = true;
        }

        if (obj.TryGetComponent<King>(out King king)) return;

        if (isAllUnique && state == State.ChoosingPerformers)
        {
            choosenPerformers++;
        }

        if (choosenPerformers >= performersNumber)
        {
            performersNumber = DetermineNumberOfPlayers();
            choosenPerformers = 0;
            chosenCards.Clear();
            state = State.WaitingForReroll;
            _notificationView.Move(-5.85f);
        }
    }

    private void OnDeckClicked()
    {
        state = State.ChoosingPerformers;
        _notificationView.Move(-4.6f);

        if (state == State.ChoosingPerformers && performersNumber == cards.Count)
        {
            //Invoke("RotateAllCards", CardMover.Instance.GetKingFlipTime() + 1f);
        }

        switch (performersNumber)
        {
            case 1:
                _notificationView.SetText("Choose one person");
                break;
            case 2:
                _notificationView.SetText("Choose two people");
                break;
            default:
                _notificationView.SetText("Challenge for everyone!");
                break;
        }
    }

    private void OnRerollClicked()
    {
        if (CardMover.Instance.GetIsMoving()) return;

        performersNumber = DetermineNumberOfPlayers();
        state = State.WaitingToStart;
        _notificationView.Move(-5.85f);
    }


    private void RotateAllCards()
    {
        for (int i = 1; i < cards.Count; i++)
        {
            cards[i].DORotate(new Vector3(0, 180, 0), 0.5f);
        }
        state = State.WaitingForReroll;
    }


    private int DetermineNumberOfPlayers()
    {
        float chance = Random.Range(0f, 1f);
        int maxPlayers = cards.Count;

        if (chance < 0.6f)
        {
            return 1;
        }
        else if (chance < 0.9f)
        {
            return 2;
        }
        else
        {
            return maxPlayers;
        }
    }

    public bool IsWaitingToStart() { return state == State.WaitingToStart; }
    public bool IsChoosingPerformers() { return state == State.ChoosingPerformers; }

}
