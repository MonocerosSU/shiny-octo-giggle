using UnityEngine;

public class PlayerBurstAttack : MonoBehaviour
{

    public GameObject projectile;
    public Transform projectileSpawn;
    public float fireRate;
    public int shotsPerBurst;

    private float nextFire;

    public void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > this.nextFire)
        {
            this.nextFire = Time.time + this.fireRate;
            for (int i = 0; i < this.shotsPerBurst; i++)
            {
                UnityEngine.Object.Instantiate(
                    this.projectile, this.projectileSpawn.position, this.projectileSpawn.rotation);
            }

            this.GetComponent<AudioSource>().Play();
        }
    }
}
