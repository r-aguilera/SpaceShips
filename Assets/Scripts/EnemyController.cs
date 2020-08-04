using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	
	private Rigidbody2D body;
	private Animator anim;
	private SpriteRenderer sprite;
	public GameObject EnemyMissile;

	public float acel = 75f;
	public float maxSpeed = 5f;
	public float shotDelay = 0.75f;
	public float spawnDelay = 2f;
	public float invincibleDelay = 2f;
	public float invincibilityTranspStep = -10f / 255f;
	public float deathAnimLenght = 0.517f;
	public int lives = 1;
	public bool canMove = true;
	public bool canShoot = true; 
	public bool isDead = false;
	public bool isInvincible = false;

	private float rangeLeft = -9.5f;
	private float rangeRight = 7.5f;
	public bool isGoingLeft = true; 
	public bool isInDanger = false;
	public bool escapeLeft = true;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer> ();
		float xPos = Random.Range(-9.5f, 7.5f);
		transform.position = new Vector3 (xPos, 9f, -5f);
		changeAnimation ();
		setRangeLeft ();
		setRangeRight ();
		makeInvincible ();
	}

	void OnTriggerEnter2D(Collider2D collided){
		if (collided.CompareTag ("Missile") && !isInvincible)
			lives--;
	}

	void FixedUpdate(){
		body.velocity *= 0.9f;

		enemyAI ();

		if (lives < 1 && !isDead) {
			canMove = false;
			canShoot = false;
			isDead = true;
			Invoke ("enemySpawn", deathAnimLenght + spawnDelay);
		}

		changeAnimation ();
	}

	void enemyAI(){
		float h;

		if (isInDanger) {
			isGoingLeft = escapeLeft;
			isInDanger = false;
		}

		if (isGoingLeft && transform.position.x < rangeLeft) {
			isGoingLeft = false;
			setRangeLeft ();
			//rangeEvaluation ();
		} else if (!isGoingLeft && transform.position.x > rangeRight) {
			isGoingLeft = true;
			setRangeRight ();
		}

		if (isGoingLeft)
			h = -1f;
		else
			h = 1f;
		
		if(canMove)
		body.AddForce(Vector2.right * h * acel);

		float clampedSpeed = Mathf.Clamp (body.velocity.x, -maxSpeed, maxSpeed);
		body.velocity = new Vector2 (clampedSpeed, 0f);

		if (canShoot) {
			Instantiate (EnemyMissile);
			canShoot = false;
			Invoke ("rechargeShot", shotDelay);
		}
	}

	void setRangeLeft(){
		float newRangeLeft;
		do {
			newRangeLeft = Random.Range(-9.5f, rangeRight);
		} while(newRangeLeft == rangeRight);
		rangeLeft = newRangeLeft;
	}

	void setRangeRight(){
		float newRangeRight;
		do {
			newRangeRight = Random.Range(rangeLeft, 7.5f);
		} while(rangeLeft == newRangeRight);
		rangeRight = newRangeRight;
	}

	void rechargeShot(){
		if(!isDead)
			canShoot = true;
	}

	void enemySpawn(){
		lives = 1;
		float xPos = Random.Range(-9.5f, 7.5f);
		transform.position = new Vector3 (xPos, 9f, -5f);
		body.velocity = Vector2.zero;
		canMove = true;
		canShoot = true;
		isDead = false;
		makeInvincible();
	}

	void makeInvincible(){
		isInvincible = true;
		Invoke ("makeVincible", invincibleDelay);
	}

	void makeVincible(){
		isInvincible = false;
		Color color = sprite.color;
		sprite.color = new Color (color.r, color.g, color.b, 1);
	}

	void changeAnimation(){
		Debug.Log (invincibilityTranspStep);

		anim.SetFloat ("h", body.velocity.x/maxSpeed);
		anim.SetInteger ("lives", lives);
		if (isInvincible) {
			if (sprite.color.a + invincibilityTranspStep < 0 || sprite.color.a + invincibilityTranspStep > 1)
				invincibilityTranspStep = -invincibilityTranspStep;
			Color color = sprite.color;
			sprite.color = new Color (color.r, color.g, color.b, color.a += invincibilityTranspStep);
		}
	}
}
