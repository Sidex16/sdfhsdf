using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] InputManager _inputManager;
    [SerializeField] CardMover _cardMover;
    [SerializeField] ButtonsManager _buttonsManager;
    [SerializeField] NotificationView _notificationView;
    [SerializeField] CardsSO _cardsSO;
    [SerializeField] Randomizer _randomizer;
    [SerializeField] CardFlipper _cardFlipper;
    [SerializeField] CardFactory _cardFactory;
    [SerializeField] PlayersInfo _playersInfo;
    [SerializeField] StateMachine _stateMachine;
    [SerializeField] CardPositionCalculator _cardPositionCalculator;


    private Randomizer randomizer;
    private CardFlipper cardFlipper;
    private CardFactory cardFactory;
    private PlayersInfo playersInfo;
    private StateMachine stateMachine;
    private CardPositionCalculator cardPositionCalculator;

    private void Awake()
    {
        //NotificationView notific = Instantiate(_notificationView);

        playersInfo = new PlayersInfo();
        cardFlipper = new CardFlipper();
        cardFactory = new CardFactory(_cardsSO);
        stateMachine = new StateMachine(_notificationView);
        cardPositionCalculator = new CardPositionCalculator();
        randomizer = new Randomizer();

        //_playersInfo.Initialize();
        //_cardFlipper.Initialize();
        //_cardFactory.Initialize(_cardsSO);
        //_stateMachine.Initialize(_notificationView);
        //_cardPositionCalculator.Initialize();
        //_randomizer.Initialize();

        _cardMover.Initialize(cardPositionCalculator);
        _inputManager.Initialize(stateMachine);

    }

}
