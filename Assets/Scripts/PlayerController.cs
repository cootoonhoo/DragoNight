using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//States da animação

// 0 - Idle
// 1 - Jump
// 2 - Run
// 3 - Fall
// 4 - Attack
// 5 - Hit

public class PlayerController : MonoBehaviour {

	SpriteRenderer Sr;
	public float horizontalSpeed = 10f ;
	public float jumpSpeed = 600f;
	Rigidbody2D rb;
	Animator anim;
	bool isJumping = false;

	// Use this for initialization	
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		Sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}	
	// Update is called once per frame
	void Update () {
//teste para andar
		float horizontalInput = Input.GetAxisRaw("Horizontal"); // -1 Esquerda, 1 Direita
		float horizontalPlayerSpeed = horizontalSpeed*horizontalInput;
		if(horizontalPlayerSpeed !=0 ){
			MoveHorizontal(horizontalPlayerSpeed);
		}
		else {
			StopMovingHorizontal();
		}
		//teste para pulo
		if (Input.GetButtonDown("Jump")){
			Jump();
		}
		ShowFalling();
	}
	void MoveHorizontal(float speed){
		rb.velocity = new Vector2(speed, rb.velocity.y);
	if(speed <0f){
		Sr.flipX =true; 
	}
	else if(speed > 0f){
		Sr.flipX =false;
		}
		if(!isJumping){
			anim.SetInteger("State" , 2);
		}
	}
	void StopMovingHorizontal(){
		rb.velocity = new Vector2(0f, rb.velocity.y);
		if(!isJumping){
			anim.SetInteger("State" , 0);			
		}
	}
	void ShowFalling(){
		if (rb.velocity.y <0){
			anim.SetInteger("State", 3);
		}
	}

	void Jump(){
		isJumping = true;
		rb.AddForce(new Vector2(0f, jumpSpeed));
		anim.SetInteger("State" , 1);
		Debug.Log(isJumping + "Executou o pulo");
		}
		void OnCollisionEnter2D(Collision2D other){
			if (other.gameObject.layer == LayerMask.NameToLayer("Ground")){
				isJumping = false;
			}
		}
	}


