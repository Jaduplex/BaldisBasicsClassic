using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public float mouseSensitivity;

	public float walkSpeed;

	public float runSpeed;

	public float maxStamina;

	public float staminaRate;

	public float staminaTripped;

	private float moveX;

	private float moveZ;

	private float playerSpeed;

	public float stamina;

	public bool playerTripped;

	public Rigidbody rb;

	public float db;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		stamina = maxStamina;
	}

	private void Update()
	{
		PlayerMove();
		StaminaCheck();
		rb.velocity = new Vector3(moveX, 0f, moveZ);
	}

	private void PlayerMove()
	{
		moveX = 0f;
		moveZ = 0f;
		base.transform.Rotate(0f, Input.GetAxis("Mouse X") * mouseSensitivity, 0f);
		float y = base.transform.eulerAngles.y;
		db = Input.GetAxisRaw("Forward");
		if (Input.GetAxisRaw("Run") > 0f)
		{
			playerSpeed = runSpeed;
		}
		else
		{
			playerSpeed = walkSpeed;
		}
		if (Input.GetAxis("Forward") > 0f)
		{
			moveZ += Mathf.Cos(y * ((float)Math.PI / 180f)) * playerSpeed;
			moveX += Mathf.Sin(y * ((float)Math.PI / 180f)) * playerSpeed;
		}
		else if (Input.GetAxis("Forward") < 0f)
		{
			moveZ += 0f - Mathf.Cos(y * ((float)Math.PI / 180f)) * playerSpeed;
			moveX += 0f - Mathf.Sin(y * ((float)Math.PI / 180f)) * playerSpeed;
		}
		if (Input.GetAxis("Strafe") > 0f)
		{
			moveZ += Mathf.Cos((y + 90f) * ((float)Math.PI / 180f)) * playerSpeed;
			moveX += Mathf.Sin((y + 90f) * ((float)Math.PI / 180f)) * playerSpeed;
		}
		else if (Input.GetAxis("Strafe") < 0f)
		{
			moveZ += 0f - Mathf.Cos((y + 90f) * ((float)Math.PI / 180f)) * playerSpeed;
			moveX += 0f - Mathf.Sin((y + 90f) * ((float)Math.PI / 180f)) * playerSpeed;
		}
		if (playerTripped)
		{
			moveX = 0f;
			moveZ = 0f;
		}
	}

	private void StaminaCheck()
	{
		if (moveZ != 0f || moveX != 0f)
		{
			if (Input.GetAxisRaw("Run") > 0f)
			{
				stamina -= staminaRate;
			}
			if (stamina <= 0f)
			{
				playerTripped = true;
				stamina = staminaTripped;
			}
		}
		else if (stamina < maxStamina)
		{
			stamina += staminaRate;
			if (playerTripped & (stamina > 0f))
			{
				stamina = maxStamina;
				playerTripped = false;
			}
		}
	}
}
