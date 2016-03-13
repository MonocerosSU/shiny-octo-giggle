using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
}

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        this.GetComponent<Rigidbody>().velocity = movement * this.speed;

        this.GetComponent<Rigidbody>().position = new Vector3(
            Mathf.Clamp(this.GetComponent<Rigidbody>().position.x, this.boundary.xMin, this.boundary.xMax),
            Mathf.Clamp(this.GetComponent<Rigidbody>().position.y, this.boundary.yMin, this.boundary.yMax),
            0.0f);

        this.GetComponent<Rigidbody>().rotation = Quaternion.Euler(
            this.GetComponent<Rigidbody>().velocity.y * -this.tilt, 90.0f, 0.0f);
    }
}
