using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyNotification : MonoBehaviour
{
    public static LobbyNotification Instance { get; private set; }

    [SerializeField] private TextMeshPro notificationText;

    private bool isTweening = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ShowNotification(string notificationMessage)
    {
        if (isTweening)
        {
            return;
        }

        notificationText.text = notificationMessage;

        isTweening = true;

        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(transform.DOMoveY(-4.5f, 0.6f).SetEase(Ease.InOutExpo).SetDelay(0.3f));
        mySequence.AppendInterval(1.5f);
        mySequence.Append(transform.DOMoveY(-5.85f, 0.6f).SetEase(Ease.InOutExpo).SetDelay(0.3f));

        mySequence.OnComplete(() =>
        {
            isTweening = false;
        });
    }
}
