using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
	public CharacterController charctrl;
	Vector3 movment;
	float turn;
	float headangle;
	public GameObject head;
	public float velocity = 0.5f;
	public float gravity = 0.5f;

		// Use this for initialization
	void Start ()
	{
		charctrl = GetComponent<CharacterController> ();
		Cursor.lockState = CursorLockMode.Locked;

	}
	
	// Update is called once per frame
	void Update ()
	{
		movment = new Vector3 (Input.GetAxis ("Horizontal"), -gravity, Input.GetAxis ("Vertical"));
		turn += Input.GetAxis ("Mouse X");
		headangle -= Input.GetAxis ("Mouse Y");
		headangle = Mathf.Clamp (headangle, -70, 70);
	}

	void FixedUpdate ()
	{
		charctrl.Move (transform.TransformDirection (movment * velocity));
		transform.rotation = Quaternion.Euler (0, turn, 0);
		head.transform.localRotation = Quaternion.Euler (headangle, 0, 0);
	}

}