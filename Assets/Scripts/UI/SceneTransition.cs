using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    private static SceneTransition instance;
    private static bool shouldPlayOpeningAnimation = false;

    private Animator animator;
    private AsyncOperation asyncOperation;

    public static void SwitchScene(string sceneName)
    {
        instance.animator.SetTrigger("SceneClosing");

        instance.asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        instance.asyncOperation.allowSceneActivation = false;
    }

    private void Start()
    {
            instance = this;

        animator = GetComponent<Animator>();
    
        if (shouldPlayOpeningAnimation)
        {
            animator.SetTrigger("SceneOpening");
            shouldPlayOpeningAnimation = false;
        }
    }

    public void OnAnimaionOver()
    {
        asyncOperation.allowSceneActivation = true;
        shouldPlayOpeningAnimation = true;
    }
}
