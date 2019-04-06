using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour {

	public GameObject PlanetToGo;
	private Vector3 startPos;
	private Vector3 maxDistance;


	void Start() {

		startPos = transform.position;
	}

	void Update() {

		if (PlanetToGo) {

			maxDistance = new Vector3 (-4, 0, PlanetToGo.transform.localScale.z);

			Vector3 dir = PlanetToGo.transform.position - maxDistance - transform.position;
			transform.position += dir * Time.deltaTime * 2;

			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (dir), Time.deltaTime * 100);

		} else {

			Vector3 dir = startPos - transform.position;
			transform.position += dir * Time.deltaTime;

			Vector3 sunDir = Vector3.zero - transform.position;

			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (sunDir), Time.deltaTime * 100);

		}
	}
}

