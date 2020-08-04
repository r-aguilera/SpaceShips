using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour {

	public float speed = 0.05f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position += Vector3.down * speed;
		if (transform.position.y <= -20)
			transform.position = Vector3.zero;
	}
}
