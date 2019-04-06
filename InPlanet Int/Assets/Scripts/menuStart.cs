using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuStart : MonoBehaviour {


	public void ChangeScene(string sceneName)	{
		Application.LoadLevel(sceneName);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
