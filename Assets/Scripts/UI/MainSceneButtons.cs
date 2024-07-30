using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneButtons : MonoBehaviour
{
    [SerializeField] private Button play;
    [SerializeField] private Button shop;
    [SerializeField] private Button settings;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        play.onClick.AddListener(() =>
        {
            RemoveAllListeners();
            SceneTransition.SwitchScene("LobbyScene");
        });

        shop.onClick.AddListener(() =>
        {
            RemoveAllListeners();
            SceneTransition.SwitchScene("ShopScene");
        });
        
        settings.onClick.AddListener(() =>
        {
            RemoveAllListeners();
            SceneTransition.SwitchScene("SettingsScene");
        });
    }

    private void RemoveAllListeners()
    {
        play.onClick.RemoveAllListeners();
        shop.onClick.RemoveAllListeners();
        settings.onClick.RemoveAllListeners();
    }
}
