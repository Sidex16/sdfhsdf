using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;



public class InputManager : MonoBehaviour
{
    private Vector3 startFingerPos;

    private StateMachine _stateMachine;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            Vector3 screenPos = Touch.activeTouches[0].screenPosition;

            if (CardMover.Instance.GetIsMoving()) return;

            if (Touch.activeTouches[0].phase == TouchPhase.Began)
            {
                startFingerPos = Camera.main.ScreenToWorldPoint(screenPos);

                RaycastHit2D hit = Physics2D.Raycast(startFingerPos, Vector2.zero);
                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<Card>())
                    {
                        if (CardMover.Instance.GetIsOnDeck() &&
                            _stateMachine.State == StateMachine.States.WaitingToStart)
                        {
                            CardMover.Instance.SetIsOnDeck(false);
                            CardMover.Instance.SetCanShowKing(true);
                            EventManager.InvokeDeckClicked();
                        }
                        else if (_stateMachine.State == StateMachine.States.ChoosingPerformers)
                        {
                            EventManager.InvokeCardClicked(hit.collider.GetComponent<Card>());
                        }
                    }
                }
            }
            else if (Touch.activeTouches[0].phase == TouchPhase.Moved)
            {

            }
            else if (Touch.activeTouches[0].phase == TouchPhase.Ended)
            {

            }
        }
    }

    public Touch GetFinger() { return Touch.activeTouches[0]; }
}