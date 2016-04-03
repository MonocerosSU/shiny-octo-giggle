using UnityEngine;
using System.Collections;

public class Stopwatch
{
    public bool IsRunning { get; private set; }

    public float Time { get; private set; }

    public void Update()
    {
        if (this.IsRunning)
        {
            this.Time += UnityEngine.Time.deltaTime;
        }
    }

    public void Start()
    {
        this.IsRunning = true;
    }

    public void Pause()
    {
        this.IsRunning = false;
    }

    public void Reset()
    {
        this.Time = 0;
    }

    public void Stop()
    {
        this.Pause();
        this.Reset();
    }

    public void Restart()
    {
        this.Reset();
        this.Start();
    }
}
