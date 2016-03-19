using UnityEngine;
using System.Collections;

public class RandomizedMover : MonoBehaviour
{
    public float upDownMin;
    public float upDownMax;
    public float speedMin;
    public float speedMax;

    void Start()
    {
        this.GetComponent<Rigidbody>().velocity = this.transform.forward 
            * Random.Range(this.speedMin, this.speedMax);
        this.GetComponent<Rigidbody>().velocity += this.transform.up 
            * Random.Range(this.upDownMin, this.upDownMax);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Boundary")
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }
}
