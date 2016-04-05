using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class NoCursor : MonoBehaviour {

	void Start ()
	{
		Cursor.visible = false;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			SceneManager.LoadScene("MenuScene");
			Cursor.visible = true;
		}
	}
}