using UnityEngine;
using System.Collections;

public class NoCursor : MonoBehaviour {

	void Start ()
	{
		Cursor.visible = false;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.LoadLevel ("MenuScene");
			Cursor.visible = true;
		}
	}
}