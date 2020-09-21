using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class Player_Platformer : MonoBehaviour {

	private Rigidbody rigidBody;

	bool movingLeft;
	bool movingRight;

	private float playerSpeed = 0.1f;
	private float jumpForce = 350f;

	private bool isInSphere;

	private int counter = 0;
	private int counter1 = -1;
	private int counter2 = -1;

	private void Start (){
		rigidBody = GetComponent<Rigidbody> ();
	}

	public void ButtonInput (JToken input){
		Debug.Log(input);
		if (counter1 == -1)
		{
			counter1 = counter;

		} else if (counter2 == -1)
		{
			counter2 = counter;
		}
		
		if (input["jump"] != null && input["jump"]["up"].ToString() == "True") {
			rigidBody.AddForce(transform.up * jumpForce);

		} else if (input["interact"] != null && input["interact"].ToString() == "False")
		{
			if (isInSphere)
			{
				if (Camera.main.backgroundColor == Color.yellow)
				{
					Camera.main.backgroundColor = Color.blue;
				}
				else
				{
					Camera.main.backgroundColor = Color.yellow;
				}
			}
		}
		else if(input["action"] != null)
		{
			Debug.Log(input["action"]["down"].ToString());

			if (input["action"]["left"].ToString() == "True")
			{
				movingLeft = true;
				movingRight = false;
			}
			else if (input["action"]["right"].ToString() == "True")
			{
				movingRight = true;
				movingLeft = false;
			}
			else
			{
				movingRight = false;
				movingLeft = false;
			}

		}

		/*switch (input) {
		case "right":
			movingRight = true;
			break;
		case "left":
			movingLeft = true;
			break;
		case "right-up":
			movingRight = false;
			break;
		case "left-up":
			movingLeft = false;
			break;
		case "jump":
			rigidBody.AddForce (transform.up * jumpForce);
			break;
		case "interact":
			if (isInSphere) {
				if (Camera.main.backgroundColor == Color.yellow) {
					Camera.main.backgroundColor = Color.blue;
				} else {
					Camera.main.backgroundColor = Color.yellow;
				}
			}
			break;
		}*/
	}

	private void FixedUpdate(){
		//Debug.Log(counter++);
		counter++;
		if (counter > 1000)
		{
			counter = 0;
		}
		if (movingLeft && !movingRight) {
			rigidBody.MovePosition(rigidBody.position + new Vector3 (-playerSpeed, 0, 0)); 
		} else if (!movingLeft && movingRight) {
			rigidBody.MovePosition(rigidBody.position + new Vector3 (playerSpeed, 0, 0)); 
		}
	}

	//Track if the player capsule is currently inside the transparent sphere or not
	void OnTriggerEnter(Collider trigger){
		if (trigger.tag == "PlatformSphere") {
			isInSphere = true;
		}
	}

	void OnTriggerExit(Collider trigger){
		if (trigger.tag == "PlatformSphere") {
			isInSphere = false;
		}
	}
}
