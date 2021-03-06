﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileController : MonoBehaviour {

	private EnemyController enemy;
	public float speed = 0.25f;

	void OnTriggerEnter2D(Collider2D collided){
		if (collided.CompareTag ("Player")) {
			bool isDead = collided.GetComponent<PlayerController> ().isDead;
			if(!isDead)
				Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.position += Vector3.down * speed;
		if (transform.position.y <= -10.5)
			Destroy (gameObject);
	}
}
