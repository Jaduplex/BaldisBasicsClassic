using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
	public GameControllerScript gc;

	public BaldiScript baldi;

	public bool gameOver;

	public float mouseSensitivity;

	public float walkSpeed;

	public float runSpeed;

	public float slowSpeed;

	public float maxStamina;

	public float staminaRate;

	public float guilt;

	public float initGuilt;

	private float moveX;

	private float moveZ;

	private float playerSpeed;

	public float stamina;

	public Rigidbody rb;

	public Slider staminaBar;

	public float db;

	public string guiltType;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		stamina = maxStamina;
	}

	private void Update()
	{
		StaminaCheck();
		GuiltCheck();
		if (moveZ != 0f || moveX != 0f)
		{
			gc.LockMouse();
		}
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
		if (gc.mouseLocked)
		{
			base.transform.Rotate(0f, Input.GetAxis("Mouse X") * mouseSensitivity, 0f);
		}
		float y = base.transform.eulerAngles.y;
		db = Input.GetAxisRaw("Forward");
		if (stamina > 0f)
		{
			if (Input.GetAxisRaw("Run") > 0f)
			{
				playerSpeed = runSpeed;
				if (rb.velocity.magnitude > 0f)
				{
					ResetGuilt("running", 0.2f);
				}
			}
			else
			{
				playerSpeed = walkSpeed;
			}
		}
		else
		{
			playerSpeed = slowSpeed;
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
	}

	private void StaminaCheck()
	{
		if (moveZ != 0f || moveX != 0f)
		{
			if ((Input.GetAxisRaw("Run") > 0f) & (stamina > 0f))
			{
				stamina -= staminaRate * Time.deltaTime;
			}
			if ((stamina < 0f) & (stamina > -25f))
			{
				stamina = -25f;
			}
		}
		else if (stamina < maxStamina)
		{
			stamina += staminaRate * Time.deltaTime;
		}
		staminaBar.value = stamina / maxStamina * 100f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((other.transform.name == "Baldi") & !gc.debugMode)
		{
			gameOver = true;
		}
	}

	public void ResetGuilt(string type, float amount)
	{
		if (amount >= guilt)
		{
			guilt = amount;
			guiltType = type;
		}
	}

	private void GuiltCheck()
	{
		if (guilt > 0f)
		{
			guilt -= Time.deltaTime;
		}
	}
}
