using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Button exit;

    private void Awake()
    {
        exit.onClick.AddListener(() =>
        {
            exit.onClick.RemoveAllListeners();
            SceneTransition.SwitchScene("_MainScene");
        });
    }
}
