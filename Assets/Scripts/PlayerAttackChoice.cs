using UnityEngine;
using System.Collections;

public class PlayerAttackChoice : MonoBehaviour
{
    public float chargeTime = 0.20f;

    [Header("Attacks:")]
    public PlayerBurstAttack quickAttack;
    public LaserAttack chargeAttack;

    private Stopwatch stopwatch;

    public void Start()
    {
        this.stopwatch = new Stopwatch();
    }

    void Update()
    {
        this.stopwatch.Update();

        if (Input.GetButtonDown("Fire1"))
        {
            this.stopwatch.Start();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            this.stopwatch.Stop();
            if (this.stopwatch.Time <= this.chargeTime)
            {
                this.quickAttack.Fire();
            }
            this.stopwatch.StopAndReset();
        }
        if (Input.GetButton("Fire1") && this.stopwatch.Time >= this.chargeTime)
        {
            this.chargeAttack.isFiring = true;
        }
        else
        {
            this.chargeAttack.isFiring = false;
        }
    }
}
