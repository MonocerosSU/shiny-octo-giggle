using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WaveSpawner : MonoBehaviour
{
    [Header("Spawn vectors:")]
    public Vector3 spawnMin;
    public Vector3 spawnMax;

    [Header("Objects to spawn:")]
    public List<ListWrapper> waves;

    [Header("Time between spawning:")]
    public float waveCooldown = 5f;
    public float objectCooldown = 1f;

    private Stopwatch waveStopwatch;
    private Stopwatch waveObjectStopwatch;
    private List<GameObject> currentWave; 

    private void Start ()
    {
	    this.waveStopwatch = new Stopwatch();
        this.waveStopwatch.Start();

        this.waveObjectStopwatch = new Stopwatch();
        this.waveObjectStopwatch.Start();

        this.currentWave = this.SelectRandomWave();
    }

    private void Update ()
    {
	    this.waveStopwatch.Update();
        this.waveObjectStopwatch.Update();

        if (this.waveStopwatch.Time >= this.waveCooldown)
        {
            if (this.currentWave.Count == 0)
            {
                this.currentWave = this.SelectRandomWave();
                this.waveStopwatch.Restart();
            }
            else
            {
                if (this.waveObjectStopwatch.Time >= this.objectCooldown)
                {
                    this.SpawnObject(this.currentWave[0]);
                    this.currentWave.RemoveAt(0);
                    this.waveObjectStopwatch.Restart();
                }
            }
        }
	}

    private void SpawnObject(GameObject obj)
    {
        Vector3 position = this.RandomVector3(this.spawnMin, this.spawnMax);

        GameObject.Instantiate(obj, position, obj.transform.rotation);
    }
    
    private Vector3 RandomVector3(Vector3 vectorMin, Vector3 vectorMax)
    {
        Vector3 result = new Vector3(
            Random.Range(vectorMin.x, vectorMax.x),
            Random.Range(vectorMin.y, vectorMax.y),
            Random.Range(vectorMin.z, vectorMax.z));

        return result;
    }

    private List<GameObject> SelectRandomWave()
    {
        ListWrapper resultWave = this.waves[Random.Range(0, this.waves.Count)];

        return resultWave.GameObjects.ToList();
    }
}
