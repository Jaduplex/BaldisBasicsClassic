using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
	public PlayerScript player;

	public Transform playerTransform;

	public EntranceScript entrance_0;

	public EntranceScript entrance_1;

	public EntranceScript entrance_2;

	public EntranceScript entrance_3;

	public GameObject baldiTutor;

	public GameObject baldi;

	public BaldiScript baldiScrpt;

	public AudioClip aud_Prize;

	public GameObject principal;

	public GameObject crafters;

	public GameObject quarter;

	public AudioSource tutorBaldi;

	public int notebooks;

	public bool spoopMode;

	public bool finaleMode;

	public bool debugMode;

	public bool mouseLocked;

	public int exitsReached;

	public int itemSelected;

	private int[] item = new int[3];

	public RawImage[] itemSlot = new RawImage[3];

	private string[] itemNames = new string[8] { "Nothing", "Energy flavored Zesty Bar", "Yellow Door Lock", "Key", "BSODA", "Quarter", "Tape", "Alarm Clock" };

	public Text itemText;

	public Object[] items = new Object[8];

	public Texture[] itemTextures = new Texture[8];

	public GameObject bsodaSpray;

	public Text notebookCount;

	public GameObject pauseText;

	public GameObject warning;

	public GameObject reticle;

	public RectTransform itemSelect;

	private int[] itemSelectOffset = new int[3] { -80, -40, 0 };

	private bool gamePaused;

	private bool learningActive;

	private float gameOverDelay;

	private AudioSource audioDevice;

	public AudioClip aud_buzz;

	private void Start()
	{
		audioDevice = GetComponent<AudioSource>();
		LockMouse();
		UpdateNotebookCount();
		itemSelected = 0;
		gameOverDelay = 60f;
	}

	private void Update()
	{
		if (!learningActive)
		{
			if (Input.GetButtonDown("Pause"))
			{
				if (!gamePaused)
				{
					PauseGame();
				}
				else
				{
					UnpauseGame();
				}
			}
			if (Input.GetKeyDown(KeyCode.Q) & gamePaused)
			{
				SceneManager.LoadScene("MainMenu");
			}
			if (!gamePaused & (Time.timeScale != 1f))
			{
				Time.timeScale = 1f;
			}
			if (Input.GetMouseButtonDown(1))
			{
				UseItem();
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0f)
			{
				DecreaseItemSelection();
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
			{
				IncreaseItemSelection();
			}
		}
		else if (Time.timeScale != 0f)
		{
			Time.timeScale = 0f;
		}
		if ((player.stamina < 0f) & !warning.activeSelf)
		{
			warning.SetActive(true);
		}
		else if (warning.activeSelf)
		{
			warning.SetActive(false);
		}
		if (player.gameOver)
		{
			Time.timeScale = 0f;
			gameOverDelay -= 1f;
			audioDevice.PlayOneShot(aud_buzz);
			if (gameOverDelay <= 0f)
			{
				Time.timeScale = 1f;
				SceneManager.LoadScene("GameOver");
			}
		}
	}

	private void UpdateNotebookCount()
	{
		notebookCount.text = notebooks + "/7";
		if (notebooks == 7)
		{
			ActivateFinaleMode();
		}
	}

	public void CollectNotebook()
	{
		notebooks++;
		UpdateNotebookCount();
	}

	public void LockMouse()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		mouseLocked = true;
		reticle.SetActive(true);
	}

	public void UnlockMouse()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		mouseLocked = false;
		reticle.SetActive(false);
	}

	private void PauseGame()
	{
		Time.timeScale = 0f;
		gamePaused = true;
		pauseText.SetActive(true);
	}

	private void UnpauseGame()
	{
		Time.timeScale = 1f;
		gamePaused = false;
		pauseText.SetActive(false);
	}

	public void ActivateSpoopMode()
	{
		spoopMode = true;
		entrance_0.Lower();
		entrance_1.Lower();
		entrance_2.Lower();
		entrance_3.Lower();
		baldiTutor.SetActive(false);
		baldi.SetActive(true);
		principal.SetActive(true);
		crafters.SetActive(true);
	}

	private void ActivateFinaleMode()
	{
		finaleMode = true;
		entrance_0.Raise();
		entrance_1.Raise();
		entrance_2.Raise();
		entrance_3.Raise();
	}

	public void GetAngry(float value)
	{
		if (!spoopMode)
		{
			ActivateSpoopMode();
		}
		baldiScrpt.GetAngry(value);
	}

	public void ActivateLearningGame()
	{
		learningActive = true;
		UnlockMouse();
	}

	public void DeactivateLearningGame(GameObject subject)
	{
		learningActive = false;
		LockMouse();
		subject.SetActive(false);
		if ((notebooks == 1) & !spoopMode)
		{
			quarter.SetActive(true);
			tutorBaldi.PlayOneShot(aud_Prize);
		}
	}

	private void IncreaseItemSelection()
	{
		itemSelected++;
		if (itemSelected > 2)
		{
			itemSelected = 0;
		}
		itemSelect.anchoredPosition = new Vector3(itemSelectOffset[itemSelected], 0f, 0f);
		UpdateItemName();
	}

	private void DecreaseItemSelection()
	{
		itemSelected--;
		if (itemSelected < 0)
		{
			itemSelected = 2;
		}
		itemSelect.anchoredPosition = new Vector3(itemSelectOffset[itemSelected], 0f, 0f);
		UpdateItemName();
	}

	public void CollectItem(int item_ID)
	{
		if (item[0] == 0)
		{
			item[0] = item_ID;
			itemSlot[0].texture = itemTextures[item_ID];
		}
		else if (item[1] == 0)
		{
			item[1] = item_ID;
			itemSlot[1].texture = itemTextures[item_ID];
		}
		else if (item[2] == 0)
		{
			item[2] = item_ID;
			itemSlot[2].texture = itemTextures[item_ID];
		}
		else
		{
			item[itemSelected] = item_ID;
			itemSlot[itemSelected].texture = itemTextures[item_ID];
		}
		UpdateItemName();
	}

	private void UseItem()
	{
		if (item[itemSelected] == 0)
		{
			return;
		}
		if (item[itemSelected] == 1)
		{
			player.stamina = player.maxStamina * 2f;
			ResetItem();
		}
		else if (item[itemSelected] == 2)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo) && ((hitInfo.collider.tag == "SwingingDoor") & (Vector3.Distance(playerTransform.position, hitInfo.transform.position) <= 10f)))
			{
				hitInfo.collider.gameObject.GetComponent<SwingingDoorScript>().LockDoor(15f);
				ResetItem();
			}
		}
		else if (item[itemSelected] == 3)
		{
			Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo2;
			if (Physics.Raycast(ray2, out hitInfo2) && ((hitInfo2.collider.tag == "Door") & (Vector3.Distance(playerTransform.position, hitInfo2.transform.position) <= 10f)))
			{
				hitInfo2.collider.gameObject.GetComponent<DoorScript>().UnlockDoor();
				hitInfo2.collider.gameObject.GetComponent<DoorScript>().OpenDoor();
				ResetItem();
			}
		}
		else if (item[itemSelected] == 4)
		{
			Object.Instantiate(bsodaSpray, playerTransform.position, playerTransform.rotation);
			ResetItem();
		}
		else if (item[itemSelected] == 5)
		{
			Ray ray3 = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo3;
			if (Physics.Raycast(ray3, out hitInfo3))
			{
				if ((hitInfo3.collider.name == "BSODAMachine") & (Vector3.Distance(playerTransform.position, hitInfo3.transform.position) <= 10f))
				{
					ResetItem();
					CollectItem(4);
				}
				else if ((hitInfo3.collider.name == "ZestyMachine") & (Vector3.Distance(playerTransform.position, hitInfo3.transform.position) <= 10f))
				{
					ResetItem();
					CollectItem(1);
				}
				else if ((hitInfo3.collider.name == "PayPhone") & (Vector3.Distance(playerTransform.position, hitInfo3.transform.position) <= 10f))
				{
					hitInfo3.collider.gameObject.GetComponent<TapePlayerScript>().Play();
					ResetItem();
				}
			}
		}
		else if (item[itemSelected] == 6)
		{
			Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo4;
			if (Physics.Raycast(ray4, out hitInfo4) && ((hitInfo4.collider.name == "TapePlayer") & (Vector3.Distance(playerTransform.position, hitInfo4.transform.position) <= 10f)))
			{
				hitInfo4.collider.gameObject.GetComponent<TapePlayerScript>().Play();
				ResetItem();
			}
		}
	}

	private void ResetItem()
	{
		item[itemSelected] = 0;
		itemSlot[itemSelected].texture = itemTextures[0];
		UpdateItemName();
	}

	private void UpdateItemName()
	{
		itemText.text = itemNames[item[itemSelected]];
	}

	public void ExitReached()
	{
		exitsReached++;
	}

	public void DespawnCrafters()
	{
		crafters.SetActive(false);
	}
}
