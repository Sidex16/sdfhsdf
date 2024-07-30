using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Runtime.CompilerServices;
using System.Collections;

public class Randomizer //: MonoBehaviour
{
    private List<TextMeshPro> nameTexts = new List<TextMeshPro>();

    private List<string> names = new List<string>();

    public Randomizer()
    {
        LoadPlayerNames();

        EventManager.OnRerollClicked += OnRerollClicked;
        GetNameTexts();
        Coroutines.Start(ShuffleNames());
    }
    public void Initialize()
    {
        LoadPlayerNames();

        EventManager.OnRerollClicked += OnRerollClicked;
        GetNameTexts();
        Coroutines.Start(ShuffleNames());
    }

    private void OnRerollClicked()
    {
        if (CardMover.Instance.GetIsMoving()) return;

        Coroutines.Start(ShuffleNames(0.6f));
    }

    private IEnumerator ShuffleNames(float time = 0)     {
        yield return new WaitForSeconds(time);
        Shuffle(names);
        for (int i = 0; i < nameTexts.Count && i < names.Count; i++)
        {
            nameTexts[i].text = names[i];
        }
    }

    private void GetNameTexts()
    {
        List<Transform> cards = CardFactory.Cards;

        foreach (Transform card in cards)
        {

            TextMeshPro textMeshPro = card.GetComponentInChildren<TextMeshPro>();
            nameTexts.Add(textMeshPro);
        }
    }

    private void LoadPlayerNames()
    {
        for(int i = 0; i < PlayersInfo.PlayersCount; i++)
        {
            names.Add(PlayersInfo.Names[i]);
        } 
       
    }


    private void Shuffle(List<string> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            string temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

  
}
