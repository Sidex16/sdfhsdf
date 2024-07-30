using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMover : MonoBehaviour
{
    public static CardMover Instance { get; private set; }

    private CardPositionCalculator _cardPositionCalculator;

    private List<Transform> cards;

    private List<Vector3> points;
    private List<Vector3> defaultPoints;
    private Vector3 initialScale;

    private bool isOnDeck = true;
    private bool isMoving = false;
    private bool canShowKing = false;

    private float moveDuration = .5f;
    private float moveDelay = .12f;

    private float scaleFactor = 1.3f;
    private float scaleDuration = 0.5f;

    private float rotationDuration = 0.5f;
    
    private float kingFlipTime;

    private void Awake()
    {
        

    }

    public void Initialize(CardPositionCalculator cardPositionCalculator)
    {
        if (Instance == null)
            Instance = this;

        defaultPoints = new List<Vector3>();

        _cardPositionCalculator = cardPositionCalculator;

        cards = CardFactory.Cards;
        SaveDefaultPositions();
        EventManager.OnDeckClicked += OnDeckClicked;
        EventManager.OnRerollClicked += OnRerollClicked;
        EventManager.OnChallengeForAll += OnChallengeForAll; ;
        points = _cardPositionCalculator.GetPoints();
        kingFlipTime = cards.Count * moveDelay + moveDuration;
    }

    private void Start()
    {
        
    }

    private void OnChallengeForAll()
    {
        Invoke(nameof(RotateAllCards), kingFlipTime + 1f);
    }

    private void OnDeckClicked()
    {
        if (isMoving) return;

        Move(points);
    }

    private void OnRerollClicked()
    {
        if (isMoving) return;

        SetDefaultRotation();
        isOnDeck = true;
        canShowKing = false;
        Move(defaultPoints);
    }

    private void Move(List<Vector3> points)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            float delay;
            if (cards.Count > 4)
            {
                if (i == 0)
                {
                    delay = i * moveDelay;
                    continue;
                }

            }
            delay = i * moveDelay;
            cards[i].DOMove(points[i], moveDuration).SetEase(Ease.OutCubic).SetDelay(delay)
                .OnStart(() => isMoving = true);
        }

        float totalDuration = kingFlipTime;
        
        if (defaultPoints[0] == cards[0].transform.position && canShowKing)
        {
            Sequence firstObjectSequence = DOTween.Sequence();
            firstObjectSequence.AppendInterval(kingFlipTime)
                .Append(cards[0].DOScale(cards[0].localScale * scaleFactor, scaleDuration).SetEase(Ease.OutCubic))
                .Append(cards[0].DORotate(new Vector3(0, 180, 0), rotationDuration).SetEase(Ease.OutCubic))
                .OnStart(() => isMoving = true);
            totalDuration += scaleDuration + rotationDuration;

        }

        Invoke(nameof(SetIsMovingToFalse), totalDuration);
    }

    private void SetDefaultRotation()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].DORotate(new Vector3(0, 0, 0), .3f);
        }
        cards[0].DOScale(initialScale, scaleDuration).SetEase(Ease.OutCubic);
    }

    private void RotateAllCards()
    {
        for (int i = 1; i < cards.Count; i++)
        {
            cards[i].DORotate(new Vector3(0, 180, 0), 0.5f);
        }
    }

    private void SaveDefaultPositions()
    {
        foreach (Transform card in cards)
        {
            Transform pos = new GameObject().transform;
            pos.position = card.position;
            pos.localScale = card.localScale;
            defaultPoints.Add(pos.position);
        }

        initialScale = cards[0].localScale;
    }

    public void SetIsOnDeck(bool isOnDeck)
    {
        this.isOnDeck = isOnDeck;
    }

    public bool GetIsOnDeck() { return isOnDeck; }
    public bool GetCanShowKing() { return canShowKing; }
    public bool GetIsMoving() { return isMoving; }
    public float GetKingFlipTime() { return kingFlipTime; }
    public void SetCanShowKing(bool canShowKing) { this.canShowKing = canShowKing; }
    private void SetIsMovingToFalse() { isMoving = false; }
}
