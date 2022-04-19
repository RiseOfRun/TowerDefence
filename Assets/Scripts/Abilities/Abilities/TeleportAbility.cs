using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu(menuName = "Abilities/TeleportAbility")]
public class TeleportAbility : AreaAbility
{
    public float ReactivateDelay = 0;
    public override bool Perform()
    {
        var enemies = Physics.OverlapSphere(indicator.transform.position,
            Range,LayerMask.GetMask("Enemies"));
        List<Enemy> detectedEnemies = new List<Enemy>();
        foreach (var enemy in enemies)
        {
            var de = enemy.GetComponent<Enemy>();
            if (de!=null)
            {
                detectedEnemies.Add(de);
                de.ReturnToStart();
            }
        }

        for (int i = 0; i < detectedEnemies.Count; i++)
        {
            float delay = (i + 1) * ReactivateDelay;
            var current = detectedEnemies[i];
            LevelController.Instance.StartCoroutine(Reactiovate(current, delay));
        }
        return true;
    }

    IEnumerator Reactiovate(Enemy enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        enemy.gameObject.SetActive(true);
    }
}
