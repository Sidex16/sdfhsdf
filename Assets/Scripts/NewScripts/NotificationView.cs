using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class NotificationView : MonoBehaviour
{
    [SerializeField] private TextMeshPro notificationText;

    public void SetText(string text)
    {
        notificationText.text = text;
    }

    public void Move(float y)
    {
        transform.DOMoveY(y, 0.6f).SetEase(Ease.InOutExpo).SetDelay(0.3f);
    }
}
