using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieControl : MonoBehaviour
{

	public Animator anim;
	Rigidbody[] rdbs;
	public CharacterController character;
	public GameObject brain;
	public GameObject paths;
	public Transform[] pathsindividual;
	int pathsround = 1;
	public enum Zstate{Patrol,Bersek,Attack,Dead};
	public Zstate zstate;
	public NavMeshAgent agent;

	public AudioClip[] audios;
	AudioSource aud;


	// Use this for initialization
	void Start ()
	{
		pathsindividual = paths.GetComponentsInChildren < Transform > ();
		rdbs = GetComponentsInChildren < Rigidbody > ();
		foreach (Rigidbody rdb in rdbs) {
			rdb.isKinematic = true;
		}
		anim.enabled = false;
		Invoke ("Startanim", Random.value);
		pathsround = Random.Range (1, pathsindividual.Length);
		agent.SetDestination (pathsindividual [pathsround].position);

		aud = GetComponent<AudioSource> ();

	}



	/// <summary>
	/// Inicia a animação do zumbi
	/// </summary>
	void Startanim ()
	{
		anim.enabled = true;
	}



	// Update is called once per frame
	void Update ()
	{

	}



	void FixedUpdate ()
	{
		switch (zstate) {
		case(Zstate.Attack):
			Attack ();
			break;

		case(Zstate.Patrol):
			Patrol ();
			break;

		case(Zstate.Bersek):
			Bersek ();
			break;

		case(Zstate.Dead):
			Dead ();
			break;
		}
	}


	/*
	/// <summary>
	/// Movimentação do zumbi
	/// </summary>
	void WalkZombie ()
	{
		if (character.enabled) {
			if (brain) {
				Bersek ();
			} else {
				Patrol ();
			}
			character.SimpleMove (transform.forward * 0.7f);
		}

	}
	*/


	void Dead ()
	{

	}



	/// <summary>
	/// Estado de ataque do zumbi
	/// </summary>
	void Attack ()
	{
		
		//Vector3 dir = brain.transform.position - transform.position;
		//transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (dir, Vector3.up), Time.fixedDeltaTime);

		//character.SimpleMove (transform.forward * 0.7f);
		Vector3 dir = brain.transform.position - transform.position;
		if (dir.magnitude > 2) {
			zstate = Zstate.Bersek;
		}

		anim.SetBool ("attack", true);
	}



	/// <summary>
	/// Vai em direção ao jogador
	/// </summary>
	void Bersek ()
	{
		
		//Vector3 dir = brain.transform.position - transform.position;
		//transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (dir, Vector3.up), Time.fixedDeltaTime);
		//character.SimpleMove (transform.forward * 0.7f);


		agent.SetDestination (brain.transform.position);
		agent.speed = 3;
		Vector3 dir = brain.transform.position - transform.position;
		if (dir.magnitude < 2) {
			zstate = Zstate.Attack;
		}
		anim.SetBool ("attack", false);
	}



	/// <summary>
	/// Patrulha a área, vai em direção aos pontos definidos
	/// </summary>
	void Patrol ()
	{
		character.SimpleMove (transform.forward * 0.7f);
		Vector3 dir = pathsindividual [pathsround].transform.position - transform.position;
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (dir, Vector3.up), Time.fixedDeltaTime);


		if (dir.magnitude < 2) {
			//pathsround++;
			pathsround = Random.Range (1, pathsindividual.Length);
			agent.SetDestination (pathsindividual [pathsround].transform.position);

			if (pathsround >= pathsindividual.Length) {
				pathsround = 1;

			}

		}
	}



	/// <summary>
	/// Estado de morte do zumbi
	/// </summary>
	public void KillZombie ()
	{
		character.enabled = false;
		foreach (Rigidbody rdb in rdbs) {
			rdb.isKinematic = false;
		}
		anim.enabled = false;
		agent.enabled = false;
		zstate = Zstate.Dead;

		aud.PlayOneShot (audios [1]);

	}



	/// <summary>
	/// Atrai o zumbi ao player, ao entrar na área definida
	/// </summary>
	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Player")) {
			brain = col.gameObject;
			zstate = Zstate.Bersek;

			aud.PlayOneShot(audios[0]);
		}
	}
		
}