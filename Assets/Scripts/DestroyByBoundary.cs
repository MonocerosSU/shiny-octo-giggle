using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Boundary")
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }
}
