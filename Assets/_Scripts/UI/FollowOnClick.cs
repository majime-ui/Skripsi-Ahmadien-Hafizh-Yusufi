using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class FollowOnClick : MonoBehaviour
{
    public Button button;
    public AudioSource buttonClickSound;

    private RectTransform textRectTransform;
    private TextMeshProUGUI tmpComponent;
    private Vector2 originalPosition;

    private void Awake()
    {
        tmpComponent = button.GetComponentInChildren<TextMeshProUGUI>();

        if (tmpComponent != null)
        {
            textRectTransform = tmpComponent.GetComponent<RectTransform>(); ;
            originalPosition = textRectTransform.anchoredPosition;
        }
        else
        {
            Debug.LogError("Text component not found as a child of the button.");
        }

        if(buttonClickSound == null)
        {
            Debug.LogError("AudioSource component not assigned. Please assign an AudioSource with an audio clip.");
        }
    }


    public void OnButtonClick()
    {
        if (textRectTransform != null)
        {
            MoveTextToBottom();

            if(buttonClickSound != null)
            {
                buttonClickSound.Play();
            }

            Invoke("MoveTextToOriginalPosition", 0.05f);
        }
    }

    private void MoveTextToBottom()
    {
        Vector2 newPosition = new Vector2(originalPosition.x, 0f);

        textRectTransform.anchoredPosition = newPosition;
    }

    private void MoveTextToOriginalPosition()
    {
        textRectTransform.anchoredPosition = originalPosition;
    }
}
