using UnityEngine;
using System.Collections;
using System.Security.Policy;

public class DestroyByHitpoints : MonoBehaviour
{
    public GameObject damageEffect;
    public GameObject destructionEffect;
    public float hitPoints;
	public int scoreValue;
	public ScoreCount scoreCount;

    private bool isMarkedForDestruction = false;

    public void Start()
    {
        GameObject scoreText = GameObject.FindWithTag("Score");
        if (scoreText != null)
        {
            this.scoreCount = scoreText.GetComponent<ScoreCount>();
        }
    }

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

        if (this.scoreCount != null)
        {
            this.scoreCount.AddScore(this.scoreValue);
        }

        this.isMarkedForDestruction = true;
        GameObject.Destroy(this.gameObject);
    }
}
