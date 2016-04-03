using UnityEngine;
using System.Collections;

public class Stopwatch
{
    private bool isRunning;

    private float time;

    public float Time {
        get
        {
            return this.time;

        }
        private set
        {
            this.time = value;
        }
    }
    
    public void Update()
    {
        if (this.isRunning)
        {
            this.Time += UnityEngine.Time.deltaTime;
        }
    }

    public void Start()
    {
        this.isRunning = true;
    }

    public void Stop()
    {
        this.isRunning = false;
    }

    public void StopAndReset()
    {
        this.isRunning = false;
        this.Time = 0;
    }

    public void Reset()
    {
        this.Time = 0;
    }

    public void Restart()
    {
        this.Time = 0;
        this.isRunning = true;
    }
}
