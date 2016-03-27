using UnityEngine;
using System.Collections;

public class Charge : MonoBehaviour {
	public Transform target;
	public float thrust;
	public Rigidbody rigidBody;

	void Start() {
		rigidBody = GetComponent<Rigidbody>();
		transform.LookAt(target);
		}

	void Update() {
		rigidBody.AddForce(transform.forward * thrust);
	}
}