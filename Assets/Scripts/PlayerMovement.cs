using System;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float tilt;
    public TiltAxis tiltAxis;
    public Transform sceneBoundary;

    private Boundary boundary;
    private Rigidbody rigidBody;

    public void Start()
    {
        this.rigidBody = this.GetComponent<Rigidbody>();
        this.boundary = new Boundary(this.sceneBoundary, this.GetComponent<Collider>().bounds);
    }

    public void FixedUpdate()
    {
        this.Move();
        this.RestrictToScreen();
        this.Tilt();
    }

    private void Tilt()
    {
        switch (this.tiltAxis)
        {
            case TiltAxis.X:
                this.rigidBody.rotation = Quaternion.Euler(this.rigidBody.velocity.y * -this.tilt, 90.0f, 0.0f);
                break;
            case TiltAxis.Y:
                this.rigidBody.rotation = Quaternion.Euler(0.0f, 90.0f + this.rigidBody.velocity.y * -this.tilt, 0.0f);
                break;
            case TiltAxis.Z:
                this.rigidBody.rotation = Quaternion.Euler(0.0f, 90.0f, this.rigidBody.velocity.y * -this.tilt);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    private void RestrictToScreen()
    {
        this.rigidBody.position = new Vector3(Mathf.Clamp(this.rigidBody.position.x, this.boundary.xMin, this.boundary.xMax), Mathf.Clamp(this.rigidBody.position.y, this.boundary.yMin, this.boundary.yMax), 0.0f);
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        this.rigidBody.velocity = movement * this.speed;
    }
}
