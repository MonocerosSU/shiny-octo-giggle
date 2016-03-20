using UnityEngine;
using System.Collections;

public class DestroyByLifetime : MonoBehaviour {

    public float lifetime;

    public void Start()
    {
        GameObject.Destroy(this.gameObject, this.lifetime);
    }
}
