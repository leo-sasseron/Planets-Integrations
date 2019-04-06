using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{

	public Navigation nav;
	public GameObject[] listaPlanetas;
	public GameObject ButtonVenus;
	public GameObject ButtonMarte;
	public GameObject ButtonExit;
	public GameObject ButtonContinue;
	public GameObject ButtonComands;
	public GameObject TextComands;
	int j;

	void Start () {
		ButtonVenus.SetActive (false);
		ButtonMarte.SetActive (false);
		ButtonExit.SetActive (false);
		ButtonContinue.SetActive (false);
		TextComands.SetActive (false);
		Cursor.lockState = CursorLockMode.None;
	}

	void Update ()
	{
		if (Input.GetKey(KeyCode.Escape)) {
			ButtonExit.SetActive (true);
			ButtonContinue.SetActive (true);
			TextComands.SetActive (false);
			ButtonMarte.SetActive (false);
			ButtonVenus.SetActive (false);
		}

		if (Input.anyKeyDown) {
			
			try {
				int i = int.Parse (Input.inputString);
				j=i;

				if (i > 0 && i <= 9) {
					nav.PlanetToGo = listaPlanetas [i - 1];
					ButtonVenus.SetActive (false);
					ButtonMarte.SetActive (false);
				}
				if (i == 2) {
					ButtonVenus.SetActive (true);
					ButtonMarte.SetActive (false);

				}

				if (i == 4) {
					ButtonMarte.SetActive (true);
					ButtonVenus.SetActive (false);
	
				}
					

			} catch {

			}
		
		}

		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			nav.PlanetToGo = null;
		}
	}

	public void quit() {
		Application.Quit ();
	}

	public void continuar() {
		ButtonExit.SetActive (false);
		ButtonContinue.SetActive (false);
		TextComands.SetActive (false);

		if (j > 0 && j <= 9) {
			nav.PlanetToGo = listaPlanetas [j - 1];
			ButtonVenus.SetActive (false);
			ButtonMarte.SetActive (false);
		}
		if (j == 2) {
			ButtonVenus.SetActive (true);
			ButtonMarte.SetActive (false);

		}

		if (j == 4) {
			ButtonMarte.SetActive (true);
			ButtonVenus.SetActive (false);

		}
	}

	public void comands() {
		ButtonComands.SetActive (true);
		TextComands.SetActive (true);
		ButtonContinue.SetActive (true);
	}
}
