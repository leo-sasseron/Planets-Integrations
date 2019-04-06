using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {
	
	public AudioClip somClip;
	public AudioSource som;

	// Use this for initialization
	void Start () {
		som.clip = somClip;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time % 5f > 4.9f) {
			som.Play ();
		}
	}
}
