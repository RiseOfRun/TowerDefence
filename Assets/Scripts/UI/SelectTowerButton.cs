using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTowerButton : MonoBehaviour
{
    public TowerPattern Pattern;
    public Image Icon;
    public Text Cost;
    public Text Name;

    private Button button;

    void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    public void Init(TowerPattern Tower)
    {
        Pattern = Tower;
        Name.text = Pattern.Name;
        Cost.text = Pattern.Cost.ToString();
        Icon.sprite = Tower.Icon;
    }

    public void OnButtonClick()
    {
        if (Pattern.Cost > Player.Instance.Money)
        {
            return;
        }
        
        if (!Pattern.IsUpgrade)
        {
            
            BuildManager.Instance.EnterToBuildMode(Pattern.Mirage, Pattern);
            return;
        }
        Tower targetTower = TargetSystem.Instance.TargetedTower;
        var place = targetTower.transform.parent.GetComponent<Square>();
        Destroy(targetTower.gameObject);
        BuildManager.Instance.BuildPanel.OnFreeTower();
        BuildManager.Instance.Build(place,Pattern);
    }
}