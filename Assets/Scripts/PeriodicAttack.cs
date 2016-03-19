using UnityEngine;
using System.Collections;

public class PeriodicAttack : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectileSpawn;
    public float fireRate;
    public float delay;

    void Start()
    {
        this.InvokeRepeating("Fire", this.delay, this.fireRate);
    }

    void Fire()
    {
        UnityEngine.Object.Instantiate(
            this.projectile,
            this.projectileSpawn.position,
            this.projectileSpawn.rotation);

        this.GetComponent<AudioSource>().Play();
    }
}
