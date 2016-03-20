using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    public float speed;

    public void Start()
    {
        this.GetComponent<Rigidbody>().velocity = this.transform.forward * this.speed;
    }
}
