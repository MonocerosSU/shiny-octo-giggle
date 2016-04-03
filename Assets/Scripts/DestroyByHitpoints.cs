using UnityEngine;
using System.Collections;

public class DestroyByHitpoints : MonoBehaviour
{
    public GameObject damageEffect;
    public GameObject destructionEffect;
    public float hitPoints;
	public int scoreValue;
	public ScoreCount scoreCount;

    private bool isMarkedForDestruction = false;
	public void TakeDamage(float damage)
    {
        this.hitPoints -= damage;

        if (this.damageEffect != null)
        {
            GameObject.Instantiate(
                this.damageEffect, this.transform.position, this.transform.rotation);
        }

        if (this.hitPoints <= 0 && !this.isMarkedForDestruction)
        {
            this.DestroyThis();
        }
    }

    private void DestroyThis()
    {
        if (this.destructionEffect != null)
        {
            GameObject.Instantiate(
                this.destructionEffect, this.transform.position, this.transform.rotation);
        }

        this.scoreCount.AddScore(this.scoreValue);

        this.isMarkedForDestruction = true;
        GameObject.Destroy(this.gameObject);
    }
}
