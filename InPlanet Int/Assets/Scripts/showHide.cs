using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showHide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Z)) {
			gameObject.GetComponent<Renderer>().enabled = true;
		}

		if (Input.GetKeyDown(KeyCode.X)) {
			gameObject.GetComponent<Renderer>().enabled = false;
		}
	}
}
