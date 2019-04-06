using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{

	public GameObject ButtonExit;
	public GameObject ButtonContinue;

	// Use this for initialization
	void Start ()
	{
		ButtonExit.SetActive (false);
		ButtonContinue.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
			ButtonExit.SetActive (true);
			ButtonContinue.SetActive (true);
		}
	}

	public void quit ()
	{
		Application.Quit ();
	}

	public void continuar ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		ButtonExit.SetActive (false);
		ButtonContinue.SetActive (false);
	}
}
