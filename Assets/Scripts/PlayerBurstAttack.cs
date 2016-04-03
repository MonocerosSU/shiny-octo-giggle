using UnityEngine;

public class PlayerBurstAttack : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectileSpawn;
    public float fireRate;
    public int shotsPerBurst;

    private AudioSource audioSource;
    private float nextFire;

    public void Start()
    {
        this.audioSource = this.GetComponent<AudioSource>();
    }

    public void Fire()
    {
        if (Time.time > this.nextFire)
        {
            this.nextFire = Time.time + this.fireRate;
            for (int i = 0; i < this.shotsPerBurst; i++)
            {
                var newProjectile = GameObject.Instantiate(
                    this.projectile, this.projectileSpawn.position, this.projectileSpawn.rotation);
                newProjectile.name = this.name + "'s " + newProjectile.name;
            }

            this.audioSource.Play();
        }
    }
}
