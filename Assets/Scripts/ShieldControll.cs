using UnityEngine;
using System.Collections;

public class ShieldControll : MonoBehaviour
{
    public GameObject parent;

    private bool isShielded;
    private GameObject shield;
    private Collider parentCollider;
    private Collider shieldCollider;
    private ParticleSystem shieldEffect;

    // Use this for initialization
    public void Start()
    {
        this.parentCollider = this.parent.GetComponent<Collider>();
        this.shield = this.gameObject;
        this.shieldEffect = this.shield.GetComponentInChildren<ParticleSystem>();
        this.shieldCollider = this.shield.GetComponent<Collider>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (this.isShielded)
        {
            this.shieldEffect.Emit(10);
            this.shieldCollider.enabled = true;
            this.parentCollider.enabled = false;
        }
        else
        {
            this.shieldCollider.enabled = false;
            this.parentCollider.enabled = true;
        }
    }

    public void StartShield()
    {
        this.isShielded = true;
    }

    public void StopShield()
    {
        this.isShielded = false;
    }

    public void ToggleShield()
    {
        this.isShielded = !this.isShielded;
    }
}
