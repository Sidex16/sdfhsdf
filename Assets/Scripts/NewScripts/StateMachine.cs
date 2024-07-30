using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class StateMachine //: MonoBehaviour
{
    public enum States
    {
        WaitingToStart,
        ChoosingKing,
        ChoosingPerformers,
        WaitingForReroll
    }

    public States State { get => _state; }

    private States _state;
    
    private NotificationView _notificationView;

    private int performersNumber;
    private int choosenPerformers;

    private List<Card> chosenCards = new List<Card>();


    public StateMachine(NotificationView notificationView)
    {
        _notificationView = notificationView;
        EventManager.OnRerollClicked += OnRerollClicked;
        EventManager.OnDeckClicked += OnDeckClicked;
        EventManager.OnCardClicked += OnCardClicked; ;
        _state = States.WaitingToStart;
        performersNumber = DetermineNumberOfPlayers();
    }
    public void Initialize(NotificationView notificationView)
    {
        _notificationView = notificationView;
        EventManager.OnRerollClicked += OnRerollClicked;
        EventManager.OnDeckClicked += OnDeckClicked;
        EventManager.OnCardClicked += OnCardClicked; ;
        _state = States.WaitingToStart;
        performersNumber = DetermineNumberOfPlayers();
    }

    private void OnRerollClicked()
    {
        if (CardMover.Instance.GetIsMoving()) return;

        performersNumber = DetermineNumberOfPlayers();
        _state = States.WaitingToStart;
        _notificationView.Move(-5.85f);
    }

    private void OnDeckClicked()
    {
        _state = States.ChoosingPerformers;
        _notificationView.Move(-4.6f);

        if (_state == States.ChoosingPerformers && performersNumber == CardFactory.Cards.Count)
        {
            EventManager.InvokeChallengeForAll();
            _state = States.WaitingForReroll;

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

    private void OnCardClicked(Card obj)
    {
        if (_state != States.ChoosingPerformers)
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

        if (isAllUnique && _state == States.ChoosingPerformers)
        {
            choosenPerformers++;
        }

        if (choosenPerformers >= performersNumber)
        {
            performersNumber = DetermineNumberOfPlayers();
            choosenPerformers = 0;
            chosenCards.Clear();
            _state = States.WaitingForReroll;
            _notificationView.Move(-5.85f);
        }
    }

    private int DetermineNumberOfPlayers()
    {
        float chance = Random.Range(0f, 1f);
        int maxPlayers = CardFactory.Cards.Count;

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
}
