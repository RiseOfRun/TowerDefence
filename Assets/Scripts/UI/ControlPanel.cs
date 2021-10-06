using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    public List<Button> Buttons;
    // Start is called before the first frame update
    void Start()
    {
    }

    void FreeAllButtons()
    {
        foreach (var button in Buttons)
        {
            button.interactable = true;
        }
    }
    
    public void Pause()
    {
        FreeAllButtons();
        TimeManager.Instance.CurrentTimeScale = 0;
    }

    public void TurnNormalSpeed()
    {
        FreeAllButtons();
        TimeManager.Instance.CurrentTimeScale = 1;
    }

    public void SetSpeed(float count)
    {
        FreeAllButtons();
        TimeManager.Instance.CurrentTimeScale = count;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
