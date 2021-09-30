using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Queue<EnemyGroup> WavePopulation = new Queue<EnemyGroup>();
    public Transform[] WayPoints;

    // Start is called before the first frame update
    void Start()
    {
    }
    public void SpawnWave(WaveSettings currentWave, WaveController controller)
    {
        WavePopulation = new Queue<EnemyGroup>();
        foreach (var party in currentWave.EnemyGroups)
        {
            for (int i = 0; i < party.Size; i++)
            {
                WavePopulation.Enqueue(party);
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
            EnemyGroup current = WavePopulation?.Peek();
            if (current==null)
            {
                break;
            }
            WavePopulation.Dequeue();
            Enemy newEnemy = controller.GetEnemy(current);
            newEnemy.Waypoints = WayPoints;
            Vector3 position = transform.position;
            newEnemy.gameObject.transform.position += new Vector3(position.x,0,position.z);
            yield return new WaitForSeconds(settings.Delay);
        }
    }
}
