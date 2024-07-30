using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{

    [SerializeField] private Button reroll;
    [SerializeField] private Button home;
 

    private void Start()
    {
        reroll.onClick.AddListener(() =>
        {
            EventManager.InvokeRerollClicked();
        });
        home.onClick.AddListener(() => 
        {
            home.onClick.RemoveAllListeners();
            SceneTransition.SwitchScene("_MainScreen");
        });
    }
}
