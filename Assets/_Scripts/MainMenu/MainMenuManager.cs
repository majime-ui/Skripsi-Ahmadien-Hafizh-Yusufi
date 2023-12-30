using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject levelButton;

    [SerializeField]
    private Transform levelButtonParent;

    private void Start()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string sceneName = "Level " + i;

            // Instantiate a new button
            GameObject newButton = Instantiate(levelButton, levelButtonParent);

            // Add a Button component to the new button
            Button buttonComponent = newButton.GetComponent<Button>();

            // Add two listeners to the onClick event
            buttonComponent.onClick.AddListener(() => LoadLevel(sceneName));
            buttonComponent.GetComponentInChildren<TextMeshProUGUI>().text = sceneName;
        }
    }

    public void LoadLevel(string sceneName)
    {
        // Load the specified level
        SceneManager.LoadScene(sceneName);
    }
}