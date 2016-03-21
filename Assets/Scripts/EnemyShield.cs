using UnityEngine;
using System.Collections;

public class EnemyShield : MonoBehaviour
{
    public ParticleSystem shieldEffect;

    private bool isMoving;
    private Rigidbody rigidBody;

    public void Start()
    {
        this.rigidBody = this.GetComponent<Rigidbody>();
        this.shieldEffect.Stop();
    }

    public void Update()
    {
        this.isMoving = !this.rigidBody.velocity.magnitude.Equals(0);

        if (this.isMoving && this.shieldEffect.isStopped)
        {
            this.shieldEffect.Play();
            Debug.Log("Play");
        }
        else if (!this.isMoving && this.shieldEffect.isPlaying)
        {
            Debug.Log("Stop");
            this.shieldEffect.Stop();
        }
    }
}
