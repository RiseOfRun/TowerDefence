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
        button.onClick.AddListener(SelectTower);
    }

    public void Init(TowerPattern Tower)
    {
        Pattern = Instantiate(Tower);
        Name.text = Pattern.Name;
        Cost.text = Pattern.Tower.Cost.ToString();
        Icon.sprite = Tower.Icon;
    }

    public void SelectTower()
    {
        if (Pattern.Tower.Cost > Player.Instance.Money)
        {
            return;
        }

        BuildManager.Instance.EnterToBuildMode(Pattern.Mirage, Pattern.Tower);
    }
}