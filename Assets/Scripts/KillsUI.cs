using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillsUI : MonoBehaviour {

	public int kills = 0;
	private Animator[] number = new Animator[3];

	// Use this for initialization
	void Start () {
		number [0] = transform.Find ("Number1").GetComponent<Animator> ();
		number [1] = transform.Find ("Number2").GetComponent<Animator> ();
		number [2] = transform.Find ("Number3").GetComponent<Animator> ();
		changeAnimation ();
	}

	// Update is called once per frame
	void Update () {
		while (kills > 999)
			kills %= 1000;
		changeAnimation ();
	}

	void changeAnimation () {
		int aux = kills;
		for(int i = 0; i < 3; ++i) {
			number [i].SetInteger ("number", aux % 10);
			aux /= 10;
		}
	}
}
