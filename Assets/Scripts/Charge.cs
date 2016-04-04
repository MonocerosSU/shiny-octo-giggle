using UnityEngine;
using System.Collections;

public class Charge : MonoBehaviour
{
	public Transform target;
    public float chargeDistance;
    public float thrust;

    private bool isCharged;
    private Rigidbody rigidBody;
    private AudioSource audio;

	void Start()
	{
	    this.audio = this.GetComponent<AudioSource>();
	    this.rigidBody = this.GetComponent<Rigidbody>();

        if (this.target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                this.target = player.transform;
            }
        }
    }

	void Update()
	{
	    if (this.target == null)
	    {
	        return;
	    }

	    bool isInRange = Vector3.Distance(this.transform.position, this.target.position) < this.chargeDistance;

        if (isInRange && !this.isCharged)
        {
            this.isCharged = true;

            this.transform.LookAt(this.target);
            this.rigidBody.AddForce(this.transform.forward * this.thrust);
            this.audio.Play();
        }
	    
	}
}