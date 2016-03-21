using System;

using UnityEngine;
using System.Collections;

using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    public float maxDistance;
    public float smoothing;
    public Range startWait;
    public Range maneuverTime;
    public Range maneuverWait;
    public Transform sceneBoundary;

    private float rotationSpeed;
    private float targetManeuver;
    private Vector3 currentSpeed;
    private Boundary boundary;
    private Rigidbody rigidBody;

    public void Start()
    {
        this.currentSpeed = this.GetComponent<Rigidbody>().velocity;
        this.StartCoroutine(this.Evade());
        this.rigidBody = this.GetComponent<Rigidbody>();
        this.boundary = new Boundary(this.sceneBoundary, this.GetComponent<Collider>().bounds);
    }

    public void FixedUpdate()
    {
        this.rotationSpeed += this.targetManeuver * 2;
        this.transform.Rotate(Vector3.forward * (this.rotationSpeed * Time.deltaTime));

        float newManeuver = Mathf.MoveTowards(this.rigidBody.velocity.y, this.targetManeuver, this.smoothing * Time.deltaTime);
        this.rigidBody.velocity = new Vector3(this.currentSpeed.x, newManeuver, this.currentSpeed.z);
        this.rigidBody.position =
            new Vector3(
                Mathf.Clamp(this.rigidBody.position.x, this.boundary.xMin, this.boundary.xMax),
                Mathf.Clamp(this.rigidBody.position.y, this.boundary.yMin, this.boundary.yMax),
                0.0f);
    }

    private IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(this.startWait.min, this.startWait.max));
        while (true)
        {
            this.targetManeuver = Random.Range(1, this.maxDistance) * -Mathf.Sign(this.transform.position.y);
            yield return new WaitForSeconds(Random.Range(this.maneuverTime.min, this.maneuverTime.max));
            this.targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(this.maneuverWait.min, this.maneuverWait.max));
        }
    }
}
