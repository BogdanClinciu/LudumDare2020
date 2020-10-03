using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float runSpeed = 40f;

    private PlayerController controller;


	private float horizontalMove = 0f;
	private bool jump = false;

	private void Awake()
	{
		controller = GetComponent<PlayerController>();
	}

	private void Update()
    {
		if (!GetComponent<PlayerController>().ControlEnabled)
			return;
		
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if(Input.GetButtonDown("Jump"))
		{
			jump = true;
		}
    }

	private void FixedUpdate()
	{
		if (!GetComponent<PlayerController>().ControlEnabled)
			return;

		controller.Move(horizontalMove * GameManager.Instance.FixedDeltaTime , false, jump);
		jump = false;
	}
}
