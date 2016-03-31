using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class UIFlashOnDamage : MonoBehaviour
{
    public Slider healthSlider;

    public Image damageImage;
    
    public float flashSpeed = 5f;

    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    public bool damaged;

    private float previousHealth;

    public void Start()
    {
        this.previousHealth = this.healthSlider.value;
    }

    public void Update()
    {
        if (this.previousHealth != this.healthSlider.value)
        {
            this.damaged = true;
            this.previousHealth = this.healthSlider.value;
        }

        if (this.damaged)
        {
            this.damageImage.color = this.flashColour;
        }
        else
        {
            this.damageImage.color = Color.Lerp(this.damageImage.color, Color.clear, this.flashSpeed * Time.deltaTime);
        }
        this.damaged = false;
    }
}
