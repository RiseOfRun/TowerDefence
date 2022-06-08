using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
class Prompts
{
    [SerializeField] public List<string> PromptsOnLevel = new List<string>();
}
public class ToolTipHelper : MonoBehaviour
{
    [SerializeField] private List<Prompts> AvailablePrompts = new List<Prompts>();
    public Button TooltipAlert;
    public ToolTipPanel Panel;

    private void Start()
    {
        int k = 0;
        if (AvailablePrompts.Count < GameManager.CurrentLevel || 
            AvailablePrompts[GameManager.CurrentLevel].PromptsOnLevel.Count==0)
        {
            TooltipAlert.gameObject.SetActive(false);
            gameObject.SetActive(false);
            return;
        }
        Panel.Prompts = AvailablePrompts[GameManager.CurrentLevel].PromptsOnLevel;
        int isAvailable = PlayerPrefs.GetInt("ToolTipsOnLevel" + GameManager.CurrentLevel,1);
        TooltipAlert.onClick.AddListener(OnPressAlertButton);
        if (isAvailable==1)
        {
            TooltipAlert.gameObject.SetActive(true);
        }
        else
        {
            TooltipAlert.GetComponent<Animator>().enabled = false;
        }
    }

    void OnPressAlertButton()
    {
        Panel.gameObject.SetActive(true);
        TooltipAlert.GetComponent<Animator>().enabled = false;
        PlayerPrefs.SetInt("ToolTipsOnLevel" + GameManager.CurrentLevel,0);
    }
}
