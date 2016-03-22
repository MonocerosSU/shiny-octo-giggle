using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float tilt;
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
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        this.rigidBody.velocity = movement * this.speed;

        this.rigidBody.position = new Vector3(
            Mathf.Clamp(this.rigidBody.position.x, this.boundary.xMin, this.boundary.xMax),
            Mathf.Clamp(this.rigidBody.position.y, this.boundary.yMin, this.boundary.yMax),
            0.0f);

        this.rigidBody.rotation = Quaternion.Euler(
            this.rigidBody.velocity.y * -this.tilt, 90.0f, 0.0f);
    }
}
