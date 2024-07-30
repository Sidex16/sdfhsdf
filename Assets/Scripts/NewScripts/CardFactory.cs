using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class CardFactory //: MonoBehaviour
{
    const int MIN_CARDS = 4;
    const int MAX_CARDS = 9;

    public static List<Transform> Cards => cards;

    private static List<Transform> cards;
    private CardsSO cardsSO;

    private Vector3 initialScale = new Vector3(.46f, .391f, .46f);

    public CardFactory(CardsSO cardsSO)
    {
        this.cardsSO = cardsSO;
        CreateCards();
    }
    public void Initialize(CardsSO cardsSO)
    {
        this.cardsSO = cardsSO;
        CreateCards();
    }

    public void CreateCards()
    {
        cards = new List<Transform>();

        Vector3 scaleFactor = GetScaleFactor(PlayersInfo.PlayersCount);

        for (int i = 0; i < PlayersInfo.PlayersCount; i++)
        {
            Transform newCard = GameObject.Instantiate(cardsSO.cards[i], Vector3.zero, Quaternion.identity);
            newCard.localScale = scaleFactor;
            cards.Add(newCard);
        }
    }

    private Vector3 GetScaleFactor(int cardCount)
    {
        float scalePercentage = 1.0f;

        if (cardCount > MIN_CARDS)
        {
            scalePercentage = 1.0f - 0.07f * (cardCount - MIN_CARDS); // Зменшення на 7% за кожну додаткову карту
            scalePercentage = Mathf.Max(scalePercentage, 0.5f); // Мінімальне масштабування 50%
        }

        return initialScale * scalePercentage;
    }

}
