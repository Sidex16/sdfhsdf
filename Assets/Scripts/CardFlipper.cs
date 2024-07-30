using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlipper// : MonoBehaviour
{
    public CardFlipper()
    {
        EventManager.OnCardClicked += OnCardClicked;
    }
    public void Initialize()
    {
        EventManager.OnCardClicked += OnCardClicked;
    }

    private void OnCardClicked(Card obj)
    {
        if (CardMover.Instance.GetIsMoving()) return;

        obj.gameObject.transform.DORotate(new Vector3(0, 180, 0), .5f);
    }

}
