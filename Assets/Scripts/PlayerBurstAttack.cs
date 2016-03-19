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

    public void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > this.nextFire)
        {
            this.nextFire = Time.time + this.fireRate;
            for (int i = 0; i < this.shotsPerBurst; i++)
            {
                UnityEngine.Object newProjectile = UnityEngine.Object.Instantiate(
                    this.projectile, this.projectileSpawn.position, this.projectileSpawn.rotation);
                newProjectile.name = this.name + "'s " + newProjectile.name;
            }

            this.audioSource.Play();
        }
    }
}
