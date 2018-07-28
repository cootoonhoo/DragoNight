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
	public Transform feet;
	public float feetWidth = 0.2f;
	public float feetHeight = 0.05f;
	public bool isGrounded;
	public LayerMask WhatIsGround;
	bool canDoubleJump = false;
	public float delayForDoubleJump = 0.2f;

	// Use this for initialization	
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		Sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(feet.position, new Vector3(feetWidth,feetHeight,0f));
	}	
	// Update is called once per frame
	void Update () {

		if(transform.position.y < GM.instance.yMinLive){
			GM.instance.KillPlayer();
		}

		isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x,feet.position.y), new Vector2(feetWidth, feetHeight), 360.0f, WhatIsGround);

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
		if(isGrounded){
		isJumping = true;
		rb.AddForce(new Vector2(0f, jumpSpeed));
		anim.SetInteger("State" , 1);

		Invoke("EnableDoubleJump", delayForDoubleJump);
		}
		if(canDoubleJump && !isGrounded ){
			rb.velocity = Vector2.zero;
		rb.AddForce(new Vector2(0f, jumpSpeed));
		anim.SetInteger("State" , 1);
		canDoubleJump = false;
		}
	}

	void EnableDoubleJump(){
		canDoubleJump = true;
	}

		void OnCollisionEnter2D(Collision2D other){
			if (other.gameObject.layer == LayerMask.NameToLayer("Ground")){
				isJumping = false;
			}
		}
	}


