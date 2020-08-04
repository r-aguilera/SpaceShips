using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileController : MonoBehaviour {

	private PlayerController player;
	public float speed = 0.25f;

	void OnTriggerEnter2D(Collider2D collided){
		if (collided.CompareTag ("Enemy")) {
			bool isDead = collided.GetComponent<EnemyController> ().isDead;
			if(!isDead)
				Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.position += Vector3.up * speed;
		if (transform.position.y >= 10.5)
			Destroy (gameObject);
	}
}
