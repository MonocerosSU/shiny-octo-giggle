﻿using UnityEngine;
using System.Collections;

public class ShieldReflect : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Projectile"))
        {
            return;
        }

        other.name = "Reflected projectile.";
        Rigidbody otherRigidBody = other.GetComponent<Rigidbody>();

        otherRigidBody.velocity = other.transform.forward * -1 * otherRigidBody.velocity.magnitude;
        otherRigidBody.velocity = new Vector3(
            otherRigidBody.velocity.x, otherRigidBody.velocity.y, 0.0f);
    }
}
