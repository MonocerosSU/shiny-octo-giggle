using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DestroyPlayerByHitpoints : MonoBehaviour
{
	public GameObject damageEffect;
	public GameObject destructionEffect;
	public float hitPoints;
	public float currentHealth;                                   // The current health the player has.
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.          
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
	bool damaged;
	//public void OnTriggerEnter(Collider other)
	//{
	//    if (this.hitPoints <= 0)
	//    {
	//        this.DestroyThis();
	//    }
	//}

	void Start ()
	{
		currentHealth = hitPoints;
	}


	public void TakeDamage(float damage)
	{
		this.hitPoints -= damage;
		currentHealth -= damage;


		healthSlider.value = currentHealth;



		if (this.damageEffect != null)
		{
			GameObject.Instantiate(
				this.damageEffect, this.transform.position, this.transform.rotation);
		}

		if (this.hitPoints <= 0)
		{
			this.DestroyThis();
		}
	}

	private void DestroyThis()
	{
		GameObject.Destroy(this.gameObject);

		if (this.destructionEffect != null)
		{
			GameObject.Instantiate(
				this.destructionEffect, this.transform.position, this.transform.rotation);
		}
	}
}
