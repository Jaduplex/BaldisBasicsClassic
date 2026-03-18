using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
	public PlayerScript player;

	public EntranceScript entrance_0;

	public EntranceScript entrance_1;

	public EntranceScript entrance_2;

	public EntranceScript entrance_3;

	public GameObject baldiTutor;

	public GameObject baldi;

	public BaldiScript baldiScrpt;

	public GameObject principal;

	public GameObject crafters;

	public int notebooks;

	public bool spoopMode;

	public bool finaleMode;

	public bool debugMode;

	public bool mouseLocked;

	public int exitsReached;

	public int itemSelected;

	private int[] item = new int[3];

	public RawImage[] itemSlot = new RawImage[3];

	public Object[] items = new Object[6];

	public Texture[] itemTextures = new Texture[6];

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
			if (Input.GetMouseButtonDown(2))
			{
				if (!mouseLocked)
				{
					LockMouse();
				}
				else
				{
					UnlockMouse();
				}
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
		mouseLocked = true;
		reticle.SetActive(true);
	}

	public void UnlockMouse()
	{
		Cursor.lockState = CursorLockMode.None;
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
	}

	private void IncreaseItemSelection()
	{
		itemSelected++;
		if (itemSelected > 2)
		{
			itemSelected = 0;
		}
		itemSelect.anchoredPosition = new Vector3(itemSelectOffset[itemSelected], 0f, 0f);
	}

	private void DecreaseItemSelection()
	{
		itemSelected--;
		if (itemSelected < 0)
		{
			itemSelected = 2;
		}
		itemSelect.anchoredPosition = new Vector3(itemSelectOffset[itemSelected], 0f, 0f);
	}

	public void CollectItem(int item_ID)
	{
		item[itemSelected] = item_ID;
		itemSlot[itemSelected].texture = itemTextures[item_ID];
	}

	private void UseItem()
	{
		if (item[itemSelected] != 0 && item[itemSelected] == 1)
		{
			player.stamina = player.maxStamina * 2f;
			ResetItem();
		}
	}

	private void ResetItem()
	{
		item[itemSelected] = 0;
		itemSlot[itemSelected].texture = itemTextures[0];
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
