using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ToolTipPanel : MonoBehaviour
{
    public TMP_Text Prompt;
    [FormerlySerializedAs("ToolTips")] public List<string> Prompts;
    public Button PrevPageButton;
    public Button NextPageButton;
    private int currentPage=0;

    void Start()
    {
        if (Prompts.Count<2)
        {
            PrevPageButton.gameObject.SetActive(false);
            NextPageButton.gameObject.SetActive(false);
        }
        else
        {
            NextPageButton.onClick.AddListener(GoToNextPage);
            PrevPageButton.onClick.AddListener(GoToPrevPage);
        }
        PrevPageButton.interactable = false;
        Prompt.text = Prompts[currentPage];

    }
    void GoToNextPage()
    {
        currentPage++;
        if (currentPage == Prompts.Count-1)
        {
            NextPageButton.interactable = false;
        }
        PrevPageButton.interactable = true;
        Prompt.text = Prompts[currentPage];
    }

    void GoToPrevPage()
    {
        currentPage--;
        if (currentPage == 0)
        {
            PrevPageButton.interactable = false;
        }

        NextPageButton.interactable = true;
        Prompt.text = Prompts[currentPage];
    }
}
