using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public GameObject destructionEffect;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary") ||
            other.gameObject.CompareTag("Shield") ||
             other.gameObject.CompareTag("Projectile"))
        {
            return;
        }

        // In case of projectiles, they should not destroy 
        // themselves if they are of the same name, 
        // or they contain the name of the object spawning them.
        if (this.gameObject.name.Contains(other.gameObject.name))
        {
            return;
        }

        if (this.destructionEffect != null)
        {
            GameObject.Instantiate(
                this.destructionEffect, this.transform.position, this.transform.rotation);
        }

        GameObject.Destroy(this.gameObject);
    }
}
