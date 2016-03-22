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

    // Movement
    private float rotationSpeed;
    private float targetManeuver;
    private Vector3 currentSpeed;
    private Boundary boundary;

    private ShieldControll shieldControll;
    private Rigidbody rigidBody;

    public void Start()
    {
        this.shieldControll = this.GetComponentInChildren<ShieldControll>();
        this.rigidBody = this.GetComponent<Rigidbody>();

        this.currentSpeed = this.GetComponent<Rigidbody>().velocity;
        this.StartCoroutine(this.EvadeCoroutine());
        this.boundary = new Boundary(this.sceneBoundary, this.GetComponent<Collider>().bounds);
    }

    public void FixedUpdate()
    {
        this.Rotate();
        this.Move();
    }
    
    private IEnumerator EvadeCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(this.startWait.min, this.startWait.max));
        while (true)
        {
            this.targetManeuver = Random.Range(1, this.maxDistance) * -Mathf.Sign(this.transform.position.y);
            this.shieldControll.StartShield();
            yield return new WaitForSeconds(Random.Range(this.maneuverTime.min, this.maneuverTime.max));
            this.shieldControll.StopShield();
            this.targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(this.maneuverWait.min, this.maneuverWait.max));
        }
    }

    private void Rotate()
    {
        this.rotationSpeed += this.targetManeuver * 2;
        this.transform.Rotate(Vector3.forward * (this.rotationSpeed * Time.deltaTime));
    }

    private void Move()
    {
        float newManeuver = Mathf.MoveTowards(this.rigidBody.velocity.y, this.targetManeuver, this.smoothing * Time.deltaTime);
        this.rigidBody.velocity = new Vector3(this.currentSpeed.x, newManeuver, this.currentSpeed.z);
        this.rigidBody.position =
            new Vector3(
                Mathf.Clamp(this.rigidBody.position.x, this.boundary.xMin, this.boundary.xMax),
                Mathf.Clamp(this.rigidBody.position.y, this.boundary.yMin, this.boundary.yMax),
                0.0f);
    }
}
