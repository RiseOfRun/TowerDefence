using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] WayPoints;
    private readonly Queue<EnemyGroup> WavePopulation = new Queue<EnemyGroup>();
    
    public void SpawnWave(WaveSettings currentWave, WaveController controller)
    {
        WavePopulation.Clear();
        foreach (var party in currentWave.EnemyGroups)
        {
            for (int i = 0; i < party.Size; i++)
            {
                WavePopulation.Enqueue(party);
            }
        }
        StartCoroutine(Spawn(currentWave,controller));
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
            Enemy newEnemy = controller.GetEnemy(current,transform.position);
            newEnemy.Waypoints = WayPoints;
            yield return new WaitForSeconds(settings.Delay);
        }
    }
}
