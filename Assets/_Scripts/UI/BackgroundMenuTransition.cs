using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMenuTransition : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPlayButtonClick()
    {
        animator.SetTrigger("PlayButtonClick");

        //Invoke("LoadGameScene", 1.0f);
    }

    /*private void LoadGameScene()
    {
        SceneManager.LoadScene("Load Game");
    }*/
}
