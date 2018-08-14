using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControll : MonoBehaviour {
	public float speed = 2f;
	public float turnTime = 1f;
	Rigidbody2D rb;
	SpriteRenderer sr;
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
	}
		void Update () {
			Move();
	}
	void Move(){
		rb.velocity = new Vector2(speed, rb.velocity.y);
	}
}
