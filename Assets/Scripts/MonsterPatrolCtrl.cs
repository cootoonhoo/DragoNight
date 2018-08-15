﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrolCtrl : MonoBehaviour {

	public Transform pos1, pos2;

	public float speed = 2f;

	public float waitTime = 1f;

	Vector3 nextPos;
	Animator anim;
	SpriteRenderer sr;

	void Start () {
		anim = GetComponent<Animator>();
		nextPos = pos1.position;
		sr = GetComponent<SpriteRenderer>();
		StartCoroutine(Move());
	}
	
	IEnumerator Move(){
		while(true){
			if (transform.position == pos1.position){
				nextPos = pos2.position;
				anim.SetInteger("State", 1);
				sr.flipX = !sr.flipX;
				yield return new WaitForSeconds(waitTime);
				anim.SetInteger("State", 0);
			}
			if (transform.position == pos2.position){
				nextPos = pos1.position;
				anim.SetInteger("State", 1);
				sr.flipX = !sr.flipX;
				yield return new WaitForSeconds(waitTime);
				anim.SetInteger("State", 0);
			}
			transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
			yield return null;
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawLine(pos1.position, pos2.position);
	}
}
