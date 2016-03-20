using UnityEngine;
using System.Collections;

public class PeriodicAttack : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectileSpawn;
    public float fireRate;
    public float delay;

    private AudioSource audioSource;

    public void Start()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        this.InvokeRepeating("Fire", this.delay, this.fireRate);
    }

    public void Fire()
    {
        UnityEngine.Object newProjectile = UnityEngine.Object.Instantiate(
            this.projectile, this.projectileSpawn.position, this.projectileSpawn.rotation);
        newProjectile.name = this.name + "'s " + newProjectile.name;

        this.audioSource.Play();
    }
}
