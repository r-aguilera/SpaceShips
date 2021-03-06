﻿using System.Collections;
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
		int livesCount = player.lives;
		for(int i = 0; i < 3; ++i) {
			life [i].SetBool("active", (livesCount > i));	
		}
	}
}
