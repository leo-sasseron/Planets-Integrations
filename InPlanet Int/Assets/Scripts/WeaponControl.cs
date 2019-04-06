using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
	public GameObject weaponmesh;
	Vector3 initialposition;
	bool fire;
	public AudioSource audiosource;
	public ParticleSystem muzzzlefire;
	public ParticleSystem debris;
	public GameObject holePrefab;
	public GameObject[] holes;
	int indexhole, roundhole;
	float delayfire;




	// Use this for initialization
	void Start ()
	{
		initialposition = weaponmesh.transform.localPosition;


	}



	// Update is called once per frame
	void Update ()
	{
		Physical ();
		FullAuto ();
		ParticleFire ();
		Sound ();
		Recoil ();
	}



	/// <summary>
	/// Mata o zumbi ao colidir com ele
	/// </summary>
	void killZombie (RaycastHit hit)
	{
		if (hit.collider.CompareTag ("Zombie")) {
			hit.collider.gameObject.SendMessageUpwards ("KillZombie");
		}
	}



	/// <summary>
	/// Realiza o impacto por forca fisica
	/// </summary>
	void Physical ()
	{
		RaycastHit hit;

		if (fire && Physics.Raycast (transform.position, transform.forward, out hit, 1000)) {

			debris.gameObject.transform.position = hit.point;
			debris.Play ();
			HoleRecicle (hit);
			killZombie (hit);

			//hit.collider.transform.position += Vector3.up;
			Rigidbody rdb = hit.collider.gameObject.GetComponent<Rigidbody> ();
			if (rdb != null) {
				rdb.AddForceAtPosition (transform.forward * 1000, hit.point);
			} 
		}
	}



	/// <summary>
	/// Modo automatico da arma
	/// </summary>
	void FullAuto ()
	{
		if (delayfire < 0 && Input.GetButton ("Fire1")) {
			fire = true;
			delayfire = 0.05f;
		} else {
			fire = false;
			delayfire -= Time.deltaTime;
		}
	}



	/// <summary>
	/// Recria os buracos feitos pela bala
	/// </summary>
	/// <param name="hit">Hit.</param>
	void HoleRecicle (RaycastHit hit)
	{
		if (indexhole < holes.Length) {
			GameObject hole = Instantiate (holePrefab, hit.point + hit.normal * 0.01f, Quaternion.LookRotation (-hit.normal));
			hole.transform.parent = hit.collider.transform;

			holes [indexhole] = hole;
			indexhole++;

		} else {
			holes [roundhole].transform.parent = null;
			holes [roundhole].transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
			holes [roundhole].transform.position = hit.point + hit.normal * 0.01f;
			holes [roundhole].transform.rotation = Quaternion.LookRotation (-hit.normal);
			holes [roundhole].transform.parent = hit.collider.transform;
			roundhole++;
			if (roundhole >= holes.Length) {
				roundhole = 0;
			}
		}
	}



	/// <summary>
	/// Cria o fogo do tiro
	/// </summary>
	void ParticleFire ()
	{
		if (fire) {
			muzzzlefire.Emit (1);
		}
	}



	/// <summary>
	/// Ativa o som do tiro
	/// </summary>
	void Sound ()
	{
		if (fire) {
			audiosource.pitch = 1f + Random.value * 0.2f;
			audiosource.Play ();
		}
	}



	/// <summary>
	/// Realiza o coice da arma
	/// </summary>
	void Recoil ()
	{
		weaponmesh.transform.localPosition = Vector3.Lerp (weaponmesh.transform.localPosition, initialposition, Time.deltaTime * 10);
		if (fire) {
			weaponmesh.transform.localPosition = initialposition + Vector3.back * 0.1f;
		}
	}
}