using UnityEngine;
using System.Collections;

public class DestroyByHitpoints : MonoBehaviour
{
    public GameObject damageEffect;
    public GameObject destructionEffect;
    public float hitPoints;

    public void OnTriggerEnter(Collider other)
    {
        if (this.hitPoints <= 0)
        {
            this.DestroyThis();
        }
    }

    public void TakeDamage(float damage)
    {
        this.hitPoints -= damage;

        if (this.damageEffect != null)
        {
            UnityEngine.Object.Instantiate(
                this.damageEffect, this.transform.position, this.transform.rotation);
        }

        if (this.hitPoints <= 0)
        {
            this.DestroyThis();
        }
    }

    private void DestroyThis()
    {
        UnityEngine.Object.Destroy(this.gameObject);

        if (this.destructionEffect != null)
        {
            UnityEngine.Object.Instantiate(
                this.destructionEffect, this.transform.position, this.transform.rotation);
        }
    }
}
