using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float horizontalSpeed = 10f ;
	public float jumpSpeed = 600f;
	Rigidbody2D rb;

	// Use this for initialization	
	void Start () {
		rb = GetComponent<Rigidbody2D>();
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

			StopMoving();

		}

		//teste para pulo

		if (Input.GetButtonDown("Jump")){
			Jump();
		}
	}

	void MoveHorizontal(float horizontalSpeed){
		
		rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);

	}
	void StopMoving(){

		rb.velocity = new Vector2(0f, rb.velocity.y);

	}

	void Jump(){
		rb.AddForce(new Vector2(0f, jumpSpeed));
	}


}
