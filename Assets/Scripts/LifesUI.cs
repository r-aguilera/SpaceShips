using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesUI : MonoBehaviour {

	private PlayerController player;
	private Animator[] life = new Animator[3];

	// Use this for initialization
	void Start () {
		life [0] = transform.Find ("Life1").GetComponent<Animator> ();
		life [1] = transform.Find ("Life2").GetComponent<Animator> ();
		life [2] = transform.Find ("Life3").GetComponent<Animator> ();
		player = FindObjectOfType<PlayerController> ();
		changeAnimation ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		changeAnimation ();
	}

	void changeAnimation () {
		int livesCount = player.lives; Debug.Log (livesCount);
		life [0].SetBool("active", (livesCount > 0));
		life [1].SetBool("active", (livesCount > 1));
		life [2].SetBool("active", (livesCount > 2));
	}
}
