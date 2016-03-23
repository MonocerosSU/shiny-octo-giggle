using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{
    private float timer;

    public void FixedUpdate()
    {
        if (this.timer > 0.1)
        {
            GameObject.Destroy(this.gameObject);
        }

        this.timer += Time.deltaTime;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary"))
        {
            this.timer = 0;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
