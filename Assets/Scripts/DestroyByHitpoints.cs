using UnityEngine;
using System.Collections;

public class DestroyByHitpoints : MonoBehaviour
{
    public GameObject explosionEffect;
    public float hitPoints;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Projectile")
        {
            return;
        }

        Component damagerClass = other.GetComponent<Damager>();
        if (damagerClass != null)
        {
            this.hitPoints -= ((Damager)damagerClass).damagePoints;
            
            // TODO: Add damage effect.

            if (this.hitPoints <= 0)
            {
                UnityEngine.Object.Destroy(this.gameObject);

                if (this.explosionEffect != null)
                {
                    UnityEngine.Object.Instantiate(
                        this.explosionEffect, this.transform.position, this.transform.rotation);
                }
            }
        }
        else
        {
            Debug.Log("Projectile " + other.gameObject.name + " does not have a Damager script attached to it!");
        }
    }
}
