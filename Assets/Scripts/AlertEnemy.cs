using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertEnemy : MonoBehaviour {

	private EnemyController enemy;
	private PlayerMissileController missile;

	void OnTriggerStay2D(Collider2D collided){
		if (collided.CompareTag ("Enemy")) {
			enemy = collided.GetComponent<EnemyController> ();
			missile = GetComponentInParent<PlayerMissileController> ();
			enemy.isInDanger = true;
			enemy.escapeLeft = (missile.transform.position.x > enemy.transform.position.x + 1);
		}
	}
}
