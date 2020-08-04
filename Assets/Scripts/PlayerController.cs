using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D body;
	private Animator anim;
	private SpriteRenderer sprite;
	public GameObject PlayerMissile;

	public float acel = 75f;
	public float maxSpeed = 5f;
	public float shotDelay = 0.75f;
	public float spawnDelay = 2f;
	public float invincibleDelay = 2f;
	public float invincibilityTranspStep = -10f / 255f;
	public float deathAnimLenght = 0.517f;
	public int lives = 3;
	public bool canMove = true;
	public bool canShoot = true; 
	public bool isDead = false;
	public bool isInvincible = false;


	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer> ();
		transform.position = new Vector3 (-1f, -9f, -5f);
		changeAnimation ();
		makeInvincible();
	}

	void OnTriggerEnter2D(Collider2D collided){
		if (collided.CompareTag ("Missile") && !isInvincible)
			lives--;
	}

	void FixedUpdate(){
		body.velocity *= 0.9f;
		float h = Input.GetAxis("Horizontal");

		if(canMove)
			body.AddForce(Vector2.right * h * acel);

		float clampedSpeed = Mathf.Clamp (body.velocity.x, -maxSpeed, maxSpeed);
		body.velocity = new Vector2 (clampedSpeed, 0f);

		if (canShoot && (Input.GetKey (KeyCode.Return) || Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W))) {
			Instantiate (PlayerMissile);
			canShoot = false;
			Invoke ("rechargeShot", shotDelay);
		}

		if (lives < 1 && !isDead) {
			canMove = false;
			canShoot = false;
			isDead = true;
			Invoke ("playerSpawn", deathAnimLenght + spawnDelay);
		}

		changeAnimation ();
	}

	void rechargeShot(){
		if(!isDead)
			canShoot = true;
	}

	void playerSpawn(){
		lives = 3;
		transform.position = new Vector3 (-1f,-9f,-5f);
		body.velocity = Vector2.zero;
		canMove = true;
		canShoot = true;
		isDead = false;
		makeInvincible ();
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
