using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float Range;
    public int Damage=200;
    private Enemy target;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.TowerBuilt(this);
        Debug.Log($"towerDamage = {Damage}");
    }

    void FindTarget()
    {
        var objects = Physics.OverlapSphere(transform.position,Range);
        foreach (var item in objects)
        {
            if (item.gameObject.GetComponent<Enemy>())
            {
                target = item.gameObject.GetComponent<Enemy>();
                StartCoroutine(Kill());
                StartCoroutine(DrawLazer());
                return;
            }
        }
    }

    IEnumerator DrawLazer()
    {
        while (target!=null)
        {
            Debug.DrawLine(transform.position+new Vector3(0,1f,0) ,target.transform.position,Color.red);
            yield return null;
        }
    }

    IEnumerator Kill()
    {
        while (target!=null)
        {
            target.Health -= Damage;
            yield return new WaitForSeconds(0.2f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (target==null)
        {
            FindTarget();
        }
    }
}
