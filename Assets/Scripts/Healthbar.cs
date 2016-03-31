using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Healthbar : MonoBehaviour {

	public float currentHealth;                                   // The current health the player has.
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.          
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	public bool damaged;
	private Component hitpointsClass;

	void Start ()
	{
		hitpointsClass = GetComponent<DestroyByHitpoints>();
	}

	void Update ()
	{
		currentHealth = ((DestroyByHitpoints)hitpointsClass).hitPoints;
		healthSlider.value = currentHealth;
		if (damaged) 
		{
			damageImage.color = flashColour;
		} 
		else 
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}


	public void TakeDamage(float damage)
	{

		((DestroyByHitpoints)hitpointsClass).hitPoints -= damage;
		currentHealth -= damage;

		damaged = true;

	}

	private void DestroyThis ()
	{
		currentHealth = 0;
	}
}

