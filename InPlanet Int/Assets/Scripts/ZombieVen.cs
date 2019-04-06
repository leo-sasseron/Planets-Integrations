using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieVen : MonoBehaviour {
	public enum Zstate1 {patrol,berserk,attack,dead};
	public Zstate1 state1;
	public Animator anim1;
	Rigidbody[] rdbs;
	public CharacterController charctrl1;
	public GameObject brain;
	public GameObject ways;
	Transform[] waypoints;
	int indexround=1;


	// Use this for initialization
	void Start () {
		anim1 = GetComponent<Animator> ();
		rdbs = GetComponentsInChildren<Rigidbody> ();
		foreach (Rigidbody rdb in rdbs) {
			rdb.isKinematic = true;
		}
		charctrl1 = GetComponent<CharacterController> ();
		waypoints = ways.GetComponentsInChildren<Transform>();
	}
	void FixedUpdate () {
		switch (state1) {
		case(Zstate1.patrol):
			Patrol ();
			break;
		case(Zstate1.berserk):
			Berserk ();
			break;
		case(Zstate1.attack):
			Attack ();
			break;
		case(Zstate1.dead):
			break;
		}
	}
	void Attack(){
		Vector3 dir = brain.transform.position - transform.position;
		transform.rotation = Quaternion.LookRotation (dir, Vector3.up);
		if (dir.magnitude > 2) {
			state1 = Zstate1.berserk;
		}
		anim1.SetBool ("attack", true);
	}
	void Berserk(){
		Vector3 dir = brain.transform.position - transform.position;
		transform.rotation = Quaternion.LookRotation (dir, Vector3.up);
		charctrl1.SimpleMove (transform.forward);
		if (dir.magnitude < 2) {
			state1 = Zstate1.attack;
		}
		anim1.SetBool ("attack", false);
	}
	void Patrol(){
		Vector3 dir = waypoints[indexround].position - transform.position;
		transform.rotation = Quaternion.LookRotation (dir, Vector3.up);
		if (dir.magnitude < 1) {
			indexround++;
			if (indexround >= waypoints.Length) {
				indexround = 1;
			}
		}
		charctrl1.SimpleMove (transform.forward);
	}
	public void KillZombie(){
		foreach (Rigidbody rdb in rdbs) {
			rdb.isKinematic = false;
		}
		anim1.enabled = false;
		charctrl1.enabled = false;
		state1 = Zstate1.dead;
	}
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.CompareTag("Player")){
			brain=collider.gameObject;
			state1 = Zstate1.berserk;
		}
	}


}

