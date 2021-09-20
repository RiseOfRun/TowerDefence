using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Queue<Enemy> WavePopulation = new Queue<Enemy>();
    public Transform[] WayPoints;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SpawnWave(WaveSettings currentWave, WaveController controller)
    {
        WavePopulation = new Queue<Enemy>();
        int spawned = 0;
        while (spawned<currentWave.Size)
        {
            foreach (Enemy enemy in currentWave.EnemyGroups.SelectMany(enemyGroup => enemyGroup.EnemiesInGroup))
            {
                if (spawned < currentWave.Size)
                {
                    WavePopulation.Enqueue(enemy);
                    spawned++;
                }
                else
                {
                    StartCoroutine(Spawn(currentWave, controller));
                    return;
                }
            }
        }
        StartCoroutine(Spawn(currentWave,controller));


    }

    // Update is called once per frame
    void Update()
    {
  
    }

    IEnumerator Spawn(WaveSettings settings, WaveController controller)
    {
        for (int i = 0; i < settings.Size; i++)
        {
            Enemy current = WavePopulation?.Peek();
            if (current==null)
            {
                break;
            }
            current.Waypoints = WayPoints;
            WavePopulation.Dequeue();
            Enemy newEnemy = Instantiate(current, LevelController.Instance.UnitPool.transform);
            newEnemy.Init(controller.ScoreMulti,controller.HealthMulti,controller.SpeedMulti,LevelController.Instance.currentWave);
            Vector3 position = transform.position;
            newEnemy.gameObject.transform.position += new Vector3(position.x,0,position.z);
            yield return new WaitForSeconds(settings.Delay);
        }
    }
}
