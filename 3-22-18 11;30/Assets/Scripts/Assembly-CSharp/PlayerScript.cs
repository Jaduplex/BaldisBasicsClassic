using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
	public float mouseSensitivity;

	public float walkSpeed;

	public float runSpeed;

	public float maxStamina;

	public float staminaRate;

	public float staminaTripped;

	public float guilt;

	public float initGuilt;

	private float moveX;

	private float moveZ;

	private float playerSpeed;

	public float stamina;

	public bool playerTripped;

	public Rigidbody rb;

	public Slider staminaBar;

	public float db;

	private string guiltType;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		stamina = maxStamina;
	}

	private void Update()
	{
		StaminaCheck();
		GuiltCheck();
	}

	private void FixedUpdate()
	{
		PlayerMove();
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
				stamina -= staminaRate * Time.deltaTime;
			}
			if (stamina <= 0f)
			{
				playerTripped = true;
				stamina = staminaTripped;
			}
		}
		else if (stamina < maxStamina)
		{
			stamina += staminaRate * Time.deltaTime;
			if (playerTripped & (stamina > 0f))
			{
				stamina = maxStamina;
				playerTripped = false;
			}
		}
		staminaBar.value = stamina / maxStamina * 100f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Joe"))
		{
			other.gameObject.transform.position = new Vector3(37f, 1f, 335f);
		}
	}

	public void ResetGuilt(string type)
	{
		guilt = initGuilt;
		guiltType = type;
	}

	private void GuiltCheck()
	{
		if (guilt > 0f)
		{
			guilt -= Time.deltaTime;
		}
	}
}
