using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        // In case of projectiles, they should not destroy 
        // themselves if they are of the same prefab name, 
        // or they contain the prefab name of the object spawning them.
        if (this.gameObject.name.Contains(other.gameObject.name))
        {
            return;
        }

        if (other.gameObject.tag != "Boundary")
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }
}
