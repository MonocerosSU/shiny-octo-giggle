using UnityEngine;
using System.Collections;

public class HitpointsDamager : MonoBehaviour
{
    public float damagePoints;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary") || 
            other.gameObject.CompareTag("Shield") ||
            other.gameObject.CompareTag("Projectile"))
        {
            return;
        }

        // In case of projectiles, they should not damage 
        // themselves or the target if they are of the same name, 
        // or they contain the name of the object spawning them.
        if (this.gameObject.name.Contains(other.gameObject.name))
        {
            return;
        }

        DestroyByHitpoints hitpointsClass = other.GetComponent<DestroyByHitpoints>();
        if (hitpointsClass != null)
        {
            hitpointsClass.TakeDamage(this.damagePoints);
        }
    }
}
