using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager 
{
    public static event Action OnRerollClicked;
    public static event Action OnDeckClicked;
    public static event Action<Card> OnCardClicked;
    public static event Action OnChallengeForAll;


    public static void InvokeRerollClicked()
    {
        OnRerollClicked?.Invoke();
    }

    public static void InvokeDeckClicked()
    {
        OnDeckClicked?.Invoke();
    }

    public static void InvokeCardClicked(Card card)
    {
        OnCardClicked?.Invoke(card);
    }

    public static void InvokeChallengeForAll()
    {
        OnChallengeForAll?.Invoke();
    }
}
