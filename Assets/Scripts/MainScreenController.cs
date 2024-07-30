using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MainScreenController : MonoBehaviour
{
    //[SerializeField] private GameObject mainScreenMid;
    [SerializeField] private TextMeshProUGUI text;
    //[SerializeField]
    private Animator animator;

    private void Start()
    {
        Application.targetFrameRate = 60;
        animator = gameObject.GetComponent<Animator>();
        SmoothlyRotateCircle();
        SmoothlyBlinkText();
    }

    private void SmoothlyBlinkText()
    {
        text.DOFade(0f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void SmoothlyRotateCircle()
    {
        transform.DORotate(new Vector3(0f, 0f, -360f), 100f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    public void OnPlayButtonClicked()
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator not found on the GameObject. Please attach an Animator component.");
            return;
        }
        animator.SetTrigger("Splash");
    }


    public void OnAnimationEnd()
    {
        SceneTransition.SwitchScene("LobbyScene");
        DOTween.KillAll();
    }
}
