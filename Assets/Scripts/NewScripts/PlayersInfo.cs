using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersInfo //: MonoBehaviour
{
    public static List<string> Names = new List<string>();
    public static int PlayersCount;

    public PlayersInfo()
    {
        PlayersCount = PlayerPrefs.GetInt("PlayerCount", 0);
        for (int i = 0; i < PlayersCount; i++)
        {
            string playerName = PlayerPrefs.GetString("PlayerName_" + i, "Unknown");

            Names.Add(playerName);
        }
    }
    public void Initialize()
    {
        PlayersCount = PlayerPrefs.GetInt("PlayerCount", 0);
        for (int i = 0; i < PlayersCount; i++)
        {
            string playerName = PlayerPrefs.GetString("PlayerName_" + i, "Unknown");

            Names.Add(playerName);
        }
    }

}
