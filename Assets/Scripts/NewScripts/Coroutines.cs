using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Coroutines : MonoBehaviour
{
    private static Coroutines instance { 
        get
        {
            if(m_instance == null)
            {
                GameObject go = new GameObject("Coroutines");
                m_instance = go.AddComponent<Coroutines>();
            }
            return m_instance;
        } }

    private static Coroutines m_instance;

    public static Coroutine Start(IEnumerator routine)
    {
        return instance.StartCoroutine(routine);
    }

    public static void Stop(Coroutine routine)
    {
        if (routine != null)
            instance.StopCoroutine(routine);
    }
}
