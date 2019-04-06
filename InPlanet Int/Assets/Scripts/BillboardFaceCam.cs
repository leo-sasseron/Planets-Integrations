using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardFaceCam : MonoBehaviour {

	private GameObject MainCamera;
	private TextMesh textMesh;
	private float alpha;

	void Start() {

		MainCamera = Camera.main.gameObject;
		textMesh = GetComponent<TextMesh> ();
	}

	void Update() {

		Vector3 dif = transform.position - MainCamera.transform.position;
		transform.forward = dif;

		if (dif.magnitude < 50) StartCoroutine(FadeToAlpha());
		else textMesh.color = Vector4.one;
	}

	IEnumerator FadeToAlpha() {

		int i = 0;

		while (i < 10) {

			textMesh.color = new Vector4 (textMesh.color.r, textMesh.color.g, textMesh.color.b, textMesh.color.a - 0.1f);
			i++;
			yield return new WaitForSeconds (1f);
		}
	}
}
