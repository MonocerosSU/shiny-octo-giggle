using UnityEngine;
using System.Collections;

public class ReplicateByTime : MonoBehaviour
{
    public float replicationTime;
    public float replicaRotation;

    public void Start()
    {
        this.Invoke("Replicate", this.replicationTime);
    }

    public void Replicate()
    {
        Vector3 euler = this.gameObject.transform.rotation.eulerAngles;
        euler.z = 90;

        var firstRotation = 
            Quaternion.Euler(euler)
            * Quaternion.AngleAxis(this.replicaRotation, Vector3.up);
        
        var secondRotation = 
            Quaternion.Euler(euler)
            * Quaternion.AngleAxis(-this.replicaRotation, Vector3.up);

        GameObject.Instantiate(
            this.gameObject,
            this.gameObject.transform.position,
            firstRotation);

        GameObject.Instantiate(
            this.gameObject,
            this.gameObject.transform.position,
            secondRotation);

        GameObject.Destroy(this.gameObject);
    }
}
