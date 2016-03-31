using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class UIHitpointsBar : MonoBehaviour
{
    public GameObject player;

    public Slider healthSlider;

    private DestroyByHitpoints playerHpScript;

    public void Start()
    {
        this.playerHpScript = this.player.GetComponent<DestroyByHitpoints>();
    }

    void FixedUpdate()
    {
        if (this.playerHpScript != null)
        {
            this.healthSlider.value = this.playerHpScript.hitPoints;
        }
        else
        {
            this.healthSlider.value = 0;
        }
    }
}
