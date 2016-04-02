using UnityEngine;
using System.Collections;

public class MoveWithParent : MonoBehaviour
{
    private Transform parentTransform;

	void Start ()
	{
	    this.parentTransform = this.transform.parent.transform;
	}
	
	void Update ()
	{
	    this.transform.position = this.parentTransform.position;
	}
}
