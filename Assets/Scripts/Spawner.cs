using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Enemy> Enemies;
    public Queue<Enemy> WavePopulation = new Queue<Enemy>();
    public Transform[] WayPoints;

    public int WaweSize = 5;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SpawnWave(WaveSettings currentWave)
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WavePopulation.Count != 0) return;
        for (int i = 0; i < WaweSize; i++)
        {
            foreach (Enemy enemy in Enemies)
            {
                WavePopulation.Enqueue(enemy);
            }
        }
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(4f);
        for (int i = 0; i < WaweSize; i++)
        {
            Enemy current = WavePopulation.Peek();
            current.Waypoints = WayPoints;
            WavePopulation.Dequeue();
            Enemy newEnemy = Instantiate(current);
            var position = transform.position;
            newEnemy.gameObject.transform.position += new Vector3(position.x,0,position.z);
            yield return new WaitForSeconds(1f);
        }
    }
}
