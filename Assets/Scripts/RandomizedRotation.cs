using UnityEngine;
using System.Collections;

public class RandomizedRotation : MonoBehaviour
{
    public float rotation;

    public void Start()
    {
        this.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * this.rotation;
    }
}
