using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;

	public float runSpeed;
	public bool canMove = true;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

    private void Start()
    {
	
	}

    // Update is called once per frame
    void Update () 
	{
		if(canMove == true)
        {
			runSpeed = 40;

		}
		if (canMove == false)
		{
			runSpeed = 0;
		}

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			jump = true;
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}


	}
  

    void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
